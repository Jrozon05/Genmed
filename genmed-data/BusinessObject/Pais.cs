using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reumed.DataAccess.BusinessObjects
{
    public class Pais : RecordBase<Pais>
    {
        #region Constructor
        public Pais()
        {
            SetInitialState();
        }
        #endregion

        #region Properties

        [JsonProperty("paisid")]
        public int PaisId { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        #endregion
    }
}
