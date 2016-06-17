using System;
using System.Text.RegularExpressions;

namespace OverwatchApi
{
    public class Player
    {
        public Player(string region, string uniqueUserString)
        {
            var match = Regex.Match(uniqueUserString, @"([^-#]+)[-#](\d+)");

            if (!match.Success) throw new ArgumentException();
            //TODO: better regex? more validation logic?

            Region = region;
            Username = match.Groups[1].Value;
            Discriminator = int.Parse(match.Groups[2].Value);
        }

        public Player(string region, string username, int discriminator)
        {
            Region = region;
            Username = username;
            Discriminator = discriminator;
        }

        public string Region { get; } //TODO: enum
        public string Username { get; }
        public int Discriminator { get; }

        public override string ToString()
        {
            return $"{Username}#{Discriminator} ({Region.ToUpper()})";
        }
    }
}