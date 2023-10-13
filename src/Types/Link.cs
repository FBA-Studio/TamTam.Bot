using TamTam.Bot.Types.Enums;

namespace TamTam.Bot.Types
{
    public class Link {
        public LinkType Type { get; set; }
        public User Sender { get; set; }
        public long ChatId { get; set; }
        public MessageBody Message { get; set; }
    }
}