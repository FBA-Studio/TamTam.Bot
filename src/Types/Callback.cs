using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TamTam.Bot.Types {
    public class Callback {
        [JsonConverter(typeof(MillisecondEpochConverter))]
        public DateTime TimeStamp { get; set; }
        
        public string CallbackId { get; set; }
        public string Payload { get; set; }
        public User User { get; set; }
    }
}
