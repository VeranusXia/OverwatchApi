namespace OverwatchApi.Properties
{
    using System;
    using System.Linq;
    using AngleSharp.Dom;

    public static class Extensions
    {
        public static IElement GetDataTableByHeaderText(this IDocument doc, string text)
        {
            return doc.QuerySelectorAll("table.data-table").First(t => t.QuerySelector("thead").TextContent == text);
        }

        public static string GetDataTableValueByRowName(this IElement table, string text)
        {
            foreach (var row in table.QuerySelectorAll("tbody tr"))
            {
                var tds = row.QuerySelectorAll("td");
                if (tds[0].TextContent == text)
                    return tds[1].TextContent;
            }

            throw new ArgumentException("Row not found");
        }
    }
}