using System;

namespace TamTam.Bot.Types
{
    public class MessageBody
    {
        public string Mid;
        public long Seq;
        public string? Text;
        public Attachment[]? Attachments;
        public Markup[]? Markup;
    }
}
