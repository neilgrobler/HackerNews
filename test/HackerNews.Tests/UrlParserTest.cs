using System.IO;
using HackerNews.Implementations.Parsers;
using HtmlAgilityPack;
using NUnit.Framework;

namespace HackerNews.Tests
{
    public class UrlParserTest
    {
        [Test]
        public void CanParseValidNode()
        {
            var doc = LoadHtmlDocument(@"TestData\Normal.html");

            var output = new UrlParser().Parse(doc, 1);

            Assert.AreEqual("https://example.com/", output);
        }

        [Test]
        public void InsertsDefaultValueWhenNodeIsEmpty()
        {
            var doc = LoadHtmlDocument(@"TestData\EmptyValues.html");

            var output = new UrlParser().Parse(doc, 1);

            Assert.AreEqual(string.Empty, output);
        }

        [Test]
        public void InsertsDefaultValueWhenNodeNotFound()
        {
            var doc = LoadHtmlDocument(@"TestData\NoPosts.html");

            var output = new UrlParser().Parse(doc, 1);

            Assert.AreEqual(string.Empty, output);
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