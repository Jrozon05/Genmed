using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reumed.DataAccess.BusinessObjects
{
    public class Abdomen : RecordBase<Abdomen>
    {
        #region Constructor
        public Abdomen()
        {
            SetInitialState();
        }
        #endregion

        #region Properties

        [JsonProperty("abdomenid")]
        public int AbdomenId { get; set; }

        [JsonProperty("detalle")]
        public string Detalle { get; set; }

        [JsonProperty("categoria")]
        public string Categoria { get; set; }

        [JsonProperty("fisico")]
        public Fisico Fisico { get; set; }
        #endregion
    }
}
