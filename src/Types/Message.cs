using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TamTam.Bot.Types
{
    public class Message {
        public User Sender;
        public Recipient Recipient;

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime TimeStamp;
        public string Text;
        public Link? Link;
        public MessageBody Body;
        public Stat? Stat;
        public string? Url;
        public User Constructor;
    }
}