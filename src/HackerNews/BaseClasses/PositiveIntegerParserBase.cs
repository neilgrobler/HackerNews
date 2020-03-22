using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace HackerNews.BaseClasses
{
    public abstract class PositiveIntegerParserBase
    {
        protected abstract string XPath { get; }
        protected abstract int DefaultValue { get; }

        public int Parse(HtmlDocument htmlDoc, int index)
        {
            var xpath = string.Format(XPath, index);

            var node = htmlDoc.DocumentNode
                .SelectSingleNode(xpath);

            if (node == null)
                return DefaultValue;

            // Strip out any non-numeric characters
            var stripped = Regex.Replace(node.InnerText, "[^0-9]", "");

            // Try to parse an integer or return the default value
            // We return the 0 is cases where instead of '0 comments' the value is 'discuss'
            return int.TryParse(stripped, out var output) ? output : 0;
        }
    }
}