using System.Collections.Generic;
using TamTam.Bot.Types.Enums;

namespace TamTam.Bot.Types {
    public class SendMessageParams {
        /// <summary>
        /// Text/caption for your message
        /// </summary>
        public string? Text;
        
        /// <summary>
        /// Your array of files/medias(File, Video, Audio, Image)
        /// </summary>
        public IEnumerable<AttachmentFile>? Files;
        
        /// <summary>
        /// Your array of other attachments(Sticker, Location, Contact, etc.)
        /// </summary>
        public IEnumerable<Attachment>? Attachments;
        
        public Link? Link;
        
        /// <summary>
        /// <b>true</b>, if you want notify after sending
        /// </summary>
        public bool Notify;
        
        /// <summary>
        /// Text format(Markdown, HTML)
        /// </summary>
        public Format? Format;

        public SendMessageParams() {
            Notify = true;
        }

        public Dictionary<string, dynamic> ToPostData() {
            var data = new Dictionary<string, dynamic>() { {"notify", Notify.ToString().ToLower()} };
            if (!string.IsNullOrEmpty(Text))
                data.Add("text", Text);
            if (Link != null)
                data.Add("link", Link);
            if (Format != null)
                data.Add("format", Format);
            return data;
        }
    }
}