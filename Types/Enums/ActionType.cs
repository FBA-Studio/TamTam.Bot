using System.Runtime.Serialization;

namespace TamTam.Bot.Types.Enums {
    public enum ActionType {
        [EnumMember(Value = "typing_on")]
        TypingOn,
        
        [EnumMember(Value = "sending_photo")]
        SendingPhoto,
        
        [EnumMember(Value = "sending_video")]
        SendingVideo,
        
        [EnumMember(Value = "sending_audio")]
        SendingAudio,
        
        [EnumMember(Value = "sending_file")]
        SendingFile,
        
        [EnumMember(Value = "mark_seen")]
        MarkSeen
    }
}