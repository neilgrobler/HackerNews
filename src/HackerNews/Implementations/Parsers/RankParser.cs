using HackerNews.BaseClasses;
using HackerNews.Interfaces;

namespace HackerNews.Implementations.Parsers
{
    public class RankParser : PositiveIntegerParserBase, IRankParser
    {
        protected override string XPath =>
            "//table[@class='itemlist']/tr[@class='athing'][{0}]//span[@class='rank']";

        protected override int DefaultValue => 0;
    }
}