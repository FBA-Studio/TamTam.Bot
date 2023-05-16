using System;

namespace TamTam.Bot.Types.Updates {
    public class MessageRemoved {
        public DateTime TimeStamp;
        public string MessageId;
        public long ChatId;
        public long UserId;
    }
}
