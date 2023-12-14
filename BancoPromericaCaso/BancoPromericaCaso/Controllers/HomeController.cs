using System.Diagnostics;
using BancoPromericaCaso.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using Microsoft.Data.SqlClient;
using ClosedXML.Excel;
using System.Text;

namespace BancoPromericaCaso.Controllers
{
    public class HomeController : Controller
    {
        private readonly BancoContext _context;

        public HomeController(BancoContext context)
        {
            _context = context;
        }
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
            ViewData["DropUsuarios"] = new SelectList(_context.usuario, "IdUsuario", "Nombres");
            ViewData["DropClientes"] = new SelectList(_context.clientes, "IdCliente", "NombreCompleto");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public FileResult exportar(string ejecutivo, string fechainicio, string fechafin)
        {

            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection("Data Source=LAPTOP-CD23EGUT\\SQLEXPRESS; Initial Catalog=BancoDB; Persist Security Info=False; Trusted_Connection=True; TrustServerCertificate=True"))
            {
                StringBuilder consulta = new StringBuilder();
                consulta.AppendLine("SET DATEFORMAT dmy;");

                consulta.AppendLine("SELECT c.IdCita, u.Nombres AS NombreUsuario, cl.NombreCompleto AS NombreCliente, c.FechaHora, c.Descripcion " +
                "FROM [dbo].[CITAS] c " +
                "JOIN [dbo].[USUARIO] u ON c.IdUsuario = u.IdUsuario " +
                "JOIN [dbo].[CLIENTES] cl ON c.IdCliente = cl.IdCliente " +
                "WHERE c.IdUsuario = IIF(@usuario = 0, c.IdUsuario, @usuario) AND CONVERT(date, c.FechaHora) BETWEEN @fechainicio AND @fechafin");



                SqlCommand cmd = new SqlCommand(consulta.ToString(), cn);
                cmd.Parameters.AddWithValue("@usuario", ejecutivo);
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

        public FileResult exportar2(string cliente, string fechainicio2, string fechafin2)
        {

            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection("Data Source=LAPTOP-CD23EGUT\\SQLEXPRESS; Initial Catalog=BancoDB; Persist Security Info=False; Trusted_Connection=True; TrustServerCertificate=True"))
            {
                StringBuilder consulta = new StringBuilder();
                consulta.AppendLine("SET DATEFORMAT dmy;");

                consulta.AppendLine("SELECT c.IdCita, u.Nombres AS NombreUsuario, cl.NombreCompleto AS NombreCliente, c.FechaHora, c.Descripcion " +
                "FROM [dbo].[CITAS] c " +
                "JOIN [dbo].[USUARIO] u ON c.IdUsuario = u.IdUsuario " +
                "JOIN [dbo].[CLIENTES] cl ON c.IdCliente = cl.IdCliente " +
                "WHERE c.IdCliente = IIF(@clientes = 0, c.IdCliente, @clientes) AND CONVERT(date, c.FechaHora) BETWEEN @fechainicio2 AND @fechafin2");



                SqlCommand cmd = new SqlCommand(consulta.ToString(), cn);
                cmd.Parameters.AddWithValue("@clientes", cliente);
                cmd.Parameters.AddWithValue("@fechainicio2", fechainicio2);
                cmd.Parameters.AddWithValue("@fechafin2", fechafin2);
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
    }
}