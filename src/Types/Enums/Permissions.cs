using System;
using System.Runtime.Serialization;

namespace TamTam.Bot.Types.Enums {
    [Flags]
    public enum Permissions {
        [EnumMember(Value = "read_all_messages")]
        ReadAllMessages,
        
        [EnumMember(Value = "add_remove_members")]
        AddRemoveMembers,
        
        [EnumMember(Value = "add_admins")]
        AddAdmins,
        
        [EnumMember(Value = "change_chat_info")]
        ChangeChatInfo,
        
        [EnumMember(Value = "pin_message")]
        PinMessage,
        
        [EnumMember(Value = "write")]
        Write
    }
}