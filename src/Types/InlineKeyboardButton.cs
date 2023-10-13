using TamTam.Bot.Types.Enums;

namespace TamTam.Bot.Types {
    public class InlineKeyboardButton {
        public InlineButtonType Type { get; set; }
        public string Text { get; set; }
        public string Payload { get; set; }
        public CallbackIntentType? Intent { get; set; }

        public string Url { get; set; }
        
        public bool? Quick { get; set; }

        public string ChatTitle { get; set; }
        public string? ChatDescription { get; set; }
        public string? StartPayload { get; set; }
        public int? Uuid { get; set; }

        public static InlineKeyboardButton ToCallback(string text, string payload, CallbackIntentType intent) {
            return new InlineKeyboardButton() {
                Text = text, Payload = payload, Intent = intent, Type = InlineButtonType.Callback
            };
        }
        public static InlineKeyboardButton ToLink(string text, string url) {
            return new InlineKeyboardButton() {
                Text = text, Url = url, Type = InlineButtonType.Link
            };
        }
        public static InlineKeyboardButton ToRequestContact(string text) {
            return new InlineKeyboardButton() {
                Text = text, Type = InlineButtonType.RequestContact
            };
        }
        public static InlineKeyboardButton ToRequestGeoLocation(string text, bool? quick = null) {
            return new InlineKeyboardButton() {
                Text = text, Quick = quick, Type = InlineButtonType.RequestGeoLocation
            };
        }
        public static InlineKeyboardButton ToChat(string text, string chatTitle, string? chatDescription = null,
            string? startPayload = null, int? uuid = null) {
            return new InlineKeyboardButton() {
                Text = text, ChatTitle = chatTitle, ChatDescription = chatDescription, StartPayload = startPayload, Uuid = uuid, Type = InlineButtonType.Chat
            };
        }
    }
}