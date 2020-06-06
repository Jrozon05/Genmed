using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reumed.DataAccess.BusinessObjects
{
    public class SeguroMedico : RecordBase<SeguroMedico>
    {
        #region Constructor
        public SeguroMedico()
        {
            SetInitialState();
        }
        #endregion

        #region Properties

        [JsonProperty("seguromedicoid")]
        public int SeguroMedicoId { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        #endregion
    }
}
