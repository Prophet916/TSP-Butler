using DSharpPlus.Entities;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP_Butler.config;

namespace TSP_Butler.Slash_Commands
{
    public class Poi : ApplicationCommandModule
    {
        public Poi(DataBaseAccess dbAccess)
        {
            Console.WriteLine("DB Access beginning");
            _dbAccess = dbAccess;
        }

        public readonly DataBaseAccess _dbAccess;

        [SlashCommand("black-pyramid", "Add a Black Pyramid location")]
        public async Task BlackPyramidLocation(InteractionContext ctx,
        [Option("SolarSystem", "Name of the solar system")] string solarSystem,
        [Option("Planet", "Name of the planet")] string planet)
        {
            Console.WriteLine("Black Pyramid command called");
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                new DiscordInteractionResponseBuilder()
                .WithContent("Starting slash command..."));
            // Create a new BlackPyramidLocation object
            var location = new BlackPyramidLocation
            {
                SolarSystem = solarSystem,
                Planet = planet
            };

            Console.WriteLine("Reached beginning of DB insert");

            // Insert the location into MongoDB
            _dbAccess.InsertBlackPyramidLocation(location);

            Console.WriteLine("DB Insert compelte");


            // Get all Black Pyramid locations from MongoDB
            var locations = _dbAccess.GetAllBlackPyramidLocations();

            Console.WriteLine("Get locations complete");

            // Create an embed to display the locations
            var embedBP = new DiscordEmbedBuilder(
            {
                Title = "Black Pyramid Locations:"
            };

            Console.WriteLine("Embed builder complete");

            for (int i = 0; i < locations.Count; i++)
            {
                var locationText = $"{i + 1}. Solar System: {locations[i].SolarSystem}, Planet: {locations[i].Planet}";
                embedBP.AddField($"Location {i + 1}", locationText);
            }

            Console.WriteLine("Update count and remake embed complete");

            await ctx.Channel.SendMessageAsync(embed: embedBP);
        }
    }

}
