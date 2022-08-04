using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace disco_octopus
{
    class Program
    {
        private DiscordSocketClient? _client;
        private InteractionService? _intSvc;

        public static Task Main(string[] args) => new Program().MainAsync();

        public async Task MainAsync()
        {
            using (var services = ConfigureServices())
            {
                var client = services.GetRequiredService<DiscordSocketClient>();
                var intSvc = services.GetRequiredService<InteractionService>();
                _client = client;
                _intSvc = intSvc;

                client.Log += LogAsync;
                intSvc.Log += LogAsync;
                client.Ready += ReadyAsync;

                string token = Environment.GetEnvironmentVariable("DiscordBotToken") ?? "";
                await client.LoginAsync(TokenType.Bot, token);
                await client.StartAsync();
                await services.GetRequiredService<CommandHandler>().InitializeAsync();

                await Task.Delay(Timeout.Infinite);
            }
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private async Task ReadyAsync()
        {
            if (_intSvc == null)
            {
                throw new ArgumentNullException("InteractionService cannot be null.");
            }
            await _intSvc.RegisterCommandsGloballyAsync();
            Console.WriteLine("Registered global commands");

            if (_client == null)
            {
                throw new ArgumentNullException("DiscordSocketClient cannot be null.");
            }
            Console.WriteLine($"Connected user {_client.CurrentUser}");
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
                .AddSingleton<CommandHandler>()
                .BuildServiceProvider();
        }
    }
}