using Newtonsoft.Json;
using TamTam.Bot.Types.Enums;

namespace TamTam.Bot.Types {
    public class Input {
        [JsonProperty("input_type")]
        public InputType InputType;
        
        [JsonProperty("messages")]
        public Message[]? Messages;
        
        [JsonProperty("payload")]
        public Payload? Payload;
    }
}