using TamTam.Bot.Types.Enums;

namespace TamTam.Bot.Types
{
    public class Link {
        public LinkType Type;
        public User Sender;
        public long ChatId;
        public MessageBody Message;
    }
}