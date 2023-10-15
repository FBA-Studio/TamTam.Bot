using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TamTam.Bot.Types.Enums;

namespace TamTam.Bot.Types
{
    public class User {
        [JsonProperty("user_id")]
        public long UserId { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("username")]
        public string? Username { get; set; }
        
        [JsonProperty("is_bot")]
        public bool IsBot { get; set; }

        [JsonConverter(typeof(MillisecondEpochConverter))]
        [JsonProperty("last_activity_time")]
        public DateTime LastActivityTime { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }
        
        [JsonProperty("avatar_url")]
        public string? AvatarUrl { get; set; }
        
        [JsonProperty("full_avatar_url")]
        public string? FullAvatarUrl { get; set; }
        
        [JsonProperty("is_owner")]
        public bool IsOwner { get; set; }
        
        [JsonProperty("is_admin")]
        public bool IsAdmin { get; set; }
        
        [JsonConverter(typeof(MillisecondEpochConverter))]
        [JsonProperty("join_time")]
        public DateTime JoinTime { get; set; }
        
        [JsonProperty("permissions")]
        public Permissions Permissions { get; set; }
        
        [JsonProperty("commands")]
        public BotCommand[] Commands { get; set; }
    }
}