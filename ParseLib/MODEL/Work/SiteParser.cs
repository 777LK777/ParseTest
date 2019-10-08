using AngleSharp.Html.Dom;
using ParseLib.Model;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ParseLib
{
    public class SiteParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            List<string> list = new List<string>();
            var items = document.QuerySelectorAll("a").
                Where(item => item.ClassName != null && item.ClassName.Contains("post_title_link"));

            foreach (var item in items)
                list.Add(item.TextContent);

            return list.ToArray();
        }
    }
}
