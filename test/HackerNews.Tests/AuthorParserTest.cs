using System.IO;
using HackerNews.Implementations.Parsers;
using HtmlAgilityPack;
using NUnit.Framework;

namespace HackerNews.Tests
{
    public class AuthorParserTest
    {
        [Test]
        public void CanParseValidNode()
        {
            var doc = LoadHtmlDocument(@"TestData\Normal.html");

            var output = new AuthorParser().Parse(doc, 1);

            Assert.AreEqual("Test User", output);
        }

        [Test]
        public void InsertsDefaultValueWhenNodeIsEmpty()
        {
            var doc = LoadHtmlDocument(@"TestData\EmptyValues.html");

            var output = new AuthorParser().Parse(doc, 1);

            Assert.AreEqual("Anonymous", output);
        }

        [Test]
        public void InsertsDefaultValueWhenNodeNotFound()
        {
            var doc = LoadHtmlDocument(@"TestData\NoPosts.html");

            var output = new AuthorParser().Parse(doc, 1);

            Assert.AreEqual("Anonymous", output);
        }

        [Test]
        public void TruncatesLongValue()
        {
            var doc = LoadHtmlDocument(@"TestData\LongValues.html");

            var output = new AuthorParser().Parse(doc, 1);

            Assert.IsTrue(output.StartsWith("User 0123456789"));
            Assert.AreEqual(256, output.Length);
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