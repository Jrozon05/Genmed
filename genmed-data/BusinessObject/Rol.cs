﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reumed.Data.BusinessObjects
{
    public class Rol : RecordBase<Rol>
    {
        #region Constructor
        public Rol() : base()
        {
            SetInitialState();
        }
        #endregion

        #region Properties

        [JsonProperty("rolid")]
        public int RolId { get; set; }

        [JsonProperty("nombrerol")]
        public string Nombre { get; set; }
        #endregion
    }
}
