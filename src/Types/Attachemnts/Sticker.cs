using System;
using Newtonsoft.Json;
using TamTam.Bot.Types;

namespace TamTam.Bot.Types.Attachemnts {
    public class Sticker : Attachment {
        [JsonProperty("width")]
        public int Width;
        
        [JsonProperty("height")]
        public int Height;
    }
}
