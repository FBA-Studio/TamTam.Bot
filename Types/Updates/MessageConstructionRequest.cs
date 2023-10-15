using System;
using Newtonsoft.Json;

namespace TamTam.Bot.Types.Updates {
    public class MessageConstructionRequest {
        [JsonConverter(typeof(MillisecondEpochConverter))]
        public DateTime TimeStamp { get; set; }

        public User User { get; set; }
        public string? UserLocale { get; set; }
        public string SessionId { get; set; }
        public string? Data { get; set; }
        public Input Input { get; set; }
    }
}