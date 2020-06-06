using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reumed.DataAccess.BusinessObjects
{
    public class Cuello : RecordBase<Cuello>
    {
        #region Constructor
        public Cuello()
        {
            SetInitialState();
        }
        #endregion

        #region Properties

        [JsonProperty("cuelloid")]
        public int CuelloId { get; set; }

        [JsonProperty("detalle")]
        public string Detalle { get; set; }

        [JsonProperty("categoria")]
        public string Categoria { get; set; }

        [JsonProperty("fisico")]
        public Fisico Fisico { get; set; }
        #endregion
    }
}
