using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using System.Reflection;

namespace disco_octopus
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly InteractionService _intSvc;
        private readonly IServiceProvider _svcProvider;

        public CommandHandler(DiscordSocketClient client, InteractionService intSvc, IServiceProvider svcProvider)
        {
            _client = client;
            _intSvc = intSvc;
            _svcProvider = svcProvider;
        }

        public async Task InitializeAsync()
        {
            await _intSvc.AddModulesAsync(Assembly.GetEntryAssembly(), _svcProvider);
            _client.InteractionCreated += HandleInteraction;
        }

        private async Task HandleInteraction(SocketInteraction arg)
        {
            try
            {
                var context = new SocketInteractionContext(_client, arg);
                await _intSvc.ExecuteCommandAsync(context, _svcProvider);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                if (arg.Type == InteractionType.ApplicationCommand)
                {
                    await arg.GetOriginalResponseAsync().ContinueWith(async (msg) => await msg.Result.DeleteAsync());
                }
            }
        }
    }
}
