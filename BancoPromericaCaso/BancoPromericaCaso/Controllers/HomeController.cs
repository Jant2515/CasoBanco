using System.Diagnostics;
using BancoPromericaCaso.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;
using ClosedXML.Excel;
using System.Text;
using JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior;

namespace BancoPromericaCaso.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Reportes()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public FileResult exportar(string empleado, string fechainicio, string fechafin)
        {

            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection("Data Source=LAPTOP-CD23EGUT\\SQLEXPRESS; Initial Catalog=BancoDB; Persist Security Info=False; Trusted_Connection=True; TrustServerCertificate=True"))
            {
                StringBuilder consulta = new StringBuilder();
                consulta.AppendLine("SET DATEFORMAT dmy;");
                consulta.AppendLine("select * from [dbo].[citas] where IdUsuario = iif(@usuario =0,IdUsuario,@usuario) and convert(date,OrderDate) between @fechainicio and @fechafin");


                SqlCommand cmd = new SqlCommand(consulta.ToString(), cn);
                cmd.Parameters.AddWithValue("@employee", empleado);
                cmd.Parameters.AddWithValue("@fechainicio", fechainicio);
                cmd.Parameters.AddWithValue("@fechafin", fechafin);
                cmd.CommandType = CommandType.Text;

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            dt.TableName = "Datos";

            using (XLWorkbook libro = new XLWorkbook())
            {
                var hoja = libro.Worksheets.Add(dt);

                hoja.ColumnsUsed().AdjustToContents();

                using (MemoryStream stream = new MemoryStream())
                {
                    libro.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte " + DateTime.Now.ToString() + ".xlsx");
                }
            }


        }

        public JsonResult obtenerUsuario()
        {
            List<usuario> listaUsuario = new List<usuario>();

            using (SqlConnection cn = new SqlConnection("Data Source=LAPTOP-CD23EGUT\\SQLEXPRESS; Initial Catalog=BancoDB; Persist Security Info=False; Trusted_Connection=True; TrustServerCertificate=True"))
            {
                SqlCommand cmd = new SqlCommand("select IdUsuario,concat(Nombres,' ',Apellidos)[Nombres] from [dbo].[usuario]", cn);
                cmd.CommandType = CommandType.Text;
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listaUsuario.Add(new usuario()
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            Nombres = dr["Nombres"].ToString()
                        });
                    }
                }
            }

            return Json(listaUsuario, JsonRequestBehavior.AllowGet);
        }
    }
}