namespace EVENicknameBot
{
    internal sealed class Player
    {
        internal string Name { get; set; }

        internal string Nickname { get; set; }

        public Player(string playerLine)
        {
            Name = playerLine.Substring(0, playerLine.IndexOf(";"));
            Nickname = playerLine.Substring(playerLine.IndexOf(";") + 1);
        }
    }
}
