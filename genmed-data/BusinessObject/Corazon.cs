using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reumed.DataAccess.BusinessObjects
{
    public class Corazon : RecordBase<Corazon>
    {
        #region Constructor
        public Corazon()
        {
            SetInitialState();
        }
        #endregion

        #region Properties

        [JsonProperty("corazonid")]
        public int CorazonId { get; set; }

        [JsonProperty("detalle")]
        public string Detalle { get; set; }

        [JsonProperty("categoria")]
        public string Categoria { get; set; }

        [JsonProperty("fisico")]
        public Fisico Fisico { get; set; }

        #endregion
    }
}
