using System;
using Newtonsoft.Json;

namespace TamTam.Bot.Types.Updates {
    public class MessageConstructed {
        [JsonConverter(typeof(MillisecondEpochConverter))]
        public DateTime TimeStamp;

        public string SessionId;
        public Message Message;
    }
}