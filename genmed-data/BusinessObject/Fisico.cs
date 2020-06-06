using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reumed.DataAccess.BusinessObjects
{
    public class Fisico : RecordBase<Fisico>
    {
        #region Constructor
        public Fisico()
        {
            SetInitialState();
        }
        #endregion

        #region Properties

        [JsonProperty("fisicid")]
        public int FisicoId { get; set; }

        [JsonProperty("fecha")]
        public DateTime Fecha { get; set; }

        [JsonProperty("paciente")]
        public Paciente Paciente { get; set; }
        #endregion
    }
}
