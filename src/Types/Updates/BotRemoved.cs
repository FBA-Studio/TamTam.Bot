using System;

namespace TamTam.Bot.Types.Updates {
    public class BotRemoved {
        public DateTime TimeStamp;
        public long ChatId;
        public User User;
        public bool IsChannel;
    }
}