using Newtonsoft.Json;

namespace EVENicknameBot.Response
{
    internal sealed class Update
    {
        [JsonProperty("update_id")]
        public int Id { get; set; }

        public Message Message { get; set; }
    }
}