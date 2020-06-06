using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reumed.DataAccess.BusinessObjects
{
    public class Boca : RecordBase<Boca>
    {
        #region Constructor
        public Boca()
        {
            SetInitialState();
        }
        #endregion

        #region Properties

        [JsonProperty("bocaid")]
        public int BocaId { get; set; }

        [JsonProperty("detalle")]
        public string Detalle { get; set; }

        [JsonProperty("categoria")]
        public string Categoria { get; set; }

        [JsonProperty("fisico")]
        public Fisico Fisico { get; set; }
        #endregion
    }
}
