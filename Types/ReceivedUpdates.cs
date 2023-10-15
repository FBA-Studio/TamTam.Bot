using System;

namespace TamTam.Bot.Types {
    public class ReceivedUpdates {
        public UpdateRaw[] Updates;
        public long Marker;
        public string JsonRaw;
    }
}
