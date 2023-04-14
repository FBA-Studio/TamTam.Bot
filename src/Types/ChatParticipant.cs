using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TamTam.Bot.Types {
    public class ChatParticipant {
        public long UserId;
        public string Name;
        public string Username;
        public bool IsOwner { get; set; }
        public bool IsAdmin { get; set; }

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime JoinTime { get; set; }
        public string AvatarUrl { get; set; }
        public string Locale { get; set; }
    }
}