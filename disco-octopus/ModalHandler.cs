using Discord.WebSocket;
using System.Text.RegularExpressions;

namespace disco_octopus
{
    internal class ModalHandler
    {
        public async Task ResolveModal(SocketModal modal)
        {
            if (modal.Data.CustomId == "format_logs")
            {
                await HandleFormatLogs(modal);
            }
        }

        private async Task HandleFormatLogs(SocketModal modal)
        {
            // Get the values of components.
            List<SocketMessageComponentData> components =
                        modal.Data.Components.ToList();

            string logs = components
                .First(x => x.CustomId == "logs").Value;

            string pattern = @"(\[[^ ]*\])*<?([A-Z][a-z|'|-]* [A-Z][a-z|'|-]*)([A-Z][a-z]*)?>?(.*)";
            string result = Regex.Replace(logs, pattern, "$2$4");
            await modal.RespondAsync(result, ephemeral: true);
        }
    }
}
