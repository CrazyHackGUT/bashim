using System;
using System.Linq;
using System.Xml.XPath;
using HtmlAgilityPack;

namespace BashIm
{
    public class Quote
    {
        public string Id { get; set; }
        public Rating Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }

        public static Quote GetQuoteById(int id)
        {
            var result = new Quote();
            var pageContent = new HtmlDocument();
            pageContent.LoadHtml(Utils.GetPageContent($"http://bash.im/quote/{id}").Result.Replace("<br>", "\n").Replace("<br/>", "\n").Replace("<br />", "\n"));

            try
            {
                // quote
                var quoteNode = pageContent.DocumentNode.Descendants().Where(x =>
                        (x.Name == "div" && x.Attributes["class"] != null &&
                         x.Attributes["class"].Value.Equals("quote")))
                    .ToList()[0];

                // rating
                var rating = quoteNode.Descendants().Where(x =>
                        (x.Name == "span" && x.Attributes["class"] != null &&
                         x.Attributes["class"].Value.Equals("rating")))
                    .ToList();

                result.Rating = (rating.Count == 1) ? (Rating) int.Parse(rating[0].InnerText) : Rating.UNKNOWN;

                // date
                result.CreatedAt = DateTime.Parse(quoteNode.Descendants().Where(x =>
                        (x.Name == "span" && x.Attributes["class"] != null &&
                         x.Attributes["class"].Value.Equals("date")))
                    .ToList()[0].InnerText);

                // id
                result.Id = quoteNode.Descendants().Where(x =>
                        (x.Name == "a") && x.Attributes["class"] != null && x.Attributes["class"].Value.Equals("id"))
                    .ToList()[0].InnerText.Replace("#", "");

                // text
                result.Text = quoteNode.Descendants().Where(x =>
                        (x.Name == "div" && x.Attributes["class"] != null &&
                         x.Attributes["class"].Value.Equals("text")))
                    .ToList()[0].InnerText;
            }
            catch (Exception)
            {
                return null;
            }

            return result;
        }
    }
}