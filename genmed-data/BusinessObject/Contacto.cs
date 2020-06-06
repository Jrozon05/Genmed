using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reumed.DataAccess.BusinessObjects
{
    public class Contacto : RecordBase<Contacto>
    {
        #region Constructor
        public Contacto()
        {
            SetInitialState();
        }
        #endregion

        #region Properties

        [JsonProperty("contactoid")]
        public int ContactoId { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("apellido")]
        public string Apellido { get; set; }

        [JsonProperty("telefono")]
        public string Telefono { get; set; }

        [JsonProperty("relacion")]
        public string Relacion { get; set; }

        [JsonProperty("paciente")]
        public Paciente Paciente { get; set; }
        #endregion
    }
}
