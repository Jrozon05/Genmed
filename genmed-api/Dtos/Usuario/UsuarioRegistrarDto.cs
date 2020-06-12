using System;
using System.ComponentModel.DataAnnotations;

namespace genmed_api.Dtos.Usuario
{
    public class UsuarioRegistrarDto
    {
        [Required(ErrorMessage = "El de Nombre Usuario es obligatorio.")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "La Clave es obligatorio.")]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Debe especificar clave entre 4 a 8 caracteres.")]
        public string Clave { get; set; }
    }
}