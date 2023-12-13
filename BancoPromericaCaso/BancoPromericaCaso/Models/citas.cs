using System.ComponentModel.DataAnnotations;

namespace BancoPromericaCaso.Models
{
    public class citas
    {
        [Key]
        public int IdCita { get; set; }

        [Required]
        public DateTime FechaHora { get; set; }

        [StringLength(255)]
        public string Descripcion { get; set; }

        [Required]
        public int IdUsuario { get; set; }
        public usuario? usuario { get; set; }

        [Required]
        public int IdCliente { get; set; }
        public clientes? clientes { get; set; }
    }
}
