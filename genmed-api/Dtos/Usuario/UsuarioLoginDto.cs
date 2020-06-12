using System.ComponentModel.DataAnnotations;

namespace genmed_api.Dtos.Usuario
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "El Nombre Usuario es un campo requerido.")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Clave es un campo requerido.")]
        public string Clave { get; set; }
    }
}