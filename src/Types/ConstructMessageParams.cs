using System.Collections;
using System.Collections.Generic;
using TamTam.Bot.Types.Enums;

namespace TamTam.Bot.Types {
    public class ConstructMessageParams
    {
        public struct ConstructMessage {
            /// <summary>
            /// Text/caption for your message
            /// </summary>
            public string? Text;
            
            /// <summary>
            /// Your array of other attachments(Sticker, Location, Contact, etc.)
            /// </summary>
            public IEnumerable<Attachment>? Attachments;
            
            /// <summary>
            /// Your array of files/medias(File, Video, Audio, Image)
            /// </summary>
            public IEnumerable<AttachmentFile>? Files;
            
            public IEnumerable<Markup>? Markup;
            
            /// <summary>
            /// Text format(Markdown, HTML)
            /// </summary>
            public Format? Format;
        }
        
        /// <summary>
        /// Array of construct messages
        /// </summary>
        public IEnumerable<ConstructMessage>? Messages;
        
        /// <summary>
        /// <b>true</b>, if you want allow user input
        /// </summary>
        public bool AllowUserInput;
        
        /// <summary>
        /// Hint to user. Will be shown on top of keyboard
        /// </summary>
        public string? Hint;
        
        /// <summary>
        /// In this property you can store any additional data up to 8KB.
        /// We send this data back to bot within the next construction request.
        /// It is handy to store here any state of construction session
        /// </summary>
        public string? Data;
        
        /// <summary>
        /// Inline keyboard markup
        /// </summary>
        public IEnumerable<IEnumerable<InlineKeyboardButton>>? Keyboard;
        
        /// <summary>
        /// Text to show over the text field
        /// </summary>
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