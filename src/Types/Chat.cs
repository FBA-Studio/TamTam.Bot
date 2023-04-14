using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TamTam.Bot.Types.Enums;

namespace TamTam.Bot.Types
{
    public class Chat {
        public long ChatId;
        public ChatType Type;
        public Status Status;
        public string? Title;
        public Icon? Icon;

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime LastEventTime;
        public int ParticipantsCount;
        public long? OwnerId;
        public ChatParticipant[] Participants;
        public bool IsPublic;
        public string? Link;
        public string? Description;
        public Dialog? DialogWithUser;
        public int? MessagesCount;
        public string? ChatMessageId;
    }
}
