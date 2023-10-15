using System;
using Newtonsoft.Json;
using TamTam.Bot.Types;

namespace TamTam.Bot.Types.Attachemnts {
    public class File : Attachment {
        [JsonProperty("file_name")]
        public string FileName;
        
        [JsonProperty("size")]
        public long Size;
    }
}
