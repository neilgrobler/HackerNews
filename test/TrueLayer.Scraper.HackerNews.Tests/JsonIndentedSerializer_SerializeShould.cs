using HackerNews.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrueLayer.Scraper.HackerNews.DataObjects;

namespace TrueLayer.Scraper.HackerNews.Tests
{
    [TestClass]
    public class JsonIndentedSerializer_SerializeShould
    {
        private const string IndentedJson =
            "{\r\n  \"Title\": \"title\",\r\n  \"Uri\": \"https://example.com\",\r\n  \"Author\": \"author\",\r\n  \"Points\": 2,\r\n  \"Comments\": 1,\r\n  \"Rank\": 3\r\n}";

        private readonly JsonIndentedSerializer _serializer;

        public JsonIndentedSerializer_SerializeShould()
        {
            _serializer = new JsonIndentedSerializer();
        }

        private Post CreatePost()
        {
            return new Post
            {
                Author = "author",
                Uri = "https://example.com",
                Title = "title",
                Comments = 1,
                Points = 2,
                Rank = 3
            };
        }

        [TestMethod]
        public void Serialize_InputIsObject_ReturnIndentedJsonString()
        {
            var post = CreatePost();

            var serialized = _serializer.Serialize(post);

            Assert.AreEqual(IndentedJson, serialized);
        }
    }
}