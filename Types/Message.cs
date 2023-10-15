using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TamTam.Bot.Types
{
    public class Message {
        [JsonProperty("sender")]
        public User Sender { get; set; }
        
        [JsonProperty("recepient")]
        public Recipient Recipient { get; set; }
        
        [JsonConverter(typeof(MillisecondEpochConverter))]
        [JsonProperty("timestamp")]
        public DateTime TimeStamp { get; set; }
        
        [JsonProperty("text")]
        public string Text { get; set; }
        
        [JsonProperty("link")]
        public Link? Link { get; set; }
        
        [JsonProperty("body")]
        public MessageBody Body { get; set; }
        
        [JsonProperty("stat")]
        public Stat? Stat { get; set; }
        
        [JsonProperty("url")]
        public string? Url { get; set; }
        
        [JsonProperty("constructor")]
        public User Constructor { get; set; }
    }
}