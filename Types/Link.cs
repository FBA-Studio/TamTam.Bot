using Newtonsoft.Json;
using TamTam.Bot.Types.Enums;

namespace TamTam.Bot.Types
{
    public class Link {
        [JsonProperty("type")]
        public LinkType Type { get; set; }
        
        [JsonProperty("sender")]
        public User Sender { get; set; }
        
        [JsonProperty("char_id")]
        public long ChatId { get; set; }
        
        [JsonProperty("message")]
        public MessageBody Message { get; set; }
    }
}