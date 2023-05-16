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

        public async void StartPolling(Func<Update, Task> updateHandler) {
            while(true) {
                var updates = await GetUpdates();
                Marker = Math.Max(Marker, updates.Marker);
                foreach (var update in updates.Updates) {
                    var upd = ParseRawUpdate(update);
                    await updateHandler.Invoke(upd);
                }
            }
        }

        private Update ParseRawUpdate(UpdateRaw raw) {
            var update = new Update() {UpdateType = raw.UpdateType };
            switch (raw.UpdateType)
            {
                case UpdateType.BotAdded:
                    update.BotAdded = new BotAdded() { TimeStamp = raw.TimeStamp, ChatId = raw.ChatId.Value, User = raw.User, IsChannel = raw.IsChannel.Value };
                    break;
                case UpdateType.UserAdded:
                    update.UserAdded = new UserAdded() { TimeStamp = raw.TimeStamp, ChatId = raw.ChatId.Value, User = raw.User, InviterId = raw.InviterId.Value, IsChannel = raw.IsChannel.Value } ;
                    break;
                case UpdateType.BotRemoved:
                    update.BotRemoved = new BotRemoved() {TimeStamp = raw.TimeStamp, ChatId = raw.ChatId.Value, User = raw.User, IsChannel = raw.IsChannel.Value };
                    break;
                case UpdateType.MessageCallback:
                    update.MessageCallback = new MessageCallback() {TimeStamp = raw.TimeStamp, Callback = raw.Callback, Message = raw.Message, UserLocale = raw.UserLocale };
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
        private async Task<string> MakeRequest(string method, string urlMethod, Dictionary<string, string> args) {
            try {
                var response = api_url + "/" + urlMethod + "?" + Token;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(response);
                httpWebRequest.Method = method;
                httpWebRequest.ContentType = header;

                if(args.Count > 0) {

                }

                using(HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse()) {
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
            var response = await MakeRequest("GET", "me", new Dictionary<string, string>());
            return JObject.Parse(response).ToObject<User>();
        }
    }
}
