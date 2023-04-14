using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TamTam.Bot.Types.Enums;

namespace TamTam.Bot.Types{

    public class UpdateRaw {
        public UpdateType UpdateType;

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime TimeStamp;

        public Message? Message;
        public Callback? Callback;
        public User? User;
        public string? MessageId;
        public long? ChatId;
        public long? UserId;
        public string? UserLocale;
        public bool? IsChannel;
        public long? InviterId;
    }
}
