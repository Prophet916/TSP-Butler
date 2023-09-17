using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using TSP_Butler.commands;
using TSP_Butler.config;
using TSP_Butler.Slash_Commands;

namespace TSP_Butler
{
    public sealed class Program
    {
        public static DiscordClient Client { get; private set; }
        public static CommandsNextExtension Commands { get; set; }
        public static DataBaseAccess DbAccess { get; private set; }

        static async Task Main(string[] args)
        {
            var testChan = 590783288386519060;

            var jsonReader = new JSONReader();
            await jsonReader.ReadJSON();

            // Bot configuration
            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = jsonReader.token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            var mongo = jsonReader.mongoDBConnection.ToString();

            // Apply the config to this DiscordClient
            Client = new DiscordClient(discordConfig);

            // Initialize database access
            DbAccess = new DataBaseAccess(mongo, "bob"); // Replace with your actual database name

            try
            {
                // Check if the database connection is successful
                DbAccess.TestConnection(); // Implement this method in your database access class

                Console.WriteLine("Connected to MongoDB.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to connect to MongoDB: {ex.Message}");
                return; // Exit the program if the connection fails
            }


            // Default timeout for commands that use interactivity
            Client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            // Task handler ready event
            Client.Ready += Client_Ready;

            // Task handler more message creation
            //Client.MessageCreated += MessageCreatedHnadler;
            Client.VoiceStateUpdated += VoiceChannelHandler;

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new String[] { jsonReader.prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false
            };

            Commands = Client.UseCommandsNext(commandsConfig);
            var slashCommandConfig = Client.UseSlashCommands();

            Commands.CommandErrored += CommandEventHandler;

            // Register commands
            Commands.RegisterCommands<TestCommands>();

            // Slash Commands
            Console.WriteLine("Registering test command");
            slashCommandConfig.RegisterCommands<TestSL>(590783286503145473);
            Console.WriteLine("Registering test command complete");
            Console.WriteLine("Registering black command");
            slashCommandConfig.RegisterCommands<Poi>(590783286503145473);
            Console.WriteLine("Registering black command complete");


            await Client.ConnectAsync();
            await Task.Delay(-1);
        
        }

        private static async Task CommandEventHandler(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
            string timeLeft = string.Empty;
            if (e.Exception is ChecksFailedException exception)
            {
                foreach (var check in exception.FailedChecks)
                {
                    var coolDown = (CooldownAttribute)check;
                    timeLeft = coolDown.GetRemainingCooldown(e.Context).ToString(@"hh\:mm\:ss");
                }

                var coolDownMessage = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Red,
                    Title = "Please wait for the cooldown to end",
                    Description = $"Time: {timeLeft}"
                };

                await e.Context.Channel.SendMessageAsync(embed : coolDownMessage);
            }
        }

        private static async Task VoiceChannelHandler(DiscordClient sender, DSharpPlus.EventArgs.VoiceStateUpdateEventArgs e)
        {
            if(e.Before == null && e.Channel.Name == "Lodge All Complaints Here")
            {
                await e.Channel.SendMessageAsync($"{e.User.Username} joined the voice channel");
            }
        }

        //private static async Task MessageCreatedHnadler(DiscordClient sender, DSharpPlus.EventArgs.MessageCreateEventArgs e)
        //{
        //    await e.Channel.SendMessageAsync("This event handler was triggered");
        //}

        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
