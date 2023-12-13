using System.ComponentModel.DataAnnotations;

namespace BancoPromericaCaso.Models
{
    public class usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [StringLength(100)]
        public string Nombres { get; set; }

        [StringLength(100)]
        public string Apellidos { get; set; }

        [StringLength(100)]
        public string Correo { get; set; }

        [StringLength(100)]
        public string Contrasena { get; set; }

        public bool EsAdministrador { get; set; }

        public bool Activo { get; set; } = true;

        public List<citas>? citas { get; set; }
    }
}
