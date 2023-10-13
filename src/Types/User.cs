using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TamTam.Bot.Types.Enums;

namespace TamTam.Bot.Types
{
    public class User {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string? Username { get; set; }
        public bool IsBot { get; set; }

        [JsonConverter(typeof(MillisecondEpochConverter))]
        public DateTime LastActivityTime { get; set; }

        public string? Description { get; set; }
        public string? AvatarUrl { get; set; }
        public string? FullAvatarUrl { get; set; }
        public bool IsOwner { get; set; }
        public bool IsAdmin { get; set; }
        
        [JsonConverter(typeof(MillisecondEpochConverter))]
        public DateTime JoinTime { get; set; }

        public Permissions Permissions { get; set; }
        public BotCommand[] Commands { get; set; }
    }
}