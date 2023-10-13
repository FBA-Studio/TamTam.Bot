using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TamTam.Bot.Types
{
    public class Message {
        public User Sender { get; set; }
        public Recipient Recipient { get; set; }
        
        [JsonConverter(typeof(MillisecondEpochConverter))]
        public DateTime TimeStamp { get; set; }
        
        public string Text { get; set; }
        public Link? Link { get; set; }
        public MessageBody Body { get; set; }
        public Stat? Stat { get; set; }
        public string? Url { get; set; }
        public User Constructor { get; set; }
    }
}