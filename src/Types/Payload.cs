using System;
using System.Collections.Generic;

namespace TamTam.Bot.Types {
    public class Payload {
        public string Url { get; set; }
        public string Token { get; set; }
        public List<List<InlineKeyboardButton>> Buttons { get; set; }
    }
}