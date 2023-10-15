using System;
using TamTam.Bot.Types.Enums;
using TamTam.Bot.Types.Updates;

namespace TamTam.Bot.Types {
    public class Update {
        public UpdateType UpdateType;
        public string JsonRaw; 
            
        #nullable enable
        public BotAdded? BotAdded;

        #nullable enable
        public BotRemoved? BotRemoved;

        #nullable enable
        public BotStarted? BotStarted;

        #nullable enable
        public ChatTitleChanged? ChatTitleChanged;

        #nullable enable
        public MessageCallback? MessageCallback;

        #nullable enable
        public MessageCreated? MessageCreated;

        #nullable enable
        public MessageEdited? MessageEdited;

        #nullable enable
        public MessageRemoved? MessageRemoved;

        #nullable enable
        public UserAdded? UserAdded;

        #nullable enable
        public UserRemoved? UserRemoved;
        
        #nullable enable
        public MessageConstructionRequest? MessageConstructionRequest;
        
        #nullable enable
        public MessageConstructed? MessageConstructed;
        
        #nullable enable
        public MessageChatCreated? MessageChatCreated;
    }
}