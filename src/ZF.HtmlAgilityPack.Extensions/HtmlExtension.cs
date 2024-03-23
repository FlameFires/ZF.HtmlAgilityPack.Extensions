namespace HtmlAgilityPack
{
    public static class HtmlExtension
    {
        /// <summary>
        /// Load html string to HtmlDocument
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static HtmlDocument LoadHtml(this string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                throw new ArgumentNullException(html);

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            return htmlDoc;
        }

        /// <summary>
        /// Load html string to HtmlNode
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static HtmlNode XPath(this string html)
        {
            return LoadHtml(html).XPath(html);
        }

        /// <summary>
        /// Get HtmlNode by xpath
        /// </summary>
        /// <param name="htmlDoc"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static HtmlNode XPath(this HtmlDocument htmlDoc, string xpath)
        {
            return htmlDoc.DocumentNode.SelectSingleNode(xpath);
        }

        /// <summary>
        /// Get HtmlNode by xpath
        /// </summary>
        /// <param name="htmlNode"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static HtmlNode XPath(this HtmlNode htmlNode, string xpath)
        {
            return htmlNode.SelectSingleNode(xpath);
        }

        /// <summary>
        /// Get HtmlNodeCollection by xpath
        /// </summary>
        /// <param name="html"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static HtmlNodeCollection XPathList(this string html, string xpath)
        {
            return LoadHtml(html).XPathList(xpath);
        }

        /// <summary>
        /// Get HtmlNodeCollection by xpath
        /// </summary>
        /// <param name="htmlDoc"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static HtmlNodeCollection XPathList(this HtmlDocument htmlDoc, string xpath)
        {
            return htmlDoc.DocumentNode.SelectNodes(xpath);
        }

        /// <summary>
        /// Get HtmlNodeCollection by xpath
        /// </summary>
        /// <param name="htmlDoc"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static HtmlNodeCollection XPathList(this HtmlNode htmlDoc, string xpath)
        {
            return htmlDoc.SelectNodes(xpath);
        }

        /// <summary>
        /// Get string value by xpath
        /// </summary>
        /// <param name="htmlDoc"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static string XPathStr(this HtmlDocument htmlDoc, string xpath)
        {
            var htmlNode = htmlDoc.DocumentNode.SelectSingleNode(xpath);
            if (htmlNode == null)
                return string.Empty;

            var expression = GetExpression(xpath);
            if (string.IsNullOrWhiteSpace(expression))
                return string.Empty;

            return GetXpathStrValue(htmlNode, expression);
        }

        /// <summary>
        /// Get string value by xpath
        /// </summary>
        /// <param name="sourceNode"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static string XPathStr(this HtmlNode sourceNode, string xpath)
        {
            var htmlNode = sourceNode?.SelectSingleNode(xpath);
            if (htmlNode == null)
                return string.Empty;

            var expression = GetExpression(xpath);
            if (string.IsNullOrWhiteSpace(expression))
                return string.Empty;

            return GetXpathStrValue(htmlNode, expression);
        }

        /// <summary>
        /// Get string value list by xpath
        /// </summary>
        /// <param name="htmlDoc"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static IEnumerable<string> XPathListStr(this HtmlDocument htmlDoc, string xpath)
        {
            var htmlNodes = htmlDoc.DocumentNode.SelectNodes(xpath);
            if (htmlNodes == null)
                yield break;

            var expression = GetExpression(xpath);
            if (string.IsNullOrWhiteSpace(expression))
                yield return string.Empty;

            foreach (var htmlNode in htmlNodes)
            {
                yield return GetXpathStrValue(htmlNode, expression);
            }
        }

        /// <summary>
        /// Get string value list by xpath
        /// </summary>
        /// <param name="sourceNode"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static IEnumerable<string> XPathListStr(this HtmlNode sourceNode, string xpath)
        {
            var htmlNodes = sourceNode.SelectNodes(xpath);
            if (htmlNodes == null)
                yield break;

            var expression = GetExpression(xpath);
            if (string.IsNullOrWhiteSpace(expression))
                yield return string.Empty;

            foreach (var htmlNode in htmlNodes)
            {
                yield return GetXpathStrValue(htmlNode, expression);
            }
        }

        /// <summary>
        /// Get xpath expression
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        private static string GetExpression(string xpath)
        {
            var arr = xpath.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length == 0)
                return string.Empty;

            var expression = arr[arr.Length - 1].Trim();
            return expression;
        }

        /// <summary>
        /// Get string value by last expression
        /// </summary>
        /// <param name="htmlNode"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        private static string GetXpathStrValue(HtmlNode htmlNode, string expression)
        {
            if (expression.StartsWith("@"))
            {
                var attrName = expression.Substring(1);
                return htmlNode.GetAttributeValue(attrName, string.Empty);
            }
            else
                switch (expression)
                {
                    case "text()":
                        return htmlNode.InnerText;
                    case "html()":
                        return htmlNode.InnerHtml;
                    case "outerhtml()":
                        return htmlNode.OuterHtml;
                }

            return htmlNode.InnerText;
        }
    }
}