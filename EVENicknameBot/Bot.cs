using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using EVENicknameBot.Response;
using Newtonsoft.Json;

namespace EVENicknameBot
{
    internal sealed class Bot
    {
        private const string Token = "409196312:AAFuR_6P4ML6VQaFB6eRaJuKZryTH_rLoxc";
        private static readonly string Url = $"https://api.telegram.org/bot{Token}/";
        private readonly List<Player> _players = new List<Player>();

        public Bot()
        {
            var playerLines = File.ReadAllLines(Environment.CurrentDirectory + "\\Players.txt");
            foreach (var playerLine in playerLines)
            {
                _players.Add(new Player(playerLine));
            }
        }

        internal T Request<T>(Method method, Dictionary<string, string> parameters)
        {
            var request = GetRequest(method, parameters);
            var responseString = GetResponseString(request);
            var jsonResponse = JsonConvert.DeserializeObject<T>(responseString);
            return jsonResponse;
        }

        internal JsonResponse Request(Method method)
        {
            var request = GetRequest(method);
            var responseString = GetResponseString(request);
            var jsonResponse = JsonConvert.DeserializeObject<JsonResponse>(responseString);
            return jsonResponse;
        }

        internal JsonResponse Request(Method method, Dictionary<string, string> parameters)
        {
            var request = GetRequest(method, parameters);
            var responseString = GetResponseString(request);
            var jsonResponse = JsonConvert.DeserializeObject<JsonResponse>(responseString);
            return jsonResponse;
        }

        internal bool GetStatus()
        {
            return Request(Method.GetMe).Ok;
        }

        internal List<Update> GetUpdates(int offset)
        {
            var paremeters = new Dictionary<string, string> { { "offset", offset.ToString() } };
            return Request<Updates>(Method.GetUpdates, paremeters).UpdatesArray.ToList();
        }

        internal void SendMessage(Update update, string text)
        {
            var parameters =
                new Dictionary<string, string>
                {
                    {"chat_id", update.Message.Chat.Id.ToString()},
                    {"text", text}
                };
            Request(Method.SendMessage, parameters);
        }

        internal void ShowPlayers()
        {
            foreach (var player in _players)
            {
                Console.WriteLine($"{player.Name} - {player.Nickname}");
            }
        }

        internal void AddPlayer(Update update, string nickname)
        {
            var playerLine = $"{update.Message.From.FirstName};{nickname}";
            File.AppendAllLines(Environment.CurrentDirectory + "Players.txt", new[] { playerLine });
            _players.Add(new Player(playerLine));
        }

        internal void SendAllPlayers(Update update)
        {
            SendMessage(update, "All players:");
            foreach (var player in _players)
            {
                SendMessage(update, $"{player.Name} a.k.a. ingame {player.Nickname}");
            }
        }

        internal ReadOnlyCollection<Player> GetPlayers()
        {
            return _players.AsReadOnly();
        }

        private string GetResponseString(WebRequest request)
        {
            try
            {
                var response = request.GetResponse();
                return GetResponseString(response);
            }
            catch (WebException e)
            {
                var responseString = GetResponseString(e.Response);
                Console.WriteLine(e.Message);
                Console.WriteLine(responseString);
                return null;
            }
        }

        private string GetResponseString(WebResponse response)
        {
            using (var stream = response.GetResponseStream())
            {
                if (stream == null)
                {
                    Console.WriteLine("Response stream is null");
                    return null;
                }

                using (var streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        private WebRequest GetRequest(Method method)
        {
            return GetRequest(method, new Dictionary<string, string>());
        }

        private WebRequest GetRequest(Method method, Dictionary<string, string> parameters)
        {
            var url = UrlHelper.GetUrlWithParameters(method, Url, parameters);
            return WebRequest.Create(url);
        }
    }
}
