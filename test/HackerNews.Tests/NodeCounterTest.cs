using System.IO;
using HackerNews.Implementations;
using HtmlAgilityPack;
using NUnit.Framework;

namespace HackerNews.Tests
{
    public class NodeCounterTest
    {
        [Test]
        public void CanCountValidNodes()
        {
            var doc = LoadHtmlDocument(@"TestData\Normal.html");

            var output = new NodeCounter().CountNodes(doc);

            Assert.AreEqual(1, output);
        }

        [Test]
        public void ReturnsZeroWhenNoNodesFound()
        {
            var doc = LoadHtmlDocument(@"TestData\NoPosts.html");

            var output = new NodeCounter().CountNodes(doc);

            Assert.AreEqual(0, output);
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