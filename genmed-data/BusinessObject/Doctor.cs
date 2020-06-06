using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reumed.DataAccess.BusinessObjects
{
    public class Doctor : RecordBase<Doctor>
    {
        #region Constructor
        public Doctor() : base()
        {
            SetInitialState();
        }
        #endregion


        #region Properties

        [JsonProperty("doctorid")]
        public int DoctorId { get; set; }

        [JsonProperty("guid")]
        public Guid Guid { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("Apellido")]
        public string Apellido { get; set; }

        [JsonProperty("posicion")]
        public string Posicion { get; set; }

        [JsonProperty("usuario")]
        public Usuario Usuario { get; set; }

        public string NombreCompleto
        {
            get { return Nombre + " " + Apellido; }
        }
        #endregion
    }
}
