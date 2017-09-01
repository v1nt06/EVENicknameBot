using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace EVENicknameBot
{
    internal static class Runner
    {
        private static void Main()
        {
            var bot = new Bot();

            Console.WriteLine($"Bot started - {bot.GetStatus()}");

            bot.ShowPlayers();

            var offset = bot.GetUpdates(0).Count != 0 ? bot.GetUpdates(0)[0].Id : 0;

            while (true)
            {
                var updates = bot.GetUpdates(offset);
                foreach (var update in updates)
                {
                    if (offset < update.Id)
                    {
                        offset = update.Id;
                    }

                    var text = update.Message?.Text;
                    if (string.IsNullOrEmpty(text))
                    {
                        continue;
                    }

                    var patternAdd = "/add\\s[A-z\\d]{1}[A-z\\d\\-\\']{1,35}[A-z\\d]{1}";
                    var patternGetNickname = "/nick\\s[A-z\\d]{1}[A-z\\d\\-\\']{1,35}[A-z\\d]{1}";
                    if (Regex.IsMatch(text, patternAdd))
                    {
                        var nicknamePattern = "/add (.+)";
                        var nickname = Regex.Match(text, nicknamePattern).Groups[1].Value;
                        var responseMessage = $"Added {update.Message.From.FirstName} a.k.a. ingame {nickname}";
                        bot.AddPlayer(update, nickname);
                        bot.SendMessage(update, responseMessage);
                        Console.WriteLine(responseMessage);
                    }
                    else if (Regex.IsMatch(text, patternGetNickname))
                    {
                        var nicknamePattern = "/nick (.+)";
                        var nickname = Regex.Match(text, nicknamePattern).Groups[1].Value;
                        var name = bot.GetPlayers().FirstOrDefault(p => p.Nickname == nickname)?.Name;
                        if (string.IsNullOrEmpty(name))
                        {
                            bot.SendMessage(update, $"Player with nickname \"{nickname}\" not found");
                        }
                        else
                        {
                            bot.SendMessage(update, $"Name of ingame character {nickname} is {name}");
                        }
                    }
                    else if (text.StartsWith("/all"))
                    {
                        bot.SendAllPlayers(update);
                    }

                    offset++;
                }
            }
        }
    }
}
