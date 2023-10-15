using System.Runtime.Serialization;

namespace TamTam.Bot.Types.Enums{

    public enum UpdateType
    {
        [EnumMember(Value = "message_callback")]
        MessageCallback,
        
        [EnumMember(Value = "message_created")]
        MessageCreated,
        
        [EnumMember(Value = "message_removed")]
        MessageRemoved,
        
        [EnumMember(Value = "message_edited")]
        MessageEdited,
        
        [EnumMember(Value = "bot_added")]
        BotAdded,
        
        [EnumMember(Value = "bot_removed")]
        BotRemoved,
        
        [EnumMember(Value = "user_added")]
        UserAdded,
        
        [EnumMember(Value = "user_removed")]
        UserRemoved,
        
        [EnumMember(Value = "bot_started")]
        BotStarted,
        
        [EnumMember(Value = "chat_title_changed")]
        ChatTitleChanged,
        
        [EnumMember(Value = "message_construction_request")]
        MessageConstructionRequest,
        
        [EnumMember(Value = "message_constructed")]
        MessageConstructed,
        
        [EnumMember(Value = "message_chat_created")]
        MessageChatCreated
    }
}