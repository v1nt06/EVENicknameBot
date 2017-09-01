using System;
using Newtonsoft.Json;

namespace EVENicknameBot
{
    internal class DateTimeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var timestamp = Convert.ToDouble(reader.Value + "000");
            var date = new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(timestamp);
            return date;
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}
