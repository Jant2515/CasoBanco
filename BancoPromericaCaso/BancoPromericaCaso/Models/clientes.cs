using System.ComponentModel.DataAnnotations;

namespace BancoPromericaCaso.Models
{
    public class clientes
    {
        [Key]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreCompleto { get; set; }

        [Required]
        [StringLength(100)]
        public string CorreoElectronico { get; set; }

        [StringLength(100)]
        public string Empresa { get; set; }

        [StringLength(20)]
        public string Telefono { get; set; }

        public List<citas>? citas { get; set; }
    }
}
