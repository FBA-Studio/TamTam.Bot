using Newtonsoft.Json;

namespace TamTam.Bot.Types {
    public class BotCommand {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("description")]
        public string? Description { get; set; }
    }
}