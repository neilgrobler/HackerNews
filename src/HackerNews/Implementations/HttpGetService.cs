using System.Net.Http;
using System.Threading.Tasks;
using HackerNews.Interfaces;

namespace HackerNews.Implementations
{
    public class HttpGetService : IHttpGetService
    {
        private readonly HttpClient _client;

        public HttpGetService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
        }

        public async Task<string> Get(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}