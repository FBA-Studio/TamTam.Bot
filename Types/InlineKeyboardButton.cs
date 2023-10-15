using Newtonsoft.Json;
using TamTam.Bot.Types.Enums;

namespace TamTam.Bot.Types {
    public class InlineKeyboardButton {
        [JsonProperty("type")]
        public InlineButtonType Type { get; set; }
        
        [JsonProperty("text")]
        public string Text { get; set; }
        
        [JsonProperty("payload")]
        public string Payload { get; set; }
        
        [JsonProperty("intent")]
        public CallbackIntentType? Intent { get; set; }
        
        [JsonProperty("url")]
        public string Url { get; set; }
        
        [JsonProperty("quick")]
        public bool? Quick { get; set; }

        [JsonProperty("chat_title")]
        public string ChatTitle { get; set; }
        
        [JsonProperty("chat_description")]
        public string? ChatDescription { get; set; }
        
        [JsonProperty("start_payload")]
        public string? StartPayload { get; set; }
        
        [JsonProperty("uuid")]
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