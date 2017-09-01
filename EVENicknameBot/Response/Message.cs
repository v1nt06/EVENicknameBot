using Newtonsoft.Json;
using System;

namespace EVENicknameBot.Response
{
    internal sealed class Message
    {
        [JsonProperty("message_id")]
        public int Id { get; set; }

        [JsonProperty("date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Date { get; set; }

        public string Text { get; set; }

        public From From { get; set; }

        public Chat Chat { get; set; }
    }
}