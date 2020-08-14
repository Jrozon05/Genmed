using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace genmed_api.Dtos.Usuario
{
    public class UsuarioActualizarClaveDto
    {
        public Guid Guid {get; set;}

        [Required(ErrorMessage = "La Clave es requerido.")]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "Debe especificar clave entre 4 a 16 caracteres.")]
        public string Clave { get; set; }

        [Compare("Clave", ErrorMessage = "Tanto la contraseña como la contraseña de confirmacion deben ser la mismas.")]
        public string ConfirmarClave { get; set; }

    }
}