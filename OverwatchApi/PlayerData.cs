using System.Globalization;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using OverwatchApi.Data;

namespace OverwatchApi
{
    
    public class PlayerData
    {
        private IBrowsingContext _browsingContext;
        public Player Player { get; set; }

        private IDocument _document;
        private OverwatchGameData _gameData;
        private OverwatchCombatData _combatData;

        private readonly string _url;


        public PlayerData(Player player)
        {
            Player = player;
            _browsingContext = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
            _url = "https://playoverwatch.com" +
                   $"/en-us/career/pc/{player.Region}/{player.Username}-{player.Discriminator.ToString(CultureInfo.InvariantCulture)}";
        }

        private async Task<IDocument> GetDocument()
        {
            if (_document != null)
                return _document;

            return _document = await _browsingContext.OpenAsync(_url);
        }

        public async Task<OverwatchGameData> GetGameDataAsync()
        {
            if (_gameData != null)
                return _gameData;

            return _gameData = new OverwatchGameData(Player, await GetDocument());
        }

        public async Task<OverwatchCombatData> GetCombatDataAsync()
        {
            if (_combatData != null)
                return _combatData;

            return _combatData = new OverwatchCombatData(Player, await GetDocument());
        }
    }
}