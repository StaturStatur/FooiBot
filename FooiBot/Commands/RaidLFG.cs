using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FooiBot.Commands
{
    class RaidLFG : BaseCommandModule
    {
        public int ExpDiAnm = 1;
        public int Lvl2DiAnm = 1;
        public int Lvl1DiAnm = 1;
        public int Lvl0DiAnm = 1;

        public int ExpDoAnm = 1;
        public int Lvl2DoAnm = 1;

        //0 = DiscordRolle
        //1 = Tag
        //2 = Rolle
        //3 = Klasse
        public string[] Anmeldung = new string[4];

        public string BuilderDi(CommandContext ctx)
        {
            string DiLFG = string.Empty;
            string[,] DiExp = new string[3, 30];
            string[,] DiLvl2 = new string[3, 30];
            string[,] DiLvl1 = new string[3, 30];
            string[,] DiLvl0 = new string[3, 30];
            string DiLFGExp = string.Empty;
            string DiLFGLvl2 = string.Empty;
            string DiLFGLvl1 = string.Empty;
            string DiLFGLvl0 = string.Empty;

            //neu einschreiben
            if (Anmeldung[1] == "Di")
            {
                DBOperations.EinschreibenDi(Anmeldung, ctx);
            }

            //auslesen
            var Data = DBOperations.AuslesenDi();
            DiExp = Data.Item1;
            DiLvl2 = Data.Item2;
            DiLvl1 = Data.Item3;
            DiLvl0 = Data.Item4;

            DiLFGExp = "**Exp** (" + (ExpDiAnm-1) + "/7 Exp-Anmeldungen) \n\u200B";
            DiLFGLvl2 = "**Trainee Level 2** (" + (Lvl2DiAnm-1) + "/1 Lvl2-Anmeldungen) \n\u200B";
            DiLFGLvl1 = "**Trainee Level 2** (" + (Lvl1DiAnm-1) + "/1 Lvl1-Anmeldungen) \n\u200B";
            DiLFGLvl0 = "**Trainee Level 2** (" + (Lvl0DiAnm-1) + "/1 Lvl0-Anmeldungen) \n\u200B";

            //build Ausgabesting
            for (int i = 0; i < ExpDiAnm || i < 7; i++)
                if (DiExp[2, i] == null)
                    DiLFGExp += " - \n";
                else
                    DiLFGExp += Emojichooser(ctx, DiExp[2, i]) + Emojichooser(ctx, DiExp[1, i]) + DiExp[0, i] + "\n";
            DiLFGExp += "\n\u200B\n\u200B";

            for (int i = 0; i < Lvl2DiAnm || i < 1; i++)
                if (DiLvl2[2, i] == null)
                    DiLFGLvl2 += " - \n";
                else
                    DiLFGLvl2 += Emojichooser(ctx, DiExp[2, i]) + Emojichooser(ctx, DiExp[1, i]) + DiExp[0, i] + "\n";
            DiLFGLvl2 += "\n\u200B\n\u200B";

            for (int i = 0; i < Lvl1DiAnm || i < 1; i++)
                if (DiLvl1[2, i] == null)
                    DiLFGLvl1 += " - \n";
                else
                    DiLFGLvl1 += Emojichooser(ctx, DiExp[2, i]) + Emojichooser(ctx, DiExp[1, i]) + DiExp[0, i] + "\n";
            DiLFGLvl1 += "\n\u200B\n\u200B";

            for (int i = 0; i < Lvl0DiAnm || i < 1; i++)
                if (DiLvl0[2, i] == null)
                    DiLFGLvl0 += " - \n";
                else
                    DiLFGLvl0 += Emojichooser(ctx, DiExp[2, i]) + Emojichooser(ctx, DiExp[1, i]) + DiExp[0, i] + "\n";
            DiLFGLvl0 += "\n\u200B\n\u200B";

            DiLFG = DiLFGExp + DiLFGLvl2 + DiLFGLvl1 + DiLFGLvl0;
            return DiLFG;
        }

        public string BuilderDo(CommandContext ctx)
        {
            string DoLFG = string.Empty;
            string[,] DoExp = new string[3, 30];
            string[,] DoLvl2 = new string[3, 30];
            string DoLFGExp = string.Empty;
            string DoLFGLvl2 = string.Empty;

            //neu einschreiben
            if (Anmeldung[1] == "Do")
            {
                DBOperations.EinschreibenDo(Anmeldung, ctx);
            }
            //auslesen
            var Data = DBOperations.AuslesenDo();
            DoExp = Data.Item1;
            DoLvl2 = Data.Item2;

            //ausgabestrin Builden noch machen
            DoLFGExp = "**Exp** (" + (ExpDoAnm-1) + "/8 Exp-Anmeldungen) \n\u200B";
            DoLFGLvl2 = "**Trainee Level 2** (" + (Lvl2DiAnm-1) + "/2 Lvl2-Anmeldungen) \n\u200B";

            //build Ausgabesting
            for (int i = 0; i < ExpDoAnm || i < 8; i++)
                if (DoExp[2, i] == null)
                    DoLFGExp += " - \n";
                else
                    DoLFGExp += Emojichooser(ctx, DoExp[2, i]) + Emojichooser(ctx, DoExp[1, i]) + DoExp[0, i] + "\n";
            DoLFGExp += "\n\u200B\n\u200B";

            for (int i = 0; i < Lvl2DiAnm || i < 2; i++)
                if (DoLvl2[2, i] == null)
                    DoLFGLvl2 += " - \n";
                else
                    DoLFGLvl2 += Emojichooser(ctx, DoExp[2, i]) + Emojichooser(ctx, DoExp[1, i]) + DoExp[0, i] + "\n";
            DoLFGLvl2 += "\n\u200B\n\u200B";

            DoLFG = DoLFGExp + DoLFGLvl2;
            return DoLFG;
        }

        public DiscordEmoji Emojichooser(CommandContext ctx, string emoji)
        {
            //benötigt beim LFG String-Bauen
            //Klassen
            var weaveremoji = DiscordEmoji.FromName(ctx.Client, ":Weaver:");
            var tempestemoji = DiscordEmoji.FromName(ctx.Client, ":Tempest:");
            var deadeyeemoji = DiscordEmoji.FromName(ctx.Client, ":Deadeye:");
            var daredevilemoji = DiscordEmoji.FromName(ctx.Client, ":Daredevil:");
            var soulbeastemoji = DiscordEmoji.FromName(ctx.Client, ":Soulbeast:");
            var druidemoji = DiscordEmoji.FromName(ctx.Client, ":Druid:");
            var holoemoji = DiscordEmoji.FromName(ctx.Client, ":Holosmith:");
            var scrapperemoji = DiscordEmoji.FromName(ctx.Client, ":Scrapper:");
            var renegadeemoji = DiscordEmoji.FromName(ctx.Client, ":Renegade:");
            var heraldemoji = DiscordEmoji.FromName(ctx.Client, ":Herald:");
            var reaperemoji = DiscordEmoji.FromName(ctx.Client, ":Reaper:");
            var scourgeemoji = DiscordEmoji.FromName(ctx.Client, ":Scourge:");
            var berserkeremoji = DiscordEmoji.FromName(ctx.Client, ":Berserker:");
            var spellbreakeremoji = DiscordEmoji.FromName(ctx.Client, ":Spellbreaker:");
            var firebrandemoji = DiscordEmoji.FromName(ctx.Client, ":Firebrand:");
            var dragonhunteremoji = DiscordEmoji.FromName(ctx.Client, ":Dragonhunter:");
            var mirageemoji = DiscordEmoji.FromName(ctx.Client, ":Mirage:");
            var chronoemoji = DiscordEmoji.FromName(ctx.Client, ":Chrono:");
            //Rollen
            var dpsemoji = DiscordEmoji.FromName(ctx.Client, ":dps:");
            var hybridemoji = DiscordEmoji.FromName(ctx.Client, ":hybrid:");
            var healemoji = DiscordEmoji.FromName(ctx.Client, ":healing:");
            //nix
            var questionemoji = DiscordEmoji.FromName(ctx.Client, ":questionmark:");
            //emojis

            switch (emoji)
            {
                case "Weaver":
                    return weaveremoji;
                case "Tempest":
                    return tempestemoji;
                case "Deadeye":
                    return deadeyeemoji;
                case "daredevil":
                    return daredevilemoji;
                case "Soulbeast":
                    return soulbeastemoji;
                case "Druid":
                    return druidemoji;
                case "Holosmith":
                    return holoemoji;
                case "Scrapper":
                    return scrapperemoji;
                case "Renegade":
                    return renegadeemoji;
                case "Herald":
                    return heraldemoji;
                case "Reaper":
                    return reaperemoji;
                case "Scourge":
                    return scourgeemoji;
                case "Berserker":
                    return berserkeremoji;
                case "Spellbreaker":
                    return spellbreakeremoji;
                case "Firebrand":
                    return firebrandemoji;
                case "Dragonhunter":
                    return dragonhunteremoji;
                case "Mirage":
                    return mirageemoji;
                case "Chrono":
                    return chronoemoji;
                case "Dps":
                    return dpsemoji;
                case "Hybrid":
                    return hybridemoji;
                case "Heal":
                    return healemoji;
                case "nix":
                    return questionemoji;
                default:
                    break;
            }
            return questionemoji;
        }

        [Command("anmelden")]
        [Description("Meldet dich für einen Raid an")]
        public async Task Anmelden(CommandContext ctx)
        {
            var Interactivy = ctx.Client.GetInteractivity();

            var rolelist = ctx.Member.Roles.ToList();

            var ExpRole = ctx.Guild.GetRole(763733958717734944);
            var Lvl2Role = ctx.Guild.GetRole(763733976262770688);
            var Lvl1Role = ctx.Guild.GetRole(763733994713251880);

            if (rolelist.Contains(ExpRole))
                Anmeldung[0] = "Exp";
            else if (rolelist.Contains(Lvl2Role))
                Anmeldung[0] = "Lvl2";
            else if (rolelist.Contains(Lvl1Role))
                Anmeldung[0] = "Lvl1";
            else
                Anmeldung[0] = "Lvl0";

            await ctx.Message.DeleteAsync().ConfigureAwait(false);

            var anmeldenemb = new DiscordEmbedBuilder
            {
                Title = "Anmeldung",
                Color = DiscordColor.Blue
            };

            var diemoji = DiscordEmoji.FromName(ctx.Client, ":dps:");
            var doemoji = DiscordEmoji.FromName(ctx.Client, ":mild_panic:");
            var nixemoji = DiscordEmoji.FromName(ctx.Client, ":nothing:");

            var emojis = diemoji + " - Dienstag\n" +
                doemoji + " - Donnerstag\n" +
                nixemoji + " - Abbrechen\n";

            anmeldenemb.AddField("Mögliche Tage:", emojis, false);
            anmeldenemb.WithFooter("Bitte Reagiere auf den Tag wo du dich anmelden willst");
            anmeldenemb.WithTimestamp(ctx.Message.Timestamp);

            var msg = await ctx.Channel.SendMessageAsync(embed: anmeldenemb).ConfigureAwait(false);

            await msg.CreateReactionAsync(diemoji).ConfigureAwait(false);
            await msg.CreateReactionAsync(doemoji).ConfigureAwait(false);
            await msg.CreateReactionAsync(nixemoji).ConfigureAwait(false);

            var reactionresult = await Interactivy.WaitForReactionAsync(
                x => x.Message == msg && //reaction must be on messeage
                x.User == ctx.User &&   //reaction mjust be from user
                (x.Emoji == diemoji || x.Emoji == doemoji)) //reaction must be one of the 2
                .ConfigureAwait(false);

            await msg.DeleteAsync().ConfigureAwait(false);

            if (reactionresult.Result.Emoji != nixemoji)
            {
                if (reactionresult.Result.Emoji == diemoji)
                {
                    Anmeldung[1] = "Di";
                    if (Anmeldung[0] == "Exp")
                        ExpDiAnm += 1;
                    else if (Anmeldung[0] == "Lvl2")
                        Lvl2DiAnm += 1;
                    else if (Anmeldung[0] == "Lvl1")
                        Lvl1DiAnm += 1;
                    else
                        Lvl0DiAnm += 1;
                    await Rolle(ctx);
                }
                else if (reactionresult.Result.Emoji == doemoji)
                {
                    if (rolelist.Contains(ExpRole) || rolelist.Contains(Lvl2Role))
                    {
                        Anmeldung[1] = "Do";
                        if (Anmeldung[0] == "Exp")
                            ExpDoAnm++;
                        else
                            Lvl2DoAnm += 1;
                        await Rolle(ctx);
                    }
                    else
                    {
                        msg = await ctx.Channel.SendMessageAsync("**Du kannst dich nicht anmelden da du noch kein Exp oder Trainee Lvl 2 bist.**").ConfigureAwait(false);
                        await Task.Delay(10000);
                        await msg.DeleteAsync().ConfigureAwait(false);
                    }
                }
            }
            else
            {
                msg = await ctx.Channel.SendMessageAsync("**Anmeldung Abgebrochen!**").ConfigureAwait(false);
                await Task.Delay(5000);
                await msg.DeleteAsync().ConfigureAwait(false);
                Array.Clear(Anmeldung, 0, Anmeldung.Length);
            }
        }

        [Command("abmelden")]
        [Description("Meldet dich für den angegeben Raid ab")]
        public async Task Abmelden(CommandContext ctx)
        {
            var Interactivy = ctx.Client.GetInteractivity();

            var rolelist = ctx.Member.Roles.ToList();

            var ExpRole = ctx.Guild.GetRole(763733958717734944);
            var Lvl2Role = ctx.Guild.GetRole(763733976262770688);
            var Lvl1Role = ctx.Guild.GetRole(763733994713251880);

            if (rolelist.Contains(ExpRole))
                Anmeldung[0] = "Exp";
            else if (rolelist.Contains(Lvl2Role))
                Anmeldung[0] = "Lvl2";
            else if (rolelist.Contains(Lvl1Role))
                Anmeldung[0] = "Lvl1";
            else
                Anmeldung[0] = "Lvl0";

            await ctx.Message.DeleteAsync().ConfigureAwait(false);

            var anmeldenemb = new DiscordEmbedBuilder
            {
                Title = "Abmeldung",
                Color = DiscordColor.Blue
            };

            var diemoji = DiscordEmoji.FromName(ctx.Client, ":dps:");
            var doemoji = DiscordEmoji.FromName(ctx.Client, ":mild_panic:");
            var nixemoji = DiscordEmoji.FromName(ctx.Client, ":nothing:");

            var emojis = diemoji + " - Dienstag\n" +
                doemoji + " - Donnerstag\n" +
                nixemoji + " - Abbrechen\n";

            anmeldenemb.AddField("Mögliche Tage:", emojis, false);
            anmeldenemb.WithFooter("Bitte Reagiere auf den Tag wo du dich abmelden willst");
            anmeldenemb.WithTimestamp(ctx.Message.Timestamp);

            var msg = await ctx.Channel.SendMessageAsync(embed: anmeldenemb).ConfigureAwait(false);

            await msg.CreateReactionAsync(diemoji).ConfigureAwait(false);
            await msg.CreateReactionAsync(doemoji).ConfigureAwait(false);
            await msg.CreateReactionAsync(nixemoji).ConfigureAwait(false);

            var reactionresult = await Interactivy.WaitForReactionAsync(
                x => x.Message == msg && //reaction must be on messeage
                x.User == ctx.User &&   //reaction mjust be from user
                (x.Emoji == diemoji || x.Emoji == doemoji)) //reaction must be one of the 2
                .ConfigureAwait(false);

            await msg.DeleteAsync().ConfigureAwait(false);

            if (reactionresult.Result.Emoji != nixemoji)
            {
                if (reactionresult.Result.Emoji == diemoji)
                {
                    if (Anmeldung[0] == "Exp")
                    {
                        ExpDiAnm -= 1;
                        AustragenDi(ctx, "Exp");
                    }
                    else if (Anmeldung[0] == "Lvl2")
                    {
                        Lvl2DiAnm -= 1;
                        AustragenDi(ctx, "Lvl2");
                    }
                    else if (Anmeldung[0] == "Lvl1")
                    {
                        Lvl2DiAnm -= 1;
                        AustragenDi(ctx, "Lvl1");
                    }
                    else
                    {
                        Lvl0DiAnm -= 1;
                        AustragenDi(ctx, "Lvl0");
                    }
                }
                else if (reactionresult.Result.Emoji == doemoji)
                {
                    if (rolelist.Contains(ExpRole) || rolelist.Contains(Lvl2Role))
                    {
                        if (Anmeldung[0] == "Exp")
                        {
                            ExpDoAnm -= 1;
                            AustragenDo(ctx, "Exp");
                        }
                        else
                        {
                            Lvl2DoAnm -= 1;
                            AustragenDo(ctx, "Lvl2");
                        }
                    }
                }
                msg = await ctx.Channel.SendMessageAsync("**Abmeldung Erfolgreich**").ConfigureAwait(false);
                await Task.Delay(5000);
                await msg.DeleteAsync().ConfigureAwait(false);
            }
            else
            {
                msg = await ctx.Channel.SendMessageAsync("**Abmeldung Abgebrochen!**").ConfigureAwait(false);
                await Task.Delay(5000);
                await msg.DeleteAsync().ConfigureAwait(false);
            }
            Array.Clear(Anmeldung, 0, Anmeldung.Length);
        }

        public int j = 0;

        public void AustragenDi(CommandContext ctx, string Level)
        {
            DBOperations.Deleter("Di", Level, ctx);
        }

        public void AustragenDo(CommandContext ctx, string Level)
        {
            DBOperations.Deleter("Do", Level, ctx);
        }

        public async Task Rolle(CommandContext ctx)
        {
            var anmeldenemb = new DiscordEmbedBuilder
            {
                Title = "Anmeldung",
                Color = DiscordColor.Blue
            };

            var dpsemoji = DiscordEmoji.FromName(ctx.Client, ":dps:");
            var hybridemoji = DiscordEmoji.FromName(ctx.Client, ":hybrid:");
            var healemoji = DiscordEmoji.FromName(ctx.Client, ":healing:");
            var nixemoji = DiscordEmoji.FromName(ctx.Client, ":nothing:");

            var emojis = dpsemoji + " - DPS (Condi/Power)\n" +
                hybridemoji + " - Hybrid\n" +
                healemoji + " - Healing\n" +
                nixemoji + " - Keine Preferenz\n";

            anmeldenemb.AddField("Mögliche Rollen:", emojis, false);
            anmeldenemb.WithFooter("Bitte Reagiere auf die Rolle mit der du dich anmelden willst");
            anmeldenemb.WithTimestamp(ctx.Message.Timestamp);

            var msg = await ctx.Channel.SendMessageAsync(embed: anmeldenemb).ConfigureAwait(false);

            await msg.CreateReactionAsync(dpsemoji).ConfigureAwait(false);
            await msg.CreateReactionAsync(hybridemoji).ConfigureAwait(false);
            await msg.CreateReactionAsync(healemoji).ConfigureAwait(false);
            await msg.CreateReactionAsync(nixemoji).ConfigureAwait(false);

            var Interactivy = ctx.Client.GetInteractivity();

            var reactionresult = await Interactivy.WaitForReactionAsync(
                x => x.Message == msg && //reaction must be on messeage
                x.User == ctx.User &&   //reaction mjust be from user
                (x.Emoji == dpsemoji || x.Emoji == hybridemoji || x.Emoji == healemoji || x.Emoji == nixemoji)) //reaction must be one of the 4
                .ConfigureAwait(false);

            await msg.DeleteAsync().ConfigureAwait(false);

            if (reactionresult.Result.Emoji == dpsemoji)
            {
                Anmeldung[2] = "Dps";
                await Klassse(ctx);
            }
            else if (reactionresult.Result.Emoji == hybridemoji)
            {
                Anmeldung[2] = "Hybrid";
                await Klassse(ctx);
            }
            else if (reactionresult.Result.Emoji == healemoji)
            {
                Anmeldung[2] = "Heal";
                await Klassse(ctx);
            }
            else if (reactionresult.Result.Emoji == nixemoji)
            {
                Anmeldung[2] = "Nix";
                await Klassse(ctx);
            }
        }
        public async Task Klassse(CommandContext ctx)
        {
            var klasseemb = new DiscordEmbedBuilder
            {
                Title = "Anmeldung",
                Color = DiscordColor.Blue
            };
            //alle Klassenemojis
            var weaveremoji = DiscordEmoji.FromName(ctx.Client, ":Weaver:");
            var tempestemoji = DiscordEmoji.FromName(ctx.Client, ":Tempest:");
            var deadeyeemoji = DiscordEmoji.FromName(ctx.Client, ":Deadeye:");
            var daredevilemoji = DiscordEmoji.FromName(ctx.Client, ":Daredevil:");
            var soulbeastemoji = DiscordEmoji.FromName(ctx.Client, ":Soulbeast:");
            var druidemoji = DiscordEmoji.FromName(ctx.Client, ":Druid:");
            var holoemoji = DiscordEmoji.FromName(ctx.Client, ":Holosmith:");
            var scrapperemoji = DiscordEmoji.FromName(ctx.Client, ":Scrapper:");
            var renegadeemoji = DiscordEmoji.FromName(ctx.Client, ":Renegade:");
            var heraldemoji = DiscordEmoji.FromName(ctx.Client, ":Herald:");
            var reaperemoji = DiscordEmoji.FromName(ctx.Client, ":Reaper:");
            var scourgeemoji = DiscordEmoji.FromName(ctx.Client, ":Scourge:");
            var berserkeremoji = DiscordEmoji.FromName(ctx.Client, ":Berserker:");
            var spellbreakeremoji = DiscordEmoji.FromName(ctx.Client, ":Spellbreaker:");
            var firebrandemoji = DiscordEmoji.FromName(ctx.Client, ":Firebrand:");
            var dragonhunteremoji = DiscordEmoji.FromName(ctx.Client, ":Dragonhunter:");
            var mirageemoji = DiscordEmoji.FromName(ctx.Client, ":Mirage:");
            var chronoemoji = DiscordEmoji.FromName(ctx.Client, ":Chrono:");
            var nixemoji = DiscordEmoji.FromName(ctx.Client, ":nothing:");

            //alle Klassenemojis
            var emojis = weaveremoji + " - Weaver\n" +
                tempestemoji + " - Tempest\n" +
                deadeyeemoji + " - Deadeye\n" +
                daredevilemoji + " - Daredevil\n" +
                soulbeastemoji + " - Soulbeast\n" +
                druidemoji + " - Druid\n" +
                holoemoji + " - Holosmith\n" +
                scrapperemoji + " - Scrapper\n" +
                renegadeemoji + " - Renegade\n" +
                heraldemoji + " - Herald\n" +
                reaperemoji + " - Reaper\n" +
                scourgeemoji + " - Scourge\n" +
                berserkeremoji + " - Berserker\n" +
                spellbreakeremoji + " - Spellbreaker\n" +
                firebrandemoji + " - Firebrand\n" +
                dragonhunteremoji + " - Dragonhunter\n" +
                mirageemoji + " - Mirage\n" +
                chronoemoji + " - Chrono\n" +
                nixemoji + " - Nix";

            klasseemb.AddField("Mögliche Klassen:", emojis, false);
            klasseemb.WithFooter("Bitte Reagiere auf die Klasse mit der du dich anmelden willst");
            klasseemb.WithTimestamp(ctx.Message.Timestamp);

            var msg = await ctx.Channel.SendMessageAsync(embed: klasseemb).ConfigureAwait(false);
            {
                await msg.CreateReactionAsync(weaveremoji).ConfigureAwait(false);
                await msg.CreateReactionAsync(tempestemoji).ConfigureAwait(false);
                await msg.CreateReactionAsync(deadeyeemoji).ConfigureAwait(false);
                await msg.CreateReactionAsync(daredevilemoji).ConfigureAwait(false);
                await msg.CreateReactionAsync(soulbeastemoji).ConfigureAwait(false);
                await msg.CreateReactionAsync(druidemoji).ConfigureAwait(false);
                await msg.CreateReactionAsync(holoemoji).ConfigureAwait(false);
                await msg.CreateReactionAsync(scrapperemoji).ConfigureAwait(false);
                await msg.CreateReactionAsync(renegadeemoji).ConfigureAwait(false);
                await msg.CreateReactionAsync(heraldemoji).ConfigureAwait(false);
                await msg.CreateReactionAsync(reaperemoji).ConfigureAwait(false);
                await msg.CreateReactionAsync(scourgeemoji).ConfigureAwait(false);
                await msg.CreateReactionAsync(berserkeremoji).ConfigureAwait(false);
                await msg.CreateReactionAsync(spellbreakeremoji).ConfigureAwait(false);
                await msg.CreateReactionAsync(firebrandemoji).ConfigureAwait(false);
                await msg.CreateReactionAsync(dragonhunteremoji).ConfigureAwait(false);
                await msg.CreateReactionAsync(mirageemoji).ConfigureAwait(false);
                await msg.CreateReactionAsync(chronoemoji).ConfigureAwait(false);
                await msg.CreateReactionAsync(nixemoji).ConfigureAwait(false);
            }
            var Interactivy = ctx.Client.GetInteractivity();

            var reactionresult = await Interactivy.WaitForReactionAsync(
                x => x.Message == msg && //reaction must be on messeage
                x.User == ctx.User)//reaction mjust be from user
                .ConfigureAwait(false);

            await msg.DeleteAsync().ConfigureAwait(false);

            switch (reactionresult.Result.Emoji.Name)
            {
                case "Weaver":
                    Anmeldung[3] = "Waever";
                    break;
                case "Tempest":
                    Anmeldung[3] = "Tempest";
                    break;
                case "Deadeye":
                    Anmeldung[3] = "Deadeye";
                    break;
                case "Daredevil":
                    Anmeldung[3] = "Daredevil";
                    break;
                case "Soulbeast":
                    Anmeldung[3] = "Soulbeast";
                    break;
                case "Druid":
                    Anmeldung[3] = "Druid";
                    break;
                case "Holosmith":
                    Anmeldung[3] = "Holosmith";
                    break;
                case "Scrapper":
                    Anmeldung[3] = "Scrapper";
                    break;
                case "Renegade":
                    Anmeldung[3] = "Renegade";
                    break;
                case "Herald":
                    Anmeldung[3] = "Hearld";
                    break;
                case "Reaper":
                    Anmeldung[3] = "Reaper";
                    break;
                case "Scourge":
                    Anmeldung[3] = "Scourge";
                    break;
                case "Berserker":
                    Anmeldung[3] = "Berserker";
                    break;
                case "Spellbreaker":
                    Anmeldung[3] = "Spellbreaker";
                    break;
                case "Firebrand":
                    Anmeldung[3] = "Firebrand";
                    break;
                case "Dragonhunter":
                    Anmeldung[3] = "Dragonhunter";
                    break;
                case "Mirage":
                    Anmeldung[3] = "Mirage";
                    break;
                case "Chrono":
                    Anmeldung[3] = "Chrono";
                    break;
                case "nothing":
                    Anmeldung[3] = "nix";
                    break;
                default:
                    break;
            }

            //altes LFG löschen
            var msgs = await ctx.Channel.GetMessagesAsync(1).ConfigureAwait(false);
            await ctx.Channel.DeleteMessageAsync(msgs[0]).ConfigureAwait(false);

            var LFGemb = LFGBuilder(ctx);
            await ctx.Channel.SendMessageAsync(embed: LFGemb).ConfigureAwait(false);

            msg = await ctx.Channel.SendMessageAsync("**Anmelden erfolgreich**\n").ConfigureAwait(false); //Hier noch anfügen als was angemeldet
            await Task.Delay(5000);
            await msg.DeleteAsync().ConfigureAwait(false);
        }
        public DiscordEmbed LFGBuilder(CommandContext ctx)
        {
            var LFGemb = new DiscordEmbedBuilder
            {
                Title = "LFG",
                Color = DiscordColor.Green,
            };

            LFGemb.AddField("__Dienstag__\n\u200B", BuilderDi(ctx), false);
            LFGemb.AddField("**\u200B**", "**\u200B\n ----------------------------------------------------- \n\u200B\n\u200B**", false);
            LFGemb.AddField("__Donnerstag__\n\u200B", BuilderDo(ctx), false);

            return LFGemb;
        }

        [Command("manuell")]
        [Description("Deletes Old LFG and Creates a New one (with same Players)")]
        //[RequireRoles(RoleCheckMode.Any, "FooiBotMod")]
        public async Task Manuell(CommandContext ctx)
        {
            Array.Clear(Anmeldung, 0, Anmeldung.Length);
            await ctx.Message.DeleteAsync().ConfigureAwait(false);
            var msg = await ctx.Channel.GetMessagesAsync(1);
            await ctx.Channel.DeleteMessageAsync(msg[0]);
            var LFG = LFGBuilder(ctx);
            await ctx.Channel.SendMessageAsync(embed: LFG).ConfigureAwait(false);
        }

        [Command("empty")]
        [Description("Deletes all Players from specified Raid (\"Di\"/\"Do\")")]
        //[RequireRoles(RoleCheckMode.Any, "FooiBotMod")]
        public async Task Delete(CommandContext ctx,
            [Description("Day on wich all Players are removed from the LFG(\"Di\"/\"Do\")")] string Tag)
        {
            await ctx.Message.DeleteAsync().ConfigureAwait(false);
            var msg = await ctx.Channel.GetMessagesAsync(1);
            await ctx.Channel.DeleteMessageAsync(msg[0]);

            if (Tag == "Di")
            {
                DBOperations.LeererDi();
                ExpDiAnm = 1;
                Lvl2DiAnm = 1;
                Lvl1DiAnm = 1;
                Lvl0DiAnm = 1;
            }
            if (Tag == "Do")
            {
                DBOperations.LeererDo();
                ExpDoAnm = 1;
                Lvl2DoAnm = 1;
            }

            var lfgemb = LFGBuilder(ctx);
            await ctx.Channel.SendMessageAsync(embed: lfgemb).ConfigureAwait(false);

        }
    }
}