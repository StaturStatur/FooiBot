using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FooiBot.Commands
{
    class UtilityCommands : BaseCommandModule
    {
        /// <summary>
        /// hier ist mein kommentar
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [Command("ping")]
        [Description("Returns Pong")]
        [RequireRoles(RoleCheckMode.All, "FooiBotMod")]
        public async Task Ping(CommandContext ctx)
        {
            var pingstr = ctx.Member.Mention;
            await ctx.Channel.SendMessageAsync(pingstr + " Pong!").ConfigureAwait(false);
        }

        //mentioning
        [Command("PingediPong")]
        [Description("PiNg!1!1!!!11!3")]
        public async Task Pingedipong(CommandContext ctx)
        {
            for (int i = 0; i < 5; i++)
            {
                await ctx.Channel
                    .SendMessageAsync(ctx.User.Mention)
                    .ConfigureAwait(false);
            }
        }

        //rollen auslesen und damit was machen
        [Command("firstreact")]
        public async Task Response(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            //rollen des absenders abfragen
            var liste = ctx.Member.Roles.ToList();
            //erlaubte rolle holen
            var role = ctx.Guild.GetRole(763274113984626688);
            //wenn nicht hat dann beschweren o.o
            if (liste.Contains(role) != true)
                await ctx.Channel.SendMessageAsync("Nix da!").ConfigureAwait(false);
            else
            {
                await ctx.Channel.SendMessageAsync("react bitch").ConfigureAwait(false);

                var msg = await interactivity.WaitForReactionAsync(x => x.Channel == ctx.Channel && x.User == ctx.User).ConfigureAwait(false);

                await ctx.Channel.SendMessageAsync(msg.Result.Emoji).ConfigureAwait(false);
            }
        }

        //rollen vergeben
        [Command("join")]
        public async Task Join(CommandContext ctx)
        {
            var joinEmbed = new DiscordEmbedBuilder
            {
                Title = "Would you like to take the Role",
                //ThumbnailUrl = ctx.Client.CurrentUser.AvatarUrl,
                Color = DiscordColor.Green
            };

            var joinMesseage =
                await ctx.Channel.SendMessageAsync(embed: joinEmbed).ConfigureAwait(false);

            var thumbsupemoji = DiscordEmoji.FromName(ctx.Client, ":+1:");
            var thumbsdownemoji = DiscordEmoji.FromName(ctx.Client, ":-1:");

            await joinMesseage.CreateReactionAsync(thumbsupemoji).ConfigureAwait(false);
            await joinMesseage.CreateReactionAsync(thumbsdownemoji).ConfigureAwait(false);

            var interactivity = ctx.Client.GetInteractivity();

            var reactionresult = await interactivity.WaitForReactionAsync(
                x => x.Message == joinMesseage && //reaction must be on messeage
                x.User == ctx.User &&
                (x.Emoji == thumbsupemoji || x.Emoji == thumbsdownemoji)).ConfigureAwait(false); //reaction either up or down emoji
            if (reactionresult.Result.Emoji == thumbsupemoji)
            {
                var role = ctx.Guild.GetRole(763274113984626688);
                await ctx.Member.GrantRoleAsync(role).ConfigureAwait(false);
            }

            await joinMesseage.DeleteAsync().ConfigureAwait(false);
        }

        //reaktionen auslesen
        [Command("poll")]
        public async Task Poll(CommandContext ctx, TimeSpan duration, params DiscordEmoji[] emojioptions)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var options = emojioptions.Select(x => x.ToString());

            var pollembed = new DiscordEmbedBuilder
            {
                Title = "Poll",
                Description = string.Join(" ", options)
            };

            var pollmsg = await ctx.Channel.SendMessageAsync(embed: pollembed).ConfigureAwait(false);

            foreach (var option in emojioptions)
            {
                await pollmsg.CreateReactionAsync(option).ConfigureAwait(false);
            }

            var result = await interactivity.CollectReactionsAsync(pollmsg, duration).ConfigureAwait(false);
            var distinctresult = result.Distinct();

            var results = distinctresult.Select(x => $"{x.Emoji}: {x.Total}");

            await ctx.Channel.SendMessageAsync(string.Join("\n", results)).ConfigureAwait(false);
        }

    }
}
