using System.IO;
using System.Threading.Tasks;
using HackerNews.Classes;
using HackerNews.Implementations;
using HackerNews.Implementations.Parsers;
using HackerNews.Interfaces;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace HackerNews.Tests
{
    public class ScraperTest
    {
        private const string Host = "https://example.com/";

        [Test]
        public void CanRunEndToEndWithPosts()
        {
            var html = File.ReadAllText(@"TestData\Normal.html");

            var scraper = CreateScraper(html);

            var output = scraper.ScrapeAsync(3).Result;

            Assert.AreEqual(JsonConvert.SerializeObject(new[] {CreatePost(), CreatePost(), CreatePost()}), output);
        }

        [Test]
        public void CanRunEndToEndWithoutPosts()
        {
            var html = File.ReadAllText(@"TestData\NoPosts.html");

            var scraper = CreateScraper(html);

            var output = scraper.ScrapeAsync(3).Result;

            Assert.AreEqual("[]", output);
        }

        private static Scraper CreateScraper(string html)
        {
            var options = Options.Create(new HackerNewsOptions
            {
                Host = Host
            });

            var httpGetService = new Mock<IHttpGetService>();
            var nodeCounter = new NodeCounter();
            var postParser = new PostParser(new TitleParser(), new UrlParser(), new AuthorParser(), new RankParser(),
                new CommentsParser(), new PointsParser());
            var nextPageUrlParser = new NextPageUrlParser(options);

            httpGetService.Setup(m => m.Get(It.IsAny<string>())).Returns(Task.FromResult(html));


            var scraper = new Scraper(options, httpGetService.Object, nodeCounter, postParser, nextPageUrlParser);
            return scraper;
        }

        private static Post CreatePost()
        {
            return new Post
            {
                Author = "Test User",
                Uri = "https://example.com/",
                Title = "Test Title",
                Points = 99,
                Comments = 50,
                Rank = 1
            };
        }
    }
}