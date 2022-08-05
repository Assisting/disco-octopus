using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace disco_octopus.HttpClients
{
    public class HawkingAPIClient : IHawkingAPIClient
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOps;

        public HawkingAPIClient(HttpClient client)
        {
            _client = client;
            _jsonOps = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }

        public async Task<HawkingResponse?> FindDatesFromString(string inputText)
        {
            HttpContent content = new StringContent(inputText);
            var response = await _client.PostAsync("/api", content);

            if (response.IsSuccessStatusCode)
            {
                HawkingResponse? hawkingResponse = await JsonSerializer.DeserializeAsync<HawkingResponse>(response.Content.ReadAsStream(), _jsonOps);
                return hawkingResponse;
            }
            else
            {
                return null;
            }
        }
    }
}
