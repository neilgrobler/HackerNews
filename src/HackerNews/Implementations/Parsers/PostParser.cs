using HackerNews.Interfaces;
using HtmlAgilityPack;

namespace HackerNews.Implementations.Parsers
{
    public class PostParser : IPostParser
    {
        private readonly ITitleParser _titleParser;
        private readonly IUrlParser _urlParser;
        private readonly IAuthorParser _authorParser;
        private readonly IRankParser _rankParser;
        private readonly ICommentsParser _commentsParser;
        private readonly IPointsParser _pointsParser;

        public PostParser(ITitleParser titleParser, IUrlParser urlParser, IAuthorParser authorParser, IRankParser rankParser, ICommentsParser commentsParser, IPointsParser pointsParser)
        {
            _titleParser = titleParser;
            _urlParser = urlParser;
            _authorParser = authorParser;
            _rankParser = rankParser;
            _commentsParser = commentsParser;
            _pointsParser = pointsParser;
        }

        public Post Parse(HtmlDocument htmlDoc, int index)
        {
            return new Post
            {
                Title = _titleParser.Parse(htmlDoc, index),
                Uri = _urlParser.Parse(htmlDoc, index),
                Author = _authorParser.Parse(htmlDoc, index),
                Rank = _rankParser.Parse(htmlDoc, index),
                Points = _pointsParser.Parse(htmlDoc, index),
                Comments = _commentsParser.Parse(htmlDoc, index)
            };
        }
    }
}