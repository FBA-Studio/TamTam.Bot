using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TamTam.Bot.Types {
    public class Payload {
        [JsonProperty("url")]
        public string Url { get; set; }
        
        [JsonProperty("token")]
        public string Token { get; set; }
        
        [JsonProperty("buttons")]
        public List<List<InlineKeyboardButton>> Buttons { get; set; }
        
        [JsonProperty("code")]
        public string Code { get; set; }
        
        [JsonProperty("vcf_info")]
        public string VcfInfo;
        
        [JsonProperty("tam_info")]
        public User TamInfo;
        
        [JsonProperty("photo_id")]
        public string PhotoId;
    }
}