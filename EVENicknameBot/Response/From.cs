﻿using Newtonsoft.Json;

namespace EVENicknameBot.Response
{
    internal sealed class From
    {
        public int Id { get; set; }

        [JsonProperty("is_bot")]
        public bool IsBot { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        public string Username { get; set; }

        [JsonProperty("language_code")]
        public string LanguageCode { get; set; }
    }
}
