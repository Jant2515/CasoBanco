using System.ComponentModel.DataAnnotations;

namespace BancoPromericaCaso.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Favor ingrese un Correo")]
        public string Correo { get; set; }
        [Required(ErrorMessage = "Favor ingrese una Contraseña")]
        public string Contrasena { get; set; }
    }
}
