using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using TamTam.Bot.Types;
using TamTam.Bot.Types.Enums;
using TamTam.Bot.Types.Updates;

namespace TamTam.Bot {
    public class TamTamClient {
        private static readonly string header = "application/json";
        private static readonly string api_url = "https://botapi.tamtam.chat";
        private static string Token = "access_token=token";
        private static string _token = "token";
        private static int Limit = 100;
        private static int Timeout = 30000;
        private static long? Marker = null;
        
        /// <summary>
        /// <b>Welcome to TamTam.Bot!</b>
        /// <code/> FAQ - https://github.com/FBA-Studio/TamTam.Bot/blob/main/FAQ.md 
        /// <code/> README - https://github.com/FBA-Studio/TamTam.Bot/tree/main#readme
        /// <code/> README-EN - https://github.com/FBA-Studio/TamTam.Bot/blob/main/README_EN.md
        /// </summary>
        /// <param name="token">Your bot token</param>
        /// <param name="limit">Limit for received updates. <code>Default: 100</code></param>
        /// <param name="timeout">Timeout in seconds for update receiving. <code>Default: 30</code></param>
        public TamTamClient(string token, int limit = 100, int timeout = 30) {
            Token = "access_token=" + token;
            _token = token;
            Limit = limit;
            Timeout = timeout * 1000;
            Marker = null;
        }
        /// <summary>
        /// Update Receiver Handler.
        /// </summary>
        /// <param name="updateHandler">Your function for update receiving, e.x. <code>static Task UpdateReceiver(TamTam.Bot.Types.Update update)</code></param>
        /// <param name="allowedUpdates">Your array of allowed updates. If <b>allowedUpdates</b> is null, you will receiving all update types. <code>Default: null</code></param>
        /// <param name="isBackground"><b>true</b>, if you want to run bot polling in background, but you must make a unreachable code, else your program will terminate. <code>Default: false</code></param>
        public void StartPolling(Func<Update, Task> updateHandler, UpdateType[] allowedUpdates = null) { 
            Thread polling = new Thread(() => Poller(updateHandler, allowedUpdates)) { Name = "Bot Polling", Priority = ThreadPriority.Normal, IsBackground = true};
            polling.Start();
        }

        private async void Poller(Func<Update, Task> updateHandler, UpdateType[] allowedUpdates = null) {
            while (true) {
                var updates = await GetUpdates(Marker);
                if (updates != null) {
                    Marker = Marker == null ? 0 : Marker;
                    Marker = Math.Max(Marker.Value, updates.Marker);
                    foreach (var update in updates.Updates) {
                        if (allowedUpdates == null) {
                            var upd = ParseRawUpdate(update, updates.JsonRaw);
                            await updateHandler.Invoke(upd);
                        }
                        else {
                            foreach (var allwUpdate in allowedUpdates)
                            {
                                if (allwUpdate == update.UpdateType)
                                {
                                    var upd = ParseRawUpdate(update, updates.JsonRaw);
                                    await updateHandler.Invoke(upd);
                                }
                            }
                        }
                    }
                }
            }
        }
        private Update ParseRawUpdate(UpdateRaw raw, string jsonRaw) {
            var update = new Update() { UpdateType = raw.UpdateType };
            switch (raw.UpdateType)
            {
                case UpdateType.BotAdded:
                    update.BotAdded = new BotAdded()
                    {
                        TimeStamp = raw.TimeStamp, ChatId = raw.ChatId.Value, 
                        User = raw.User, IsChannel = raw.IsChannel.Value
                    };
                    break;
                case UpdateType.UserAdded:
                    update.UserAdded = new UserAdded() { 
                        TimeStamp = raw.TimeStamp, ChatId = raw.ChatId.Value, 
                        User = raw.User, InviterId = raw.InviterId.Value, IsChannel = raw.IsChannel.Value };
                    break;
                case UpdateType.BotRemoved:
                    update.BotRemoved = new BotRemoved() { 
                        TimeStamp = raw.TimeStamp, ChatId = raw.ChatId.Value, 
                        User = raw.User, IsChannel = raw.IsChannel.Value };
                    break;
                case UpdateType.MessageCallback:
                    update.MessageCallback = new MessageCallback()
                    {
                        TimeStamp = raw.TimeStamp, Callback = raw.Callback, 
                        Message = raw.Message, UserLocale = raw.UserLocale
                    };
                    break;
                case UpdateType.MessageCreated:
                    update.MessageCreated = new MessageCreated() { 
                        TimeStamp = raw.TimeStamp, Message = raw.Message, 
                        UserLocale = raw.UserLocale };
                    break;
                case UpdateType.MessageRemoved:
                    update.MessageRemoved = new MessageRemoved()
                    {
                        TimeStamp = raw.TimeStamp, MessageId = raw.MessageId, ChatId = raw.ChatId.Value, 
                        UserId = raw.UserId.Value
                    };
                    break;
                case UpdateType.MessageEdited:
                    update.MessageEdited = new MessageEdited()
                    {
                        TimeStamp = raw.TimeStamp, Message = raw.Message
                    };
                    break;
                case UpdateType.UserRemoved:
                    update.UserRemoved = new UserRemoved() { 
                        TimeStamp = raw.TimeStamp, 
                        ChatId = raw.ChatId.Value, User = raw.User, AdminId = raw.AdminId.Value, 
                        IsChannel = raw.IsChannel.Value };
                    break;
                case UpdateType.BotStarted:
                    update.BotStarted = new BotStarted()
                    {
                        TimeStamp = raw.TimeStamp, ChatId = raw.ChatId.Value, User = raw.User, 
                        Payload = raw.Payload, UserLocale = raw.UserLocale
                    };
                    break;
                case UpdateType.ChatTitleChanged:
                    update.ChatTitleChanged = new ChatTitleChanged()
                    {
                        TimeStamp = raw.TimeStamp, ChatId = raw.ChatId.Value,
                    };
                    break;
                case UpdateType.MessageConstructionRequest:
                    update.MessageConstructionRequest = new MessageConstructionRequest()
                    {
                        TimeStamp = raw.TimeStamp, User = raw.User, UserLocale = raw.UserLocale, 
                        SessionId = raw.SessionId, Data = raw.Data, Input = raw.Input
                    };
                    break;
                case UpdateType.MessageConstructed:
                    update.MessageConstructed = new MessageConstructed()
                    {
                        TimeStamp = raw.TimeStamp, Message = raw.Message, SessionId = raw.SessionId
                    };
                    break;
                case UpdateType.MessageChatCreated:
                    update.MessageChatCreated = new MessageChatCreated()
                    {
                        TimeStamp = raw.TimeStamp, Chat = raw.Chat, MessageId = raw.MessageId,
                        StartPayload = raw.StartPayload
                    };
                    break;
            }
            update.JsonRaw = jsonRaw;
            return update;
        }
        private async Task<string> MakeRequest(string method, string urlMethod, Dictionary<string, dynamic> args = null, Dictionary<string, string> additionalParams = null) {
            try {
                var response = api_url + "/" + urlMethod + "?" + Token;
                
                if (additionalParams != null)
                    for (int i = 0; i < additionalParams.Count; i++)
                        response += "&" + additionalParams.Keys.ToArray()[i] + "=" +
                                    additionalParams.Values.ToArray()[i];
                
                using (HttpClient client = new HttpClient()) {
                    client.DefaultRequestHeaders.Add("Accept", header);
                    switch (method) {
                        case "GET": {
                            return await client.GetStringAsync(response);
                        }
                        case "POST": {
                            var data = JsonConvert.SerializeObject(args);
                            var content = new StringContent(data, Encoding.UTF8, header);
                            var result = await client.PostAsync(response, content);
                            return await result.Content.ReadAsStringAsync();
                        }
                        case "DELETE": {
                            var result = await client.DeleteAsync(response);
                            return await result.Content.ReadAsStringAsync();
                        }
                        case "PUT": {
                            var data = JsonConvert.SerializeObject(args);
                            var content = new StringContent(data, Encoding.UTF8, header);
                            var result = await client.PutAsync(response, content);
                            return await result.Content.ReadAsStringAsync();
                        }
                        case "PATCH": {
                            var data = JsonConvert.SerializeObject(args);
                            var content = new StringContent(data, Encoding.UTF8, header);
                            var result = await client.PatchAsync(response, content);
                            return await result.Content.ReadAsStringAsync();
                        }
                    }
                    return null;
                }
            }
            catch(Exception exc) {
                Console.WriteLine(exc);
                return null;
            }
        }

        private async Task<string> UploadFile(string url, AttachmentFile attachment) {
            using (HttpClient client = new HttpClient()) {
                using (MultipartFormDataContent formData = new MultipartFormDataContent()) {
                    FileStream fileStream = File.OpenRead(attachment.Path);
                    formData.Add(new StreamContent(fileStream), attachment.Type.ToString().ToLower(), fileStream.Name);

                    HttpResponseMessage httpResponse = await client.PostAsync(url, formData);
                    string response = await httpResponse.Content.ReadAsStringAsync();
                    return (string)JObject.Parse(response)["token"];
                }
            }
        }
        
        /// <summary>
        /// Get Updates without handler. <code>⚠️For advanced developers</code>
        /// </summary>
        /// <param name="marker">Marker for receiving next updates. If you haven't marker - keep it null</param>
        /// <returns>Array of updates in object <see cref="ReceivedUpdates"/></returns>
        public async Task<ReceivedUpdates> GetUpdates(long? marker = null) {
            try {
                var response = api_url + "/" + "updates" + "?" + Token + "&limit=" + Limit + "&timeout=" + Timeout;
                if(marker != null && marker > 0) {
                    response += "&marker=" + marker;
                }
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(response);
                httpWebRequest.Method ="GET";
                httpWebRequest.ContentType = header;
                httpWebRequest.Timeout = Timeout;
                httpWebRequest.ReadWriteTimeout = Timeout;

                using(HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse()) {
                    using Stream stream = httpWebResponse.GetResponseStream();
                    using StreamReader streamReader = new StreamReader(stream);
                    var jsonRaw = await streamReader.ReadToEndAsync();
                    var obj = JObject.Parse(jsonRaw).ToObject<ReceivedUpdates>();
                    obj.JsonRaw = jsonRaw;
                    return obj;
                }
            }
            catch(Exception exc) {
                return null;
            }
        }
        
        /// <summary>
        /// Get info about bot
        /// </summary>
        /// <returns>Bot info in object <see cref="User"/></returns>
        public async Task<User> GetMeAsync() {
            var response = await MakeRequest("GET", "me");
            return JObject.Parse(response).ToObject<User>();
        }
        
        /// <summary>
        /// Get list of dialogs/chats with your bot
        /// </summary>
        /// <param name="count">Max dialogs/chats returning. <code>Default: 50</code></param>
        /// <param name="marker">Page of dialogs/chats. <code>Default: null</code></param>
        /// <returns>Array of chats in object <see cref="ChatsList"/></returns>
        public async Task<ChatsList> GetChatsAsync(int count = 50, long? marker = null) {
            var response = await MakeRequest("GET", "chats");
            return JObject.Parse(response).ToObject<ChatsList>();
        }
        
        /// <summary>
        /// Get chat info by chat's link
        /// </summary>
        /// <param name="chatLink">Link of chat</param>
        /// <returns>Chat info in <see cref="Chat"/></returns>
        public async Task<Chat> GetChatByLinkAsync(string chatLink) {
            var response = await MakeRequest("GET", $"chats/{chatLink}");
            return JObject.Parse(response).ToObject<Chat>();
        }
        
        /// <summary>
        /// Get chat info by chat ID
        /// </summary>
        /// <param name="chatId">Chat ID</param>
        /// <returns>Chat info in <see cref="Chat"/></returns>
        public async Task<Chat> GetChatAsync(long chatId) {
            var response = await MakeRequest("GET", $"chats/{chatId}");
            return JObject.Parse(response).ToObject<Chat>();
        }
        
        /// <summary>
        /// Edit chat info
        /// </summary>
        /// <param name="chatId">ID of target chat</param>
        /// <param name="icon">New chat icon. <code>Default: null</code></param>
        /// <param name="title">New chat title. <code>Default: null</code></param>
        /// <param name="pin">New pinned message by message ID. <code>Default: null</code></param>
        /// <param name="notify">Notify about editing. <code>Default: null</code></param>
        /// <returns>Chat info in <see cref="Chat"/>, if one of optional param not null</returns>
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

            return args.Count > 0 ? JObject.Parse(await MakeRequest("PATCH", $"chats/{chatId}", args)).ToObject<Chat>() : null;
        }
        
        /// <summary>
        /// Send bot action to user or chat
        /// </summary>
        /// <param name="chatId">ID of chat</param>
        /// <param name="action">Action type</param>
        /// <returns>Request status</returns>
        public async Task<RequestStatus> SendActionAsync(long chatId, ActionType action) {
            var args = new Dictionary<string, dynamic>() { { "action", action } };
            return JsonConvert.DeserializeObject<RequestStatus>(await MakeRequest("POST", $"chats/{chatId}/actions", args));
        }
        
        /// <summary>
        /// Get pinned message info
        /// </summary>
        /// <param name="chatId">ID of chat</param>
        /// <returns>Message info in <see cref="Message"/></returns>
        public async Task<Message> GetPinnedMessageAsync(long chatId) {
            return JsonConvert.DeserializeObject<Message>(await MakeRequest("GET", $"chats/{chatId}/pin"));
        }
        
        /// <summary>
        /// Pin message in chat/dialog by message ID
        /// </summary>
        /// <param name="chatId">ID of chat/dialog</param>
        /// <param name="messageId">ID of target message</param>
        /// <param name="notify">Notify about pinned message. <code>Default: null</code></param>
        /// <returns>Request status</returns>
        public async Task<RequestStatus> PinMessageAsync(long chatId, string messageId, bool? notify = null) {
            var args = new Dictionary<string, dynamic>() { {"message_id", messageId} };
            if (notify != null)
                args.Add("notify", notify);
            return JsonConvert.DeserializeObject<RequestStatus>(await MakeRequest("PUT", $"chats/{chatId}/pin", args));
        }
        
        /// <summary>
        /// Unpin message in chat/dialog by message ID
        /// </summary>
        /// <param name="chatId">ID of chat/dialog</param>
        /// <returns>Request status</returns>
        public async Task<RequestStatus> UnpinMessageAsync(long chatId) {
            return JsonConvert.DeserializeObject<RequestStatus>(await MakeRequest("DELETE", $"chats/{chatId}/pin"));
        }
        
        /// <summary>
        /// Get chat membership for your bot
        /// </summary>
        /// <param name="chatId">ID of target chat</param>
        /// <returns>Bot info in <see cref="User"/></returns>
        public async Task<User> GetChatMembershipAsync(long chatId) {
            return JsonConvert.DeserializeObject<User>(await MakeRequest("GET", $"chats/{chatId}/members/me"));
        }
        
        /// <summary>
        /// Leave from chat by chat ID
        /// </summary>
        /// <param name="chatId">ID of target chat</param>
        /// <returns>Request status</returns>
        public async Task<RequestStatus> LeaveChatAsync(long chatId) {
            return JsonConvert.DeserializeObject<RequestStatus>(await MakeRequest("DELETE", $"chats/{chatId}/members/me"));
        }
        
        /// <summary>
        /// Get list of chat admins
        /// </summary>
        /// <param name="chatId">ID of target chat</param>
        /// <returns>List of admins in <see cref="ChatMembers"/></returns>
        public async Task<ChatMembers> GetChatAdminsAsync(long chatId)
        {
            return JsonConvert.DeserializeObject<ChatMembers>(
                await MakeRequest("GET", $"chats/{chatId}/members/admins"));
        }
        
        /// <summary>
        /// Get list of chat members info
        /// </summary>
        /// <param name="chatId">ID of target chat</param>
        /// <param name="userIds">List of user IDs. If array is null, you will get list of chat members by count. <code>Default: null</code></param>
        /// <param name="marker">Page of chat members. <code>Default: null</code></param>
        /// <param name="count">Members list's limit. <code>Default: null</code></param>
        /// <returns>Arrays of chat members in <see cref="ChatMembers"/></returns>
        public async Task<ChatMembers> GetChatMembersAsync(long chatId, IEnumerable<long>? userIds = null,
            long? marker = null, int? count = null) {
            var args = new Dictionary<string, dynamic>();
            if(userIds != null)
                args.Add("user_ids", userIds);
            if (marker != null)
                args.Add("marker", marker);
            if (count != null)
                args.Add("count", count);

            return JsonConvert.DeserializeObject<ChatMembers>(await MakeRequest("GET", $"chats/{chatId}/members", args));
        }
        
        /// <summary>
        /// Add member to chat
        /// </summary>
        /// <param name="chatId">ID of target chat</param>
        /// <param name="userIds">Array of user IDs for inviting to chat</param>
        /// <returns>Request status</returns>
        public async Task<RequestStatus> AddMembersAsync(long chatId, IEnumerable<long> userIds) {
            var args = new Dictionary<string, dynamic>() { {"user_ids", userIds} };
            return JsonConvert.DeserializeObject<RequestStatus>(await MakeRequest("POST", $"chats/{chatId}/members", args));
        }
        
        /// <summary>
        /// Remove chat member from chat
        /// </summary>
        /// <param name="chatId">ID of chat</param>
        /// <param name="userId">ID of target user</param>
        /// <param name="block">Block user after removing, if <b>block = true</b>. <code>Default: null</code></param>
        /// <returns>Request status</returns>
        public async Task<RequestStatus> RemoveMemberAsync(long chatId, long userId, bool? block = null) {
            var args = new Dictionary<string, string>() { {"user_ids", userId.ToString()} };
            if (block != null)
                args.Add("block", block.ToString().ToLower());
            return JsonConvert.DeserializeObject<RequestStatus>(await MakeRequest("DELETE", $"chats/{chatId}/members", additionalParams: args));
        }
        
        /// <summary>
        /// Get list of messages
        /// </summary>
        /// <param name="chatId">ID of chat</param>
        /// <param name="messageIds">Array of allowed message IDs. <code>Default: null</code></param>
        /// <param name="from">[FILTER] Time of sent message. <code>Default: null</code></param>
        /// <param name="to">[FILTER] Time of sent message. <code>Default: null</code></param>
        /// <param name="count">Limit for list of messages. <code>Default: null</code></param>
        /// <returns>Array of <see cref="Message"/></returns>
        public async Task<Message[]> GetMessagesAsync(long? chatId = null, IEnumerable<string> messageIds = null,
            DateTime? from = null, DateTime? to = null, int? count = null) {
            var args = new Dictionary<string, dynamic>();

            if (chatId != null) 
                args.Add("chat_id", chatId);
            if (messageIds != null)
                args.Add("message_ids", messageIds);
            if (from != null)
                args.Add("from", ((DateTimeOffset)from).ToUnixTimeMilliseconds());
            if (to != null)
                args.Add("to", ((DateTimeOffset)to).ToUnixTimeMilliseconds());
            if (count != null)
                args.Add("count", count);

            return JsonConvert.DeserializeObject<Message[]>(await MakeRequest("GET", "messages", args));
        }
        
        /// <summary>
        /// Send message function
        /// </summary>
        /// <param name="chatId">ID of target chat/dialog</param>
        /// <param name="isChat"><b>true</b>, if it's a chat</param>
        /// <param name="sendParams">Send message params(Attachments, buttons etc.)</param>
        /// <param name="disableLinkPreview">true, if you want disable link preview in message. <code>Default: true</code></param>
        /// <returns>Your sent message in <see cref="Message"/></returns>
        public async Task<Message> SendMessageAsync(long chatId, bool isChat, SendMessageParams sendParams,
            bool disableLinkPreview = false)
        {
            var urlArgs = new Dictionary<string, string>() { {"disable_link_preview", disableLinkPreview.ToString().ToLower()} };
            urlArgs.Add(isChat ? "chat_id" : "user_id", chatId.ToString());

            var args = sendParams.ToPostData();
            if (sendParams.Attachments != null || sendParams.Files != null) {
                var attachments = new List<Attachment>();
                if (sendParams.Attachments != null)
                    attachments = (List<Attachment>)sendParams.Attachments;
                if (sendParams.Files != null)
                    foreach (var attachment in sendParams.Files) {
                        switch (attachment.Type) {
                            case AttachmentType.Audio: {
                                var serverUpload = await MakeRequest("GET", "uploads", additionalParams:
                                    new Dictionary<string, string>() { { "type", "audio" } });
                                attachments.Add(new Attachment()
                                {
                                    Type = attachment.Type,
                                    Payload = new Payload() {
                                        Token = await UploadFile(serverUpload, attachment)
                                    }
                                });
                                break;
                            }
                            case AttachmentType.File: {
                                var serverUpload = await MakeRequest("GET", "uploads", additionalParams:
                                    new Dictionary<string, string>() { { "type", "file" } });
                                attachments.Add(new Attachment()
                                {
                                    Type = attachment.Type,
                                    Payload = new Payload() {
                                        Token = await UploadFile(serverUpload, attachment)
                                    }
                                });
                                break;
                            }
                            case AttachmentType.Video: {
                                var serverUpload = await MakeRequest("GET", "uploads", additionalParams:
                                    new Dictionary<string, string>() { { "type", "video" } });
                                attachments.Add(new Attachment()
                                {
                                    Type = attachment.Type,
                                    Payload = new Payload() {
                                        Token = await UploadFile(serverUpload, attachment)
                                    }
                                });
                                break;
                            }
                            case AttachmentType.Image: {
                                var serverUpload = await MakeRequest("GET", "uploads", additionalParams:
                                    new Dictionary<string, string>() { { "type", "image" } });
                                attachments.Add(new Attachment()
                                {
                                    Type = attachment.Type,
                                    Payload = new Payload() {
                                        Token = await UploadFile(serverUpload, attachment)
                                    }
                                });
                                break;
                            }
                        }
                    }
                args.Add("attachments", attachments);
            }

            try
            {
                return JsonConvert.DeserializeObject<Message>(await MakeRequest("POST", "messages", args, urlArgs));
            }
            catch
            {
                return null;
            }
        }
        
        /// <summary>
        /// Edit message by message ID
        /// </summary>
        /// <param name="messageId">ID of target message to edit it</param>
        /// <param name="editParams">Edit params</param>
        /// <returns>Request status</returns>
        public async Task<RequestStatus> EditMessageAsync(string messageId, EditMessageParams editParams) {
            var urlArgs = new Dictionary<string, string>() { {"message_id", messageId} };

            var args = editParams.ToPostData();
            if (editParams.Attachments != null) {
                var attachments = new List<Attachment>();
                if (editParams.Attachments != null)
                    attachments = (List<Attachment>)editParams.Attachments;
                if (editParams.Files != null)
                    foreach (var attachment in editParams.Files) {
                        switch (attachment.Type) {
                            case AttachmentType.Audio: {
                                var serverUpload = await MakeRequest("GET", "uploads", additionalParams:
                                    new Dictionary<string, string>() { { "type", "audio" } });
                                attachments.Add(new Attachment()
                                {
                                    Type = attachment.Type,
                                    Payload = new Payload() {
                                        Token = await UploadFile(serverUpload, attachment)
                                    }
                                });
                                break;
                            }
                            case AttachmentType.File: {
                                var serverUpload = await MakeRequest("GET", "uploads", additionalParams:
                                    new Dictionary<string, string>() { { "type", "file" } });
                                attachments.Add(new Attachment()
                                {
                                    Type = attachment.Type,
                                    Payload = new Payload() {
                                        Token = await UploadFile(serverUpload, attachment)
                                    }
                                });
                                break;
                            }
                            case AttachmentType.Video: {
                                var serverUpload = await MakeRequest("GET", "uploads", additionalParams:
                                    new Dictionary<string, string>() { { "type", "video" } });
                                attachments.Add(new Attachment()
                                {
                                    Type = attachment.Type,
                                    Payload = new Payload() {
                                        Token = await UploadFile(serverUpload, attachment)
                                    }
                                });
                                break;
                            }
                            case AttachmentType.Image: {
                                var serverUpload = await MakeRequest("GET", "uploads", additionalParams:
                                    new Dictionary<string, string>() { { "type", "image" } });
                                attachments.Add(new Attachment()
                                {
                                    Type = attachment.Type,
                                    Payload = new Payload() {
                                        Token = await UploadFile(serverUpload, attachment)
                                    }
                                });
                                break;
                            }
                        }
                    }
                args.Add("attachments", attachments);
            }

            return JsonConvert.DeserializeObject<RequestStatus>(await MakeRequest("PUT", "messages", args, urlArgs));
        }
        
        /// <summary>
        /// Delete message by message ID
        /// </summary>
        /// <param name="messageId">ID of target message</param>
        /// <returns>Request status</returns>
        public async Task<RequestStatus> DeleteMessageAsync(string messageId) {
            var urlArgs = new Dictionary<string, string>() { {"message_id", messageId} };

            return JsonConvert.DeserializeObject<RequestStatus>(await MakeRequest("DELETE", "messages", additionalParams: urlArgs));
        }
        
        /// <summary>
        /// Get message info by message ID
        /// </summary>
        /// <param name="messageId">ID of target message</param>
        /// <returns>Message info in <see cref="Message"/></returns>
        public async Task<Message> GetMessageAsync(string messageId) {
            return JsonConvert.DeserializeObject<Message>(await MakeRequest("GET", "messages/" + messageId));
        }
        
        /// <summary>
        /// Answer on callback
        /// </summary>
        /// <param name="callbackId">ID of callback</param>
        /// <param name="answerParams">Answer params for callback</param>
        /// <param name="notification"><b>true</b>, if you want answer as notification. <code>Default: null</code></param>
        /// <returns>Request status</returns>
        public async Task<RequestStatus> AnswerOnCallbackAsync(string callbackId, CallbackAnswerParams answerParams,
            bool? notification = null) {
            var urlArgs = new Dictionary<string, string>() { {"callback_id", callbackId} };

            var args = new Dictionary<string, dynamic>();
            
            if (answerParams.Attachments != null || answerParams.Files != null) {
                var attachments = new List<Attachment>();
                if (answerParams.Attachments != null)
                    attachments = (List<Attachment>)answerParams.Attachments;
                if (answerParams.Files != null)
                    foreach (var attachment in answerParams.Files) {
                        switch (attachment.Type) {
                            case AttachmentType.Audio: {
                                var serverUpload = await MakeRequest("GET", "uploads", additionalParams:
                                    new Dictionary<string, string>() { { "type", "audio" } });
                                attachments.Add(new Attachment()
                                {
                                    Type = attachment.Type,
                                    Payload = new Payload() {
                                        Token = await UploadFile(serverUpload, attachment)
                                    }
                                });
                                break;
                            }
                            case AttachmentType.File: {
                                var serverUpload = await MakeRequest("GET", "uploads", additionalParams:
                                    new Dictionary<string, string>() { { "type", "file" } });
                                attachments.Add(new Attachment()
                                {
                                    Type = attachment.Type,
                                    Payload = new Payload() {
                                        Token = await UploadFile(serverUpload, attachment)
                                    }
                                });
                                break;
                            }
                            case AttachmentType.Video: {
                                var serverUpload = await MakeRequest("GET", "uploads", additionalParams:
                                    new Dictionary<string, string>() { { "type", "video" } });
                                attachments.Add(new Attachment()
                                {
                                    Type = attachment.Type,
                                    Payload = new Payload() {
                                        Token = await UploadFile(serverUpload, attachment)
                                    }
                                });
                                break;
                            }
                            case AttachmentType.Image: {
                                var serverUpload = await MakeRequest("GET", "uploads", additionalParams:
                                    new Dictionary<string, string>() { { "type", "image" } });
                                attachments.Add(new Attachment()
                                {
                                    Type = attachment.Type,
                                    Payload = new Payload() {
                                        Token = await UploadFile(serverUpload, attachment)
                                    }
                                });
                                break;
                            }
                        }
                    }

                var message = new NewMessage() { Text = answerParams.Text, Attachments = attachments.ToArray(), 
                    Format = answerParams.Format, Notify = answerParams.Notify};
                args.Add("message", message);
                args.Add("notification", notification);
            }
            
            return JsonConvert.DeserializeObject<RequestStatus>(await MakeRequest("POST", "answers", args, urlArgs));
        }
        
        /// <summary>
        /// Send constructor to user
        /// </summary>
        /// <param name="sessionId">ID of session</param>
        /// <param name="constructParams">Constructor params</param>
        /// <returns>Request status</returns>
        public async Task<RequestStatus> ConstructMessageAsync(string sessionId, ConstructMessageParams constructParams) {
            var urlArgs = new Dictionary<string, string>() { {"session_id", sessionId} };

            var args = constructParams.ToPostData();
            if (constructParams.Messages != null) {
                var attachments = new List<Attachment>();
                foreach (var message in constructParams.Messages)
                {
                    if (message.Attachments != null)
                        attachments = (List<Attachment>)message.Attachments;
                    if (message.Files != null)
                        foreach (var attachment in message.Files) {
                            switch (attachment.Type) {
                                case AttachmentType.Audio: {
                                    var serverUpload = await MakeRequest("GET", "uploads", additionalParams:
                                        new Dictionary<string, string>() { { "type", "audio" } });
                                    attachments.Add(new Attachment()
                                    {
                                        Type = attachment.Type,
                                        Payload = new Payload() {
                                            Token = await UploadFile(serverUpload, attachment)
                                        }
                                    });
                                    break;
                                }
                                case AttachmentType.File: {
                                    var serverUpload = await MakeRequest("GET", "uploads", additionalParams:
                                        new Dictionary<string, string>() { { "type", "file" } });
                                    attachments.Add(new Attachment()
                                    {
                                        Type = attachment.Type,
                                        Payload = new Payload() {
                                            Token = await UploadFile(serverUpload, attachment)
                                        }
                                    });
                                    break;
                                }
                                case AttachmentType.Video: {
                                    var serverUpload = await MakeRequest("GET", "uploads", additionalParams:
                                        new Dictionary<string, string>() { { "type", "video" } });
                                    attachments.Add(new Attachment()
                                    {
                                        Type = attachment.Type,
                                        Payload = new Payload() {
                                            Token = await UploadFile(serverUpload, attachment)
                                        }
                                    });
                                    break;
                                }
                                case AttachmentType.Image: {
                                    var serverUpload = await MakeRequest("GET", "uploads", additionalParams:
                                        new Dictionary<string, string>() { { "type", "image" } });
                                    attachments.Add(new Attachment()
                                    {
                                        Type = attachment.Type,
                                        Payload = new Payload() {
                                            Token = await UploadFile(serverUpload, attachment)
                                        }
                                    });
                                    break;
                                }
                            }
                        }
                }

                args.Add("attachments", attachments);
            }

            return JsonConvert.DeserializeObject<RequestStatus>(await MakeRequest("POST", "answers/constructor", args, urlArgs));
        }
    }
}