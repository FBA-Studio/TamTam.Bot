using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TamTam.Bot.Types.Enums;

namespace TamTam.Bot.Types{

  public class UpdateRaw {
    public UpdateType UpdateType { get; set; }
    
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTime TimeStamp { get; set; }

    public Message? Message { get; set; }
    public Callback? Callback { get; set; }
    public User? User { get; set; }
    public string? MessageId { get; set; }
    public long? ChatId { get; set; }
    public long? UserId { get; set; }
    public string? UserLocale { get; set; }
    public bool? IsChannel { get; set; }
    public long? InviterId { get; set; }

    public Payload? Payload { get; set; }
    public long? AdminId { get; set; }
    public string? Title { get; set; }
  }
}
