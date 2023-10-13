using System.Collections.Generic;
using TamTam.Bot.Types.Enums;

namespace TamTam.Bot.Types {
    public class CallbackAnswerParams {
        public string? Text;
        public List<AttachmentFile>? Files;
        public List<Attachment> Attachments;
        public Link Link;
        public bool? Notify;
        public Format? Format;
    }
}