using System;

namespace TamTam.Bot.Types
{
    public class MessageBody
    {
        public string Mid { get; set; }
        public long Seq { get; set; }
        public string? Text { get; set; }
        public Attachment[]? Attachments { get; set; }
        public Markup[]? Markup { get; set; }
    }
}
