using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_Butler.commands
{
    public class TestCommands : BaseCommandModule
    {
        [Command("test")]
        [Cooldown(2, 10, CooldownBucketType.User)]
        public async Task MyFirstCommand(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Hello");
        }

        [Command("interactivity")]
        public async Task InterTest(CommandContext ctx)
        {
            var interactivity = Program.Client.GetInteractivity();

            var messageToRetrieve = await interactivity.WaitForMessageAsync(message => message.Content == "Hello");

            if(messageToRetrieve.Result.Content == "Hello")
            {
                await ctx.Channel.SendMessageAsync($"Hello {ctx.User.Username}, I am your factions butler, what can I do for you today?");
            }
            
        }

        [Command("Reaction")]
        public async Task ReactionTest(CommandContext ctx)
        {
            var interactivity = Program.Client.GetInteractivity();

            var messageToReact = await interactivity.WaitForReactionAsync(message => message.Message.Id == 1152464155354337280);

            if (messageToReact.Result.Message.Id == 1152464155354337280)
            {
                await ctx.Channel.SendMessageAsync($"{ctx.User.Username} thinks they are a devil");
            }
        }
    }
}
