using System;

namespace TamTam.Bot.Types.Updates {
    public class ChatTitleChanged {
        public DateTime TimeStamp;
        public long ChatId;
        public User User;
        public string Title;
    }
}