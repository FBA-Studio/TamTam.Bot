using System;

namespace TamTam.Bot.Types.Updates{
    public class MessageCreated {
        public DateTime TimeStamp;
        public Message Message;
        public string UserLocale;
    }
}