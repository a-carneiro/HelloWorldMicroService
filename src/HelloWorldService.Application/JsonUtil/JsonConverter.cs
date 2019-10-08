using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorldService.Application.JsonUtil
{
    public static class JsonConverter
    {
        public static string Serialize<T>(T value)
        {
            var result = JsonConvert.SerializeObject(value);
            return result;
        }

        public static T Desserialized<T>(string value)
        {
            var result = JsonConvert.DeserializeObject<T>(value);
            return result;
        }
    }
}
