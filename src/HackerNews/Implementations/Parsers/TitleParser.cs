using HackerNews.BaseClasses;
using HackerNews.Interfaces;

namespace HackerNews.Implementations.Parsers
{
    public class TitleParser : StringParserBase, ITitleParser
    {
        protected override  string XPath => "//table[@class = 'itemlist']/tr[@class = 'athing'][{0}]//a[@class='storylink']";
        protected override string DefaultValue => "Not supplied";
        protected override int MaxLength => 256;
    }
}