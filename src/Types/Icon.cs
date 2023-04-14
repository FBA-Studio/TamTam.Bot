using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TamTam.Bot.Types {
    public class Icon
    {
        public string Url;

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime LastUpdatedTime;
    }
}