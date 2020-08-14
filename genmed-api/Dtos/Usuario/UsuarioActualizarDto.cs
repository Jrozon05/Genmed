using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace genmed_api.Dtos.Usuario
{
    public class UsuarioActualizarDto
    {
        public int UsuarioId {get; set;}
        public Guid Guid {get; set;}

        [Required(ErrorMessage = "El de Nombre Usuario es un campo requerido.")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "El email es un campo requerido")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "El rol es un campo requerido.", AllowEmptyStrings = false)]
        public int RolId { get; set; }

        public bool Activo {get; set;} = true;
    }
}