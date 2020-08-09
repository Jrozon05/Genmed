using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace genmed_api.Dtos.Usuario
{
    public class UsuarioRegistrarDto
    {
        [Required(ErrorMessage = "El de Nombre Usuario es un campo requerido.")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "El email es un campo requerido")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "La Clave es requerido.")]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Debe especificar clave entre 4 a 8 caracteres.")]
        public string Clave { get; set; }

        [Compare("Clave", ErrorMessage = "Tanto la contraseña como la contraseña de confirmacion deben ser la mismas.")]
        public string ConfirmarClave { get; set; }

        [Required(ErrorMessage = "El doctor es un campo requerido.", AllowEmptyStrings = false)]
        public int DoctorId { get; set; }

        public List<SelectListItem> Rol { get; set; }

        [Required(ErrorMessage = "El rol es un campo requerido.", AllowEmptyStrings = false)]
        public int RolId { get; set; }
    }
}