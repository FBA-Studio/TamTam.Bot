using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

using TamTam.Bot.Types;
using TamTam.Bot.Types.Enums;
using TamTam.Bot.Types.Updates;

namespace TamTam.Bot
{
    public class TamTamClient {
        private static readonly string header = "application/json";
        private static readonly string api_url = "https://botapi.tamtam.chat";
        private static string Token = "access_token=token";
        private static int Limit = 100;
        private static int Timeout = 30;
        private static long Marker = 0;

        public TamTamClient(string token, int limit = 100, int timeout = 30) {
            Token = "access_token=" + token;
            Limit = limit;
            Timeout = timeout;
            Marker = 0;
        }

        public async void StartPolling(Func<Update, Task> updateHandler, UpdateType[] allowedUpdates = null) {
            while(true) {
                var updates = await GetUpdates();
                Marker = Math.Max(Marker, updates.Marker);
                foreach (var update in updates.Updates) {
                    if(allowedUpdates == null) {
                        var upd = ParseRawUpdate(update);
                        await updateHandler.Invoke(upd);
                    }
                    else {
                        foreach(var allwUpdate in allowedUpdates) {
                            if(allwUpdate == update.UpdateType) {
                                var upd = ParseRawUpdate(update);
                                await updateHandler.Invoke(upd);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private Update ParseRawUpdate(UpdateRaw raw) {
            var update = new Update() { UpdateType = raw.UpdateType };
            switch (raw.UpdateType)
            {
                case UpdateType.BotAdded:
                    update.BotAdded = new BotAdded() { TimeStamp = raw.TimeStamp, ChatId = raw.ChatId.Value, User = raw.User, IsChannel = raw.IsChannel.Value };
                    break;
                case UpdateType.UserAdded:
                    update.UserAdded = new UserAdded() { TimeStamp = raw.TimeStamp, ChatId = raw.ChatId.Value, User = raw.User, InviterId = raw.InviterId.Value, IsChannel = raw.IsChannel.Value };
                    break;
                case UpdateType.BotRemoved:
                    update.BotRemoved = new BotRemoved() { TimeStamp = raw.TimeStamp, ChatId = raw.ChatId.Value, User = raw.User, IsChannel = raw.IsChannel.Value };
                    break;
                case UpdateType.MessageCallback:
                    update.MessageCallback = new MessageCallback() { TimeStamp = raw.TimeStamp, Callback = raw.Callback, Message = raw.Message, UserLocale = raw.UserLocale };
                    break;
                case UpdateType.MessageCreated:
                    update.MessageCreated = new MessageCreated() { TimeStamp = raw.TimeStamp, Message = raw.Message, UserLocale = raw.UserLocale };
                    break;
                case UpdateType.MessageRemoved:
                    update.MessageRemoved = new MessageRemoved() { TimeStamp = raw.TimeStamp, MessageId = raw.MessageId, ChatId = raw.ChatId.Value, UserId = raw.UserId.Value};
                    break;
                case UpdateType.MessageEdited:
                    update.MessageEdited = new MessageEdited() { TimeStamp = raw.TimeStamp, Message = raw.Message };
                    break;
                case UpdateType.UserRemoved:
                    update.UserRemoved = new UserRemoved() { TimeStamp = raw.TimeStamp, ChatId = raw.ChatId.Value, User = raw.User, AdminId = raw.AdminId.Value, IsChannel = raw.IsChannel.Value };
                    break;
                case UpdateType.BotStarted:
                    update.BotStarted = new BotStarted() { TimeStamp = raw.TimeStamp, ChatId = raw.ChatId.Value, User = raw.User, Payload = raw.Payload, UserLocale = raw.UserLocale };
                    break;
                case UpdateType.ChatTitleChanged:
                    update.ChatTitleChanged = new ChatTitleChanged() { TimeStamp = raw.TimeStamp, ChatId = raw.ChatId.Value, };
                    break;
                case UpdateType.MessageConstructionRequest:
                    break;
            }
            return update;
        }

        private async Task<ReceivedUpdates> GetUpdates() {
            try {
                var response = api_url + "/" + "updates" + "?" + Token + "&limit=" + Limit + "&timeout=" + Timeout;
                if(Marker > 0) {
                    response += "&marker=" + Marker;
                }
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(response);
                httpWebRequest.Method ="GET";
                httpWebRequest.ContentType = header;

                using(HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse()) {
                    using Stream stream = httpWebResponse.GetResponseStream();
                    using StreamReader streamReader = new StreamReader(stream);
                    return (JArray.Parse(await streamReader.ReadToEndAsync())).ToObject<ReceivedUpdates>();
                }
            }
            catch(Exception exc) {
                return null;
            }
        }
        private async Task<string> MakeRequest(string method, string urlMethod, Dictionary<string, dynamic> args = null) {
            try {
                var response = api_url + "/" + urlMethod + "?" + Token;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(response);
                httpWebRequest.Method = method;
                httpWebRequest.ContentType = header;
                
                if(args != null) {
                    var jsonData = JsonConvert.SerializeObject(args);
                    httpWebRequest.ContentLength = jsonData.Length;

                    using (StreamWriter writer = new StreamWriter(await httpWebRequest.GetRequestStreamAsync())) {
                        await writer.WriteAsync(jsonData);
                        await writer.FlushAsync();
                    }
                }

                using (HttpWebResponse httpWebResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync()) {
                    using Stream stream = httpWebResponse.GetResponseStream();
                    using StreamReader streamReader = new StreamReader(stream);
                    return await streamReader.ReadToEndAsync();
                }
            }
            catch(Exception exc) {
                return null;
            }
        }
        
        public async Task<User> GetMeAsync() {
            var response = await MakeRequest("GET", "me");
            return JObject.Parse(response).ToObject<User>();
        }
        public async Task<ChatsList> GetChatsAsync(int count = 50, long? marker = null) {
            var response = await MakeRequest("GET", "chats");
            return JObject.Parse(response).ToObject<ChatsList>();
        }
        public async Task<Chat> GetChatByLinkAsync(string chatLink) {
            var response = await MakeRequest("GET", $"chats/{chatLink}");
            return JObject.Parse(response).ToObject<Chat>();
        }
        public async Task<Chat> GetChatAsync(long chatId) {
            var response = await MakeRequest("GET", $"chats/{chatId}");
            return JObject.Parse(response).ToObject<Chat>();
        }
        public async Task<Chat?> EditChatInfoAsync(long chatId, Icon? icon = null, string? title = null, string? pin = null,
            bool? notify = null) {
            Dictionary<string, dynamic> args = new Dictionary<string, dynamic>();
            if (icon != null)
                args.Add("icon", icon);
            if (!string.IsNullOrEmpty(title))
                args.Add("title", title);
            if (!string.IsNullOrEmpty(pin))
                args.Add("pin", pin);
            if (notify != null)
                args.Add("notify", notify);

            return args.Count > 0 ? JsonConvert.DeserializeObject<Chat>(await MakeRequest("PATCH", $"chats/{chatId}", args)) : null;
        }
        public async Task<RequestStatus> SendActionAsync(long chatId, ActionType action) {
            var args = new Dictionary<string, dynamic>() { { "action", action } };
            return JsonConvert.DeserializeObject<RequestStatus>(await MakeRequest("POST", $"chats/{chatId}/actions", args));
        }
        public async Task<Message> GetPinnedMessageAsync(long chatId) {
            return JsonConvert.DeserializeObject<Message>(await MakeRequest("GET", $"chats/{chatId}/pin"));
        }
        public async Task<RequestStatus> PinMessageAsync(long chatId, string messageId, bool? notify = null) {
            var args = new Dictionary<string, dynamic>() { {"message_id", messageId} };
            if (notify != null)
                args.Add("notify", notify);
            return JsonConvert.DeserializeObject<RequestStatus>(await MakeRequest("PUT", $"chats/{chatId}/pin", args));
        }
        public async Task<RequestStatus> UnpinMessageAsync(long chatId) {
            return JsonConvert.DeserializeObject<RequestStatus>(await MakeRequest("DELETE", $"chats/{chatId}/pin"));
        }
        public async Task<User> GetChatMembershipAsync(long chatId) {
            return JsonConvert.DeserializeObject<User>(await MakeRequest("GET", $"chats/{chatId}/members/me"));
        }
        public async Task<RequestStatus> LeaveChatAsync(long chatId) {
            return JsonConvert.DeserializeObject<RequestStatus>(await MakeRequest("DELETE", $"chats/{chatId}/members/me"));
        }
    }
}