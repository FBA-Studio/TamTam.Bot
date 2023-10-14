using System;
using Newtonsoft.Json;

namespace TamTam.Bot.Types.Updates {
    public class MessageChatCreated {
        [JsonConverter(typeof(MillisecondEpochConverter))]
        public DateTime TimeStamp { get; set; }

        public Chat Chat { get; set; }
        public string MessageId { get; set; }
        public string? StartPayload { get; set; }
    }
}