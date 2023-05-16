using System;

namespace TamTam.Bot.Types.Updates{
    public class BotStarted{
        public DateTime TimeStamp;
        public long ChatId;
        public User User;
        public Payload Payload;
        public string UserLocale;
    }
}