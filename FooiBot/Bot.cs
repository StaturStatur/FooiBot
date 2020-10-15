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
            Client.Heartbeated += LFGPoster;

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
            string[] status = { "I Serve Fooi. I am a Salve of the Micha! " };
            var x = new DiscordActivity(name: status[0], type: ActivityType.Playing);
            Client.UpdateStatusAsync(x);

            Console.WriteLine("Online Bitch");
            return Task.CompletedTask;
        }

        public int i = 0;

        //nur wöchentlicher poster
        public async Task LFGPoster(HeartbeatEventArgs e)
        {
            var lfgchannel = await Client.GetChannelAsync(763993776246882314).ConfigureAwait(false);

            //wöchentlicher reset
            var dt = DateTime.Now;
            if (dt.DayOfWeek == DayOfWeek.Tuesday &&
                dt.ToShortTimeString() == "22:00")
            {
                //leeren anmeldungen
                LeerenDi();

                //altes lfg löschen
                var msg = await lfgchannel.GetMessagesAsync(1).ConfigureAwait(false);
                await lfgchannel.DeleteMessageAsync(msg[0]).ConfigureAwait(false);

                //neues (halb) leeres LFG bauen und posten
                var lfgemb = LFGembbuilder();
                await Client.SendMessageAsync(lfgchannel, null, false, embed: lfgemb);
            }
            if (dt.DayOfWeek == DayOfWeek.Thursday &&
                dt.ToShortTimeString() == "22:00")
            {
                //leeren anmeldungen
                LeerenDo();

                //altes lfg löschen
                var msg = await lfgchannel.GetMessagesAsync(1).ConfigureAwait(false);
                await lfgchannel.DeleteMessageAsync(msg[0]).ConfigureAwait(false);

                //neues (halb) leeres LFG bauen und posten
                var lfgemb = LFGembbuilder();
                await Client.SendMessageAsync(lfgchannel, null, false, embed: lfgemb);
            }
        }

        //leeres lfg machen (für diensatg und donnerstag)
        public DiscordEmbed LFGembbuilder()
        {
            var LFGemb = new DiscordEmbedBuilder
            {
                Title = "LFG",
                Color = DiscordColor.Green,
            };
            string emptyline = "\u200B\n";

            string ExpDi = emptyline + "**Exp**\n - \n - \n - \n - \n - \n - \n - \n";
            string Lvl2Di = emptyline + "**Trainee Lvl2**\n - \n";
            string Lvl1Di = emptyline + "**Trainee Lvl1**\n - \n";
            string Lvl0Di = emptyline + "**Trainee Lvl0**\n - \n";

            string Dienstag = ExpDi + Lvl2Di + Lvl1Di + Lvl0Di;

            string ExpDo = emptyline + "**Exp**\n - \n - \n - \n - \n - \n - \n - \n - \n";
            string Lvl2Do = emptyline + "**Trainee Lvl2**\n - \n - ";

            string Donnerstag = ExpDo + Lvl2Do;

            LFGemb.AddField("**Dienstag**", Dienstag, false);
            LFGemb.AddField(emptyline, emptyline + "** ----------------------------------------------------- **" + emptyline + emptyline, false);
            LFGemb.AddField("**Donnerstag**", Donnerstag, false);

            return LFGemb;
        }

        public void LeerenDi()
        {
            //reset Dienstag txt's
            File.WriteAllText(@"dienstagexp.txt", "\n**Exp**\n - \n - \n - \n - \n - \n - \n - \n");
            File.WriteAllText(@"dienstaglvl2.txt", "\n**Trainee Lvl2**\n - \n");
            File.WriteAllText(@"dienstaglvl1.txt", "\n**Trainee Lvl1**\n - \n");
            File.WriteAllText(@"dienstaglvl0.txt", "\n**Trainee Lvl0**\n - \n");

            //reset anmeldungs couter
            _ = new RaidLFG
            {
                ExpDiAnm = 1,
                Lvl2DiAnm = 1,
                Lvl1DiAnm = 1,
                Lvl0DiAnm = 1
            };
        }

        public void LeerenDo()
        {
            //reset Donnerstag txt's
            File.WriteAllText(@"donnerstagexp.txt", "\n**Exp**\n - \n - \n - \n - \n - \n - \n - \n - \n");
            File.WriteAllText(@"donnerstaglvl2.txt", "\n**Trainee Lvl2**\n - \n - \n");

            //reset anmeldungs counters
            _ = new RaidLFG
            {
                ExpDoAnm = 1,
                Lvl2DoAnm = 1
            };
        }
    }
}
