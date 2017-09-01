using Newtonsoft.Json;

namespace EVENicknameBot.Response
{
    internal sealed class Chat
    {
        public int Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        public string Username { get; set; }

        public string Type { get; set; }
    }
}
