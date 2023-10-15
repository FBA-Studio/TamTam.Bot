using System;

namespace TamTam.Bot.Types.Updates {
    public class UserAdded {
        public DateTime TimeStamp;
        public long ChatId;
        public User User;
        public long InviterId;
        public bool IsChannel;
    }
}