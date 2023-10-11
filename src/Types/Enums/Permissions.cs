using System;

namespace TamTam.Bot.Types.Enums {
    [Flags]
    public enum Permissions {
        ReadAllMessages,
        AddRemoveMembers,
        AddAdmins,
        ChangeChatInfo,
        PinMessage,
        Write
    }
}