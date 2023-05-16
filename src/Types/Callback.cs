using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TamTam.Bot.Types {
    public class Callback {
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime TimeStamp;
        public string CallbackId;
        public string Payload;
        public User User;
    }
}
