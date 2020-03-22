using System.Collections.Generic;
using System.Threading.Tasks;
using HackerNews.Interfaces;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HackerNews.Implementations
{
    public partial class PostScraper : IPostScraper
    {
        private readonly IConfiguration _configuration;
        private readonly INodeCounter _nodeCounter;
        private readonly IPostParser _postParser;
        private readonly INextPageUrlParser _nextPageUrlParser;
        private readonly HtmlWeb _htmlParser;


        public PostScraper(IConfiguration configuration, INodeCounter nodeCounter, IPostParser postParser, INextPageUrlParser nextPageUrlParser)
        {
            _configuration = configuration;
            _nodeCounter = nodeCounter;
            _postParser = postParser;
            _nextPageUrlParser = nextPageUrlParser;
            _htmlParser = new HtmlWeb();
        }

        public async Task<string> ScrapeAsync(int count)
        {
            var output = new List<Post>();
            var url = _configuration.GetValue<string>("Host");

            do
            {
                // Fetch the HTML
                var htmlDoc = await _htmlParser.LoadFromWebAsync(url);

                // Get the number of items
                var nodeCount = _nodeCounter.CountNodes(htmlDoc);

                // Make sure we received some posts
                if (nodeCount == 0)
                    break;

                // Iterate through the items in the page
                for (var index = 1; index <= nodeCount && output.Count < count; index++)
                {
                    output.Add(_postParser.Parse(htmlDoc, index));
                }

                // Get the URL for the next page
                url = _nextPageUrlParser.Parse(htmlDoc);
            } while (output.Count < count && !string.IsNullOrWhiteSpace(url));

            return JsonConvert.SerializeObject(output);
        }
    }
}