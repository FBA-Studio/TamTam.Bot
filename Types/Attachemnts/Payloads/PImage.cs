using System;
using Newtonsoft.Json;
using TamTam.Bot.Types;

namespace TamTam.Bot.Types.Attachemnts.Payloads {
    public class PImage : Payload {
        [JsonProperty("photo_id")]
        public string PhotoId;
    }
}
