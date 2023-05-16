using System;
using TamTam.Bot.Types.Enums;
using TamTam.Bot.Types.Updates;

namespace TamTam.Bot.Types {
    public class Update {
        public UpdateType UpdateType;

        #nullable enable
        public BotAdded? BotAdded;

        #nullable enable
        public BotRemoved? BotRemoved;

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
    }
}