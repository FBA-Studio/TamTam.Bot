using Newtonsoft.Json;
using TamTam.Bot.Types.Enums;

namespace TamTam.Bot.Types
{
    public class Recipient {
        [JsonProperty("chat_id")]
        public long? ChatId { get; set; }
        
        [JsonProperty("chat_type")]
        public ChatType ChatType { get; set; }
        
        [JsonProperty("user_id")]
        public long? UserId { get; set; }
    }
}