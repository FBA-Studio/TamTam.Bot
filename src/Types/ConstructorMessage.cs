using System.Collections.Generic;
using TamTam.Bot.Types.Enums;

namespace TamTam.Bot.Types {
    public class ConstructorMessage {
        public string Text;
        public IEnumerable<Attachment> Attachments;
        public IEnumerable<Markup> Markup;
        public Format  Format;
    }
}