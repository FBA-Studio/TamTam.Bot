using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TamTam.Bot {
    public class MillisecondEpochConverter : DateTimeConverterBase {
        private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            long milliseconds = (long)((DateTime)value - _epoch).TotalMilliseconds;
            writer.WriteValue(milliseconds);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (reader.Value == null) { return null; }
            long milliseconds = (long)reader.Value;
            return _epoch.AddMilliseconds(milliseconds);
        }
    }
}