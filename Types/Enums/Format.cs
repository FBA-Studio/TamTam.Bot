using System.Runtime.Serialization;

namespace TamTam.Bot.Types.Enums{

    public enum Format {
        [EnumMember(Value = "markdown")]
        Markdown, 
        
        [EnumMember(Value = "html")]
        Html
    }
}