using System.Collections;
using System.Collections.Generic;
using TamTam.Bot.Types.Enums;

namespace TamTam.Bot.Types {
    public class ConstructMessageParams
    {
        public struct ConstructMessage {
            public string? Text;
            public IEnumerable<Attachment>? Attachments;
            public IEnumerable<AttachmentFile>? Files;
            public IEnumerable<Markup>? Markup;
            public Format? Format;
        }

        public IEnumerable<ConstructMessage>? Messages;
        public bool AllowUserInput;
        public string? Hint;
        public string? Data;
        public IEnumerable<IEnumerable<InlineKeyboardButton>>? Keyboard;
        public string? Placeholder;

        public Dictionary<string, dynamic> ToPostData() {
            var data = new Dictionary<string, dynamic>() { {"allow_user_input", AllowUserInput} };
            
            if (Data != null)
                data.Add("data", Data);
            if (Messages != null)
                data.Add("messages", Messages);
            if (Keyboard != null)
                data.Add("keyboard", Keyboard);
            if (Hint != null)
                data.Add("hint", Hint);
            if (Placeholder != null)
                data.Add("placeholder", Placeholder);

            return data;
        }
    }
}