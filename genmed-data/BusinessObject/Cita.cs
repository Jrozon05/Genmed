using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reumed.DataAccess.BusinessObjects
{
    public class Cita : RecordBase<Cita>
    {
        #region Constructor
        public Cita()
        {
            SetInitialState();
        }
        #endregion

        #region Properties

        [JsonProperty("citaid")]
        public int CitaId { get; set; }

        [JsonProperty("fechacita")]
        public DateTime FechaCita { get; set; }

        [JsonProperty("motivo")]
        public string Motivo { get; set; }

        public Paciente Paciente { get; set; }
        #endregion
    }
}
