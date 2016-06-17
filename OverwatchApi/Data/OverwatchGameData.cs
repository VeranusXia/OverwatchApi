using System;
using System.Linq;
using AngleSharp.Dom;
using OverwatchApi.Properties;

namespace OverwatchApi.Data
{
    
    public sealed class OverwatchGameData : OverwatchData<OverwatchGameData>
    {
        internal OverwatchGameData(Player player, IDocument document) : base(player)
        {
            RegisterDataProperty("Games won", x => x.GamesWon);
            RegisterDataProperty("Games played", x => x.GamesPlayed);
            RegisterDataProperty("Time spent on fire", x => x.TimeSpentOnFire);
            RegisterDataProperty("Objective time", x => x.ObjectiveTime);
            RegisterDataProperty("Score", x => x.Score);
            RegisterDataProperty("Time played", x => x.TimePlayed, ConvertPlayedTime);

            LoadData(document.GetDataTableByHeaderText("Game"));
        }

        private TimeSpan ConvertPlayedTime(string s)
        {
            //TODO: more thorough. This assumes hours which may not be what the page actually sends in all cases.
            var sanitized = string.Concat(s.TakeWhile(char.IsDigit));
            return TimeSpan.FromHours(int.Parse(sanitized));
        }

        public int GamesWon { get; internal set; }

        public int GamesPlayed { get; internal set; }

        public TimeSpan TimeSpentOnFire { get; internal set; }

        public TimeSpan ObjectiveTime { get; internal set; }

        public long Score { get; internal set; }

        public TimeSpan TimePlayed { get; internal set; }
    }
}