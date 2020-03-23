using System.IO;
using HackerNews.Config;
using HackerNews.Implementations.Parsers;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace HackerNews.Tests
{
    public class NextPageUrlParserTest
    {
        private const string Host = "https://example.com/";
        [Test]
        public void CanParseValidNode()
        {
            var doc = LoadHtmlDocument(@"TestData\Normal.html");

            

            var options = Options.Create(new HackerNewsOptions
            {
                Host = Host
            });

            var output = new NextPageUrlParser(options).Parse(doc);

            Assert.AreEqual($"{Host}news?p=2", output);
        }

        [Test]
        public void InsertsDefaultValueWhenNodeIsEmpty()
        {
            var doc = LoadHtmlDocument(@"TestData\EmptyValues.html");

            var options = Options.Create(new HackerNewsOptions
            {
                Host = Host
            });

            var output = new NextPageUrlParser(options).Parse(doc);

            Assert.IsNull(output);
        }

        [Test]
        public void InsertsDefaultValueWhenNodeNotFound()
        {
            var doc = LoadHtmlDocument(@"TestData\NoPosts.html");

            var options = Options.Create(new HackerNewsOptions
            {
                Host = Host
            });

            var output = new NextPageUrlParser(options).Parse(doc);

            Assert.IsNull(output);
        }


        private HtmlDocument LoadHtmlDocument(string path)
        {
            var html = File.ReadAllText(path);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc;
        }
    }
}