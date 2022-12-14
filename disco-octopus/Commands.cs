using disco_octopus.HttpClients;
using Discord.Interactions;
using Discord;

namespace disco_octopus
{
    public class Commands : InteractionModuleBase<SocketInteractionContext>
    {
        private IHawkingAPIClient _api;

        public Commands(IHawkingAPIClient api)
        {
            _api = api;
        }

        [SlashCommand("countdown", "Generates fancy time text, in hours and minutes from current time"), EnabledInDm(true)]
        public async Task Countdown(int hour, int minute)
        {
            long unixTimestamp = DateTimeOffset.UtcNow.AddHours(hour).AddMinutes(minute).ToUnixTimeSeconds();

            await RespondAsync(embed: EmbedHelper.GenerateTimeEmbed(unixTimestamp), ephemeral: true);
        }

        [SlashCommand("exactly", "Generates fancy time text, based on the time you give it."), EnabledInDm(true)]
        public async Task Exactly(string time)
        {
            HawkingResponse? hawkingResponse = await _api.FindDatesFromString(time);
            // I'm not taking chances
            string? utcTime = hawkingResponse?.ParserOutputs?.FirstOrDefault()?.DateRange?.Start;
            if (String.IsNullOrWhiteSpace(utcTime))
            {
                await RespondAsync($"I didn't understand that time", ephemeral: true);
            }
            else
            {
                long unixTimestamp = DateTimeOffset.Parse(utcTime).ToUnixTimeSeconds();

                await RespondAsync(embed: EmbedHelper.GenerateTimeEmbed(unixTimestamp), ephemeral: true);
            }
        }

        [SlashCommand("formatlogs", "Takes logs from ffxiv in-game chat and formats them to be prettier. Press enter to get started."), EnabledInDm(true)]
        public async Task FormatLogs()
        {
            var mb = new ModalBuilder()
            .WithTitle("Log Formatter")
            .WithCustomId("format_logs")
            .AddTextInput("Logs", "logs", TextInputStyle.Paragraph, "Silvaire AmeillaudLeviathan: Silvaire Ameillaud; of the Montroux Ameillauds.");

            await Context.Interaction.RespondWithModalAsync(mb.Build());
        }
    }
}
