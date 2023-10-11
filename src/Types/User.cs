using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TamTam.Bot.Types.Enums;

namespace TamTam.Bot.Types
{
    public class User {
        public long UserId;
        public string Name;
        public string? Username;
        public bool IsBot;

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime LastActivityTime;

        public string? Description;
        public string? AvatarUrl;
        public string? FullAvatarUrl;
        public bool IsOwner;
        public bool IsAdmin;
        
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime JoinTime;

        public Permissions Permissions;
        public BotCommand[] Commands;
    }
}