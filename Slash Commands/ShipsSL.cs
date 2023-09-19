using DSharpPlus.Entities;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_Butler.Slash_Commands
{
    public class ShipsSL : ApplicationCommandModule
    {
        public CRUD _crud { get; set; }

        [SlashCommand("ship", "Which ship to kill is it?")]
        public async Task ShipToKill(InteractionContext ctx,
            [Choice("Tovera", "Tovera")]
            [Choice("Battlecarrier", "Battlecarrier")]
            [Option("Ship", "What Ship is it?")] string text,
            [Option("System", "What solar system is it in?")] string system,
            [Option("Sector", "What sector is it in?")] string sector)
        {
            Console.WriteLine("Beginning"); // beginning
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                                         new DiscordInteractionResponseBuilder()
                                         .WithContent("Starting slash command..."));

            var timeout = TimeSpan.FromSeconds(30);
            var databaseTask = _crud.GetAllShips();

            var completedTask = await Task.WhenAny(databaseTask, Task.Delay(timeout));

            if (completedTask == databaseTask)
            {
                var shipsToKillLists = await databaseTask;
                var embedShips = new DiscordEmbedBuilder()
                {
                    Title = text,
                    Description = system + sector
                };

                Console.WriteLine("Foreach");

                foreach (var ship in shipsToKillLists)
                {
                    var shipInfo = $"{ship.ShipName} in {ship.SolarSystem}, {ship.Sector}";
                    embedShips.AddField(ship.ShipName, shipInfo);
                }

                await ctx.Channel.SendMessageAsync(embed: embedShips);
            }

        }
    }
}
