using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace genmed_api.Utils.Extensions
{
    public static class IdentityExtension
    {
        public static void SetObjetoSesion(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjetoSesion<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}