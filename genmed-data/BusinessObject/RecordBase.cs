using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Reumed.DataAccess.BusinessObjects
{
    public abstract class RecordBase<T>
    {
        string _Orgvalue = string.Empty;

        [JsonIgnore()]
        public string CreatedBy { get; set; }

        [JsonIgnore()]
        public string ModifiedBy { get; set; }

        [JsonIgnore()]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [JsonIgnore()]
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        [JsonProperty("activo")]
        public bool Activo { get; set; }

        [JsonIgnore()]
        public bool HasChanges
        {
            get { return string.Compare(_Orgvalue, GetCurrentState()) != 0; }
        }

        ///<summary>
        /// Used to initialize the 'saved' or 'initial' state of the object.
        /// This routine should be called anytime the object's contents are saved or when the object's values are initially loaded or set.
        /// </summary>
        public void SetInitialState()
        {
            _Orgvalue = GetCurrentState();
        }

        /// <summary>
        /// Get the currrent state of the object
        /// </summary>
        private string GetCurrentState()
        {
            JsonSerializerSettings jsonset = new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.Include,
                Error = (object sender, ErrorEventArgs e) => { e.ErrorContext.Handled = true; },
                NullValueHandling = NullValueHandling.Include
            };

            return JsonConvert.SerializeObject(this, jsonset);
        }
    }
}
