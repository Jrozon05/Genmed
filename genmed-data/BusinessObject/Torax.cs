using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reumed.DataAccess.BusinessObjects
{
    public class Torax : RecordBase<Torax>
    {
        #region Constructor
        public Torax()
        {
            SetInitialState();
        }
        #endregion

        #region Properties

        [JsonProperty("toraxid")]
        public int ToraxId { get; set; }

        [JsonProperty("detalle")]
        public string Detalle { get; set; }

        [JsonProperty("categoria")]
        public string Categoria { get; set; }

        [JsonProperty("fisico")]
        public Fisico Fisico { get; set; }
        #endregion
    }
}
