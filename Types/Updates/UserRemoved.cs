using System;

namespace TamTam.Bot.Types.Updates {
    public class UserRemoved {
        public DateTime TimeStamp;
        public long ChatId;
        public User User;
        public long AdminId;
        public bool IsChannel;
    }
}