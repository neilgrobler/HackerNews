using System.Collections.Generic;
using System.Threading.Tasks;
using HackerNews.Config;
using HackerNews.DataObjects;
using HackerNews.Interfaces;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HackerNews.Implementations
{
    public class Scraper : IScraper
    {
        private readonly string _host;
        private readonly IHttpGetService _httpGetService;
        private readonly INextPageUrlParser _nextPageUrlParser;
        private readonly INodeCounter _nodeCounter;
        private readonly IPostParser _postParser;


        public Scraper(IOptions<HackerNewsOptions> options, IHttpGetService httpGetService, INodeCounter nodeCounter,
            IPostParser postParser, INextPageUrlParser nextPageUrlParser)
        {
            _host = options.Value.Host;
            _httpGetService = httpGetService;
            _nodeCounter = nodeCounter;
            _postParser = postParser;
            _nextPageUrlParser = nextPageUrlParser;
        }

        public async Task<string> ScrapeAsync(int count)
        {
            var output = new List<Post>();
            var url = _host;

            do
            {
                // Fetch the HTML
                var html = await _httpGetService.Get(url);

                // Load the HTML document
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);

                // Get the number of items
                var nodeCount = _nodeCounter.CountNodes(htmlDoc);

                // Make sure we received some posts
                if (nodeCount == 0)
                    break;

                // Iterate through the items in the page
                for (var index = 1; index <= nodeCount && output.Count < count; index++)
                    output.Add(_postParser.Parse(htmlDoc, index));

                // Get the URL for the next page
                url = _nextPageUrlParser.Parse(htmlDoc);
            } while (output.Count < count && !string.IsNullOrWhiteSpace(url));

            // Serialize the output
            return JsonConvert.SerializeObject(output);
        }
    }
}