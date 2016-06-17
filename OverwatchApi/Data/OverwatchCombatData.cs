using AngleSharp.Dom;
using OverwatchApi.Properties;

namespace OverwatchApi.Data
{
    public sealed class OverwatchCombatData : OverwatchData<OverwatchCombatData>
    {
        internal OverwatchCombatData(Player player, IDocument document) : base(player)
        {
            RegisterDataProperty("Melee final blows", x => x.MeleeFinalBlows);
            RegisterDataProperty("Solo kills", x => x.SoloKills);
            RegisterDataProperty("Objective kills", x => x.ObjectiveKills);
            RegisterDataProperty("Final blows", x => x.FinalBlows);
            RegisterDataProperty("Damage done", x => x.DamageDone);
            RegisterDataProperty("Eliminations", x => x.Eliminations);
            RegisterDataProperty("Environmental kills", x => x.EnvironmentalKills);
            RegisterDataProperty("Multikills", x => x.MultiKills);

            LoadData(document.GetDataTableByHeaderText("Combat"));
        }
        
        public int SoloKills { get; internal set; }

        public int MultiKills { get; set; }

        public int EnvironmentalKills { get; set; }

        public int Eliminations { get; set; }

        public long DamageDone { get; set; }

        public int FinalBlows { get; set; }

        public int ObjectiveKills { get; set; }

        public int MeleeFinalBlows { get; set; }
    }
}