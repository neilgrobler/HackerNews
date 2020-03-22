using HackerNews.BaseClasses;
using HackerNews.Interfaces;

namespace HackerNews.Implementations.Parsers
{
    public class CommentsParser : PositiveIntegerParserBase, ICommentsParser
    {
        protected override string XPath =>
            "//table[@class = 'itemlist']/tr[@class='athing'][{0}]/following-sibling::tr[1]/td[@class='subtext']//a[3]";

        protected override int DefaultValue => 0;
    }
}