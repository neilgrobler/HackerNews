using HackerNews.BaseClasses;
using HackerNews.Interfaces;

namespace HackerNews.Implementations.Parsers
{
    public class AuthorParser : StringParserBase, IAuthorParser
    {
        protected override string XPath => "//table[@class='itemlist']/tr[@class = 'athing'][{0}]/following-sibling::tr[1]/td[@class='subtext']//a[@class='hnuser']";
        protected override string DefaultValue => "Anonymous";
        protected override int MaxLength => 256;
    }
}