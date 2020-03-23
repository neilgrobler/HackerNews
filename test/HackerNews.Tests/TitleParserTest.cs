using System.IO;
using HackerNews.Implementations.Parsers;
using HtmlAgilityPack;
using NUnit.Framework;

namespace HackerNews.Tests
{
    public class TitleParserTest
    {
        [Test]
        public void CanParseValidNode()
        {
            var doc = LoadHtmlDocument(@"TestData\Normal.html");

            var output = new TitleParser().Parse(doc, 1);

            Assert.AreEqual("Test Title", output);
        }

        [Test]
        public void InsertsDefaultValueWhenNodeIsEmpty()
        {
            var doc = LoadHtmlDocument(@"TestData\EmptyValues.html");

            var output = new TitleParser().Parse(doc, 1);

            Assert.AreEqual("Not specified", output);
        }

        [Test]
        public void InsertsDefaultValueWhenNodeNotFound()
        {
            var doc = LoadHtmlDocument(@"TestData\NoPosts.html");

            var output = new TitleParser().Parse(doc, 1);

            Assert.AreEqual("Not specified", output);
        }

        [Test]
        public void TruncatesLongValue()
        {
            var doc = LoadHtmlDocument(@"TestData\LongValues.html");

            var output = new TitleParser().Parse(doc, 1);

            Assert.IsTrue(output.StartsWith("Title 0123456789"));
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