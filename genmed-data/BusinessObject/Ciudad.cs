using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reumed.DataAccess.BusinessObjects
{
    public class Ciudad : RecordBase<Ciudad>
    {
        #region Constructor
        public Ciudad()
        {
            SetInitialState();
        }
        #endregion

        #region Properties

        [JsonProperty("ciudadid")]
        public int CiudadId { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        #endregion
    }
}
