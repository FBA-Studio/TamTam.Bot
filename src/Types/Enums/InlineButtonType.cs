using System.Runtime.Serialization;

namespace TamTam.Bot.Types.Enums {
    public enum InlineButtonType {
        Callback,
        Link,
        
        [EnumMember(Value = "request_contact")]
        RequestContact,
        
        [EnumMember(Value = "request_geo_location")]
        RequestGeoLocation,
        
        Chat
    }
}