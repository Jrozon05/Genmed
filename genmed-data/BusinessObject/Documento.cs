using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reumed.DataAccess.BusinessObjects
{
    public class Documento : RecordBase<Documento>
    {
        #region Constructor
        public Documento()
        {
            SetInitialState();
        }
        #endregion

        #region Properties

        [JsonProperty("documentoid")]
        public int DocumentoId { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("ruta")]
        public string Ruta { get; set; }

        [JsonProperty("tipodocumento")]
        public string TipoDocumento { get; set; }
        #endregion
    }
}
