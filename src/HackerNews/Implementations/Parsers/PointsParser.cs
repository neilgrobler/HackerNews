using HackerNews.BaseClasses;
using HackerNews.Interfaces;

namespace HackerNews.Implementations.Parsers
{
    public class PointsParser : PositiveIntegerParserBase, IPointsParser
    {
        protected override string XPath =>
            "//table[@class='itemlist']/tr[@class='athing'][{0}]/following-sibling::tr[1]/td[@class='subtext']//span[@class='score']";

        protected override int DefaultValue => 0;
    }
}