using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TamTam.Bot.Types.Enums;

namespace TamTam.Bot.Types{

  public class UpdateRaw {
    [JsonProperty("update_type")]
    public UpdateType UpdateType { get; set; }
    
    [JsonConverter(typeof(MillisecondEpochConverter))]
    [JsonProperty("timestamp")]
    public DateTime TimeStamp { get; set; }
    
    [JsonProperty("message")]
    public Message? Message { get; set; }
    
    [JsonProperty("callback")]
    public Callback? Callback { get; set; }
    
    [JsonProperty("user")]
    public User? User { get; set; }
    
    [JsonProperty("message_id")]
    public string? MessageId { get; set; }
    
    [JsonProperty("chat_id")]
    public long? ChatId { get; set; }
    
    [JsonProperty("user_id")]
    public long? UserId { get; set; }
    
    [JsonProperty("user_locale")]
    public string? UserLocale { get; set; }
    
    [JsonProperty("is_channel")]
    public bool? IsChannel { get; set; }
    
    [JsonProperty("inviter_id")]
    public long? InviterId { get; set; }
    
    [JsonProperty("session_id")]
    public string? SessionId { get; set; }
    
    [JsonProperty("data")]
    public string? Data { get; set; }
    
    [JsonProperty("input")]
    public Input? Input { get; set; }
    
    [JsonProperty("chat")]
    public Chat? Chat { get; set; }
    
    [JsonProperty("start_payload")]
    public string? StartPayload { get; set; }
    
    [JsonProperty("payload")]
    public Payload? Payload { get; set; }
    
    [JsonProperty("admin_id")]
    public long? AdminId { get; set; }
    
    [JsonProperty("title")]
    public string? Title { get; set; }
  }
}
