using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace genmed_api.Dtos.Doctor 
{
    public class DoctorRegistrarDto {

        [Required(ErrorMessage = "El Nombre es un campo requerido.")]
        public string Nombre {get; set;}

        [Required(ErrorMessage = "El Apellido es un campo requerido.")]
        public string Apellido {get; set;}

        [Required(ErrorMessage = "La posicion es un campo requerido.")]
        public string Posicion {get; set;}

        [Required(ErrorMessage = "El usuario es necesario")]
        public int UsuarioId {get; set;}

    }
}