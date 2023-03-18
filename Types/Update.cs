using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TamTam.Bot.Types.Enums;

namespace TamTam.Bot.Types{

    public class Update {
        public UpdateType UpdateType;

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime TimeStamp;
        
    }
}