using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disco_octopus
{
    public class EmbedHelper
    {
        public static Embed GenerateTimeEmbed(long unixSeconds)
        {
            EmbedBuilder embedBuilder = new EmbedBuilder();
            embedBuilder.WithColor(new Color(239, 228, 225))
                        .WithTitle($"Time Code: `<t:{unixSeconds}>`")
                        .WithDescription($"\nThis is the time code for <t:{unixSeconds}>.\n\n" +
                                         $"Copy/paste this time code into a Discord message, and everyone who looks at it will see the time in their local time zone.\n\n" +
                                         $"**Example:** If you send a message:\n" +
                                         $"> `Let's meet up at <t:{unixSeconds}>!`\n\n" +
                                         $"The message will display as:\n" +
                                         $"> Let's meet up at <t:{unixSeconds}>!");

            return embedBuilder.Build();
        }
    }
}
