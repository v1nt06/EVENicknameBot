using Newtonsoft.Json;

namespace EVENicknameBot.Response
{
    internal sealed class Updates : JsonResponse
    {
        [JsonProperty("result")]
        public Update[] UpdatesArray { get; set; }
    }
}