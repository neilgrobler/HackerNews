using HtmlAgilityPack;

namespace HackerNews.BaseClasses
{
    public abstract class StringParserBase
    {
        protected abstract string XPath { get; }
        protected abstract string DefaultValue { get; }
        protected abstract int MaxLength { get; }

        public string Parse(HtmlDocument htmlDoc, int index)
        {
            var xpath = string.Format(XPath, index);

            var node = htmlDoc.DocumentNode.SelectSingleNode(xpath);

            if (node == null || string.IsNullOrWhiteSpace(node.InnerText))
                return DefaultValue;

            return node.InnerText.Length > MaxLength ? node.InnerText.Substring(0, MaxLength) : node.InnerText;
        }
    }
}