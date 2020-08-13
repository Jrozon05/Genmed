using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace genmed_api.Dtos.Doctor 
{
    public class DoctorRegistrarDto {

        public Guid Guid {get; set;}

        [Required(ErrorMessage = "El Nombre es un campo requerido.")]
        public string Nombre {get; set;}

        [Required(ErrorMessage = "El Apellido es un campo requerido.")]
        public string Apellido {get; set;}

        [Required(ErrorMessage = "La posicion es un campo requerido.")]
        public string Posicion {get; set;}

        public bool Activo {get; set;}

    }
}