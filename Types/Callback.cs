using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TamTam.Bot.Types {
    public class Callback {
        [JsonConverter(typeof(MillisecondEpochConverter))]
        [JsonProperty("timestamp")]
        public DateTime TimeStamp { get; set; }
        
        [JsonProperty("callback_id")]
        public string CallbackId { get; set; }
        
        [JsonProperty("payload")]
        public string Payload { get; set; }
        
        [JsonProperty("user")]
        public User User { get; set; }
    }
}
