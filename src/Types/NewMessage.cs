using System;
using TamTam.Bot.Types.Enums;

namespace TamTam.Bot.Types {
    public class NewMessage {
        public string? Text;
        public Attachment[]? Attachments;
        public Link? Link;
        public bool Notify;
        public Format Format;
    }
}