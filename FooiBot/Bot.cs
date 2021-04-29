using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using FooiBot;
using FooiBot.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FooiBot
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        public DiscordRichPresence Presence { get; private set; }

        public async Task RunAsync()
        {
            RaidLFG raidLFG = new RaidLFG();


            var json = string.Empty;

            using (var fs = File.OpenRead(@"D:\Programme\Token\config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            var config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            };

            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;
            Client.Heartbeated += HearthBeatPoster;

            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableDms = false,
                EnableMentionPrefix = true
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<UtilityCommands>();
            Commands.RegisterCommands<RaidLFG>();

            await Client.ConnectAsync();

            await Task.Delay(-1);
        }

        private Task OnClientReady(ReadyEventArgs e)
        {
            //Status machen
            string[] status = { "I Serve Fooi. I am a Slave of the Micha!" };
            var x = new DiscordActivity(name: status[0], type: ActivityType.Playing);
            Client.UpdateStatusAsync(x);

            Console.WriteLine("Online Bitch");
            return Task.CompletedTask;
        }

        public int i = 0;

        //regeläßiger poster
        public async Task HearthBeatPoster(HeartbeatEventArgs e)
        {
            //di/do reset noch implementiere
            //RaidLFG raidLFG = new RaidLFG();
            var lfgchannel = await Client.GetChannelAsync(763993776246882314).ConfigureAwait(false);

            //wöchentlicher reset
            var dt = DateTime.Now;
            if (dt.DayOfWeek == DayOfWeek.Tuesday &&
                dt.ToShortTimeString() == "22:00")
            {
                //leeren anmeldungen
                DBOperations.LeererDi();

                //altes lfg löschen
                var msg = await lfgchannel.GetMessagesAsync(1).ConfigureAwait(false);
                await lfgchannel.DeleteMessageAsync(msg[0]).ConfigureAwait(false);

                //neues (halb) leeres LFG bauen und posten (aus RaidLFG manuell ausführen)
                //raidLFG.Manuell(ctx);      
            }
            if (dt.DayOfWeek == DayOfWeek.Thursday &&
                dt.ToShortTimeString() == "22:00")
            {
                //leeren anmeldungen
                DBOperations.LeererDo();

                //altes lfg löschen
                var msg = await lfgchannel.GetMessagesAsync(1).ConfigureAwait(false);
                await lfgchannel.DeleteMessageAsync(msg[0]).ConfigureAwait(false);

                //neues (halb) leeres LFG bauen und posten (aus RaidLFG manuell ausführen)
                //raidLFG.Manuell(ctx); 
            }
        }
    }
}