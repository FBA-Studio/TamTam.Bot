using System;
using Newtonsoft.Json;
using TamTam.Bot.Types;

namespace TamTam.Bot.Types.Attachemnts {
    public class Share : Attachment {
        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
    }
}
