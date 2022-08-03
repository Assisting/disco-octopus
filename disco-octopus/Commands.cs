using Discord.Interactions;

namespace disco_octopus
{
    public class Commands : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("countdown", "Generates fancy time text, in hours and minutes from current time"), EnabledInDm(true)]
        public async Task Countdown(int hour, int minute)
        {
            long unixTimestamp = DateTimeOffset.UtcNow.AddHours(hour).AddMinutes(minute).ToUnixTimeSeconds();

            await RespondAsync($"`<t:{unixTimestamp}>`", ephemeral: true);
        }
    }
}
