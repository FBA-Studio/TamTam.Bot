using TamTam.Bot.Types.Enums;

namespace TamTam.Bot.Types
{
    public class Recipient {
        public long? ChatId { get; set; }
        public ChatType ChatType { get; set; }
        public long? UserId { get; set; }
    }
}