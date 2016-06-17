using System;
using System.Threading.Tasks;
using OverwatchApi;

namespace TestUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var task = Run();

            task.Wait();
        }

        private static async Task Run()
        {
            try
            {
                await MainAsync();
            }
            catch (Exception x)
            {
                x.GetBaseException();
                ;
            }
        }

        private static async Task MainAsync()
        {
            var player = new Player("eu", "Amiodarone#2335");

            var playerData = new PlayerData(player);

            var gameData = await playerData.GetGameDataAsync();
            var combatData = await playerData.GetCombatDataAsync();

            ;
        }
    }
}