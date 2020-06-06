using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reumed.DataAccess.BusinessObjects
{
    public class Consulta : RecordBase<Consulta>
    {
        #region Constructor
        public Consulta()
        {
            SetInitialState();
        }
        #endregion

        #region Properties

        [JsonProperty("consultaid")]
        public int ConsultaId { get; set; }

        [JsonProperty("detalles")]
        public string Detalles { get; set; }

        public Paciente Paciente { get; set; }

        public Cita Cita { get; set; }
        #endregion
    }
}
