using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HackerNews.Interfaces;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using TrueLayer.Scraper.HackerNews.DataObjects;

namespace HackerNews.Classes
{
    public class PostScraper : IPostScraper
    {
        private readonly HtmlWeb _htmlParser;
        private readonly ISerializer _serializer;
        private readonly HackerNewsOptions _options;

        public PostScraper(IOptions<HackerNewsOptions> options, ISerializer serializer)
        {
            _htmlParser = new HtmlWeb();
            _serializer = serializer;
            _options = options.Value;
        }
        public async Task<string> ScrapeAsync(int count)
        {
            if (count < 0 || count > 100)
                throw new ArgumentOutOfRangeException(nameof(count), count, "Parameter must be a value > 0 and < 100.");

            var output = new List<Post>();
            var url = _options.Host;

            do
            {
                // Fetch the HTML
                var htmlDoc = await _htmlParser.LoadFromWebAsync(url);

                // Get the number of items
                var nodeCount = htmlDoc.DocumentNode.SelectNodes(_options.ArticleXPath).Count;

                // Make sure we received some posts
                if (nodeCount == 0)
                    break;

                // Iterate through the items in the page
                for (var index = 1; index <= nodeCount && output.Count < count; index++)
                {
                    var indexedArticleXPath = $"{_options.ArticleXPath}[{index}]";
                    var indexedSubtextXPath = $"{indexedArticleXPath}{_options.SubtextXPath}";

                    var post = new Post();

                    // Get the <a></a> containing the title
                    var titleNode = htmlDoc.DocumentNode.SelectSingleNode($"{indexedArticleXPath}{_options.TitleXPath}");

                    post.Title = titleNode == null 
                        ? "Not supplied" 
                        : ParseText(titleNode.InnerText, "Not supplied");

                    // Get the <a></a> containing the URL
                    var storyLink = htmlDoc.DocumentNode.SelectSingleNode($"{indexedArticleXPath}{_options.UrlXPath}");

                    post.Uri = storyLink == null ? string.Empty : ParseUri(storyLink.Attributes["href"].Value);

                    // Get the <a></a> containing the author
                    var user = htmlDoc.DocumentNode.SelectSingleNode($"{indexedSubtextXPath}{_options.AuthorXPath}");

                    post.Author = user == null ? "Anonymous" : ParseText(user.InnerText, "Anonymous");

                    // Get the <span></span> containing the rank
                    var rank = htmlDoc.DocumentNode
                        .SelectSingleNode($"{indexedArticleXPath}{_options.RankXPath}");

                    post.Rank = rank == null ? 0 : ParsePositiveInteger(rank.InnerText);

                    // Get the <span></span> containing the score
                    var score = htmlDoc.DocumentNode
                        .SelectSingleNode($"{indexedSubtextXPath}{_options.ScoreXPath}");

                    post.Points = score == null ? 0 : ParsePositiveInteger(score.InnerText);

                    // Get the <a></a> containing the comments
                    var comments = htmlDoc.DocumentNode.SelectSingleNode($"{indexedSubtextXPath}{_options.CommentsXPath}");

                    post.Comments = comments == null ? 0 : ParsePositiveInteger(comments.InnerText);

                    output.Add(post);
                }

                // Get the URL for the next page
                url = GetNextPageUrl(htmlDoc);

            } while (output.Count < count && !string.IsNullOrWhiteSpace(url));

            return _serializer.Serialize(output);
        }

        private string GetNextPageUrl(HtmlDocument htmlDoc)
        {
            // Look for the node
            var node = htmlDoc.DocumentNode
                .SelectSingleNode(_options.NextPageXPath);

            // Do we have a next page link
            if (node == null)
                return null;

            // Does the node have and href attribute
            if (!node.Attributes.Contains("href"))
                return null;

            // Extract the value of the attribute
            var path = node.Attributes.First(a => a.Name == "href").Value;

            return string.IsNullOrWhiteSpace(path) ? null : $"{_options.Host}{path}";
        }

        private static string ParseText(string input, string defaultValue)
        {
            if (string.IsNullOrWhiteSpace(input))
                return defaultValue;

            if (input.Length < 256)
                return input;

            return $"{input.Substring(0, 253)}...";
        }

        private static int ParsePositiveInteger(string input)
        {
            // Strip out any non-numeric characters
            var stripped = Regex.Replace(input, "[^0-9]", "");

            // Try to parse an integer or return the default value
            // We return the 0 is cases where instead of '0 comments' the value is 'discuss'
            return int.TryParse(stripped, out var output) ? output : 0;
        }

        private static string ParseUri(string input)
        {
            return Uri.TryCreate(input, UriKind.Absolute, out var uri) ? uri.ToString() : string.Empty;
        }
    }
}