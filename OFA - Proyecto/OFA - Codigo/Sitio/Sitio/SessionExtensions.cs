using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Sitio.Models;
using System;
using System.Net.Http;

namespace Sitio
{
    public static class SessionExtensions
    {

        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            try
            {
                session.SetString(key, JsonConvert.SerializeObject(value));
            }
            catch (Exception ex) 
            { 
                throw new Exception(ex.Message);
            }
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            try
            {
                var value = session.GetString(key);
                return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
