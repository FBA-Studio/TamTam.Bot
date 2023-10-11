using System.Collections.Generic;
using TamTam.Bot.Types.Enums;

namespace TamTam.Bot.Types {
    public class SendMessageParams {
        public string? Text;
        public IEnumerable<AttachmentFile>? Attachments;
        public Link? Link;
        public bool Notify;
        public Format? Format;

        public SendMessageParams() {
            Notify = true;
        }

        public Dictionary<string, dynamic> ToPostData() {
            var data = new Dictionary<string, dynamic>() { {"notify", Notify} };
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