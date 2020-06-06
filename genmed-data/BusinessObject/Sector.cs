using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reumed.DataAccess.BusinessObjects
{
    public class Sector : RecordBase<Sector>
    {
        #region Constructor
        public Sector()
        {
            SetInitialState();
        }
        #endregion

        #region Properties

        [JsonProperty("sectorid")]
        public int SectorId { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        #endregion
    }
}
