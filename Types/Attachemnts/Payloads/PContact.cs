using System;
using Newtonsoft.Json;
using TamTam.Bot.Types;

namespace TamTam.Bot.Types.Attachemnts.Payloads {
    public class PContact : Payload {
        [JsonProperty("vcf_info")]
        public string VcfInfo;
        
        [JsonProperty("tam_info")]
        public User TamInfo;
    }
}