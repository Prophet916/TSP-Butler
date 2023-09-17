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
    public class TestSL : ApplicationCommandModule
    {
        [SlashCommand("test", "This is a test slash command")]
        public async Task TestSlashCommand(InteractionContext ctx,
            [Choice("Tovera", "Tovera")]
            [Choice("Battlecarrier", "Battlecarrier")]
            [Option("Ship", "What Ship is it?")] string text,
            [Option("System", "What solar system is it in?")] string system,
            [Option("Sector", "What sector is it in?")] string sector)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                new DiscordInteractionResponseBuilder()
                .WithContent("Starting slash command..."));

            var embedMessage = new DiscordEmbedBuilder()
            {
                Title = text,
                Description = system + "\n" + sector
                
            };

            await ctx.Channel.SendMessageAsync(embed :  embedMessage);
        }

    }
}
