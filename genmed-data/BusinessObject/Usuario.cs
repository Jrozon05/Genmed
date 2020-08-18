using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reumed.Data.BusinessObjects
{
    public class Usuario : RecordBase<Usuario>
    {
        #region Constructor
        public Usuario() : base()
        {
            base.SetInitialState();
        }

        #endregion

        #region Properties

        [JsonProperty("usuarioid")]
        public int UsuarioId { get; set; }

        [JsonProperty("guid")]
        public Guid Guid { get; set; } = Guid.NewGuid();

        [JsonProperty("nombreusuario")]
        public string NombreUsuario { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("clave")]
        public string Clave { get; set; }

        [JsonProperty("clavehash")]
        public byte[] ClaveHash { get; set; }

        [JsonProperty("clavesalt")]
        public byte[] ClaveSalt { get; set; }
        
        [JsonProperty("rol")]
        public Rol Rol { get; set; }

        [JsonProperty("utlimaactividad", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime UltimaActividad { get; set; }

        #endregion
    }
}
