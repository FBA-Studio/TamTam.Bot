using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TamTam.Bot.Types
{
    public class Dialog {
        public long UserId;
        public string Name;
        public string? Username;
        public bool IsBot;

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime LastActivityTime;
        public string? Description;
        public string AvatarUrl;
        public string FullAvatarUrl;
    }
}
