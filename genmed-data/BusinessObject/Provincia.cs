using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reumed.DataAccess.BusinessObjects
{
    public class Provincia : RecordBase<Provincia>
    {
        #region Constructor
        public Provincia()
        {
            SetInitialState();
        }
        #endregion

        #region Properties

        [JsonProperty("provinciaid")]
        public int ProvinciaId { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        #endregion
    
    }
}
