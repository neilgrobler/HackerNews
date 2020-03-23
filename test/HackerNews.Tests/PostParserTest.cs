using HackerNews.DataObjects;
using HackerNews.Implementations.Parsers;
using HackerNews.Interfaces;
using HtmlAgilityPack;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace HackerNews.Tests
{
    public class PostParserTest
    {
        [Test]
        public void Test()
        {
            var index = 1;
            var htmlDocument = new HtmlDocument();

            var titleParser = new Mock<ITitleParser>();
            var urlParser = new Mock<IUrlParser>();
            var authorParser = new Mock<IAuthorParser>();
            var rankParser = new Mock<IRankParser>();
            var commentsParser = new Mock<ICommentsParser>();
            var pointsParser = new Mock<IPointsParser>();

            var post = new Post
            {
                Title = "title",
                Uri = "https://example.com",
                Author = "author",
                Rank = 3,
                Comments = 2,
                Points = 1
            };

            titleParser.Setup(m => m.Parse(htmlDocument, index)).Returns(post.Title);
            urlParser.Setup(m => m.Parse(htmlDocument, index)).Returns(post.Uri);
            authorParser.Setup(m => m.Parse(htmlDocument, index)).Returns(post.Author);
            rankParser.Setup(m => m.Parse(htmlDocument, index)).Returns(post.Rank);
            commentsParser.Setup(m => m.Parse(htmlDocument, index)).Returns(post.Comments);
            pointsParser.Setup(m => m.Parse(htmlDocument, index)).Returns(post.Points);

            var output = new PostParser(titleParser.Object, urlParser.Object, authorParser.Object, rankParser.Object,
                commentsParser.Object, pointsParser.Object).Parse(htmlDocument, index);

            Assert.AreEqual(JsonConvert.SerializeObject(post), JsonConvert.SerializeObject(output));
        }
    }
}