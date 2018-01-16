using System.Collections.Generic;
using System;
using System.Linq;
using HtmlAgilityPack;

namespace BashIm
{
    public abstract class BaseLoader
    {
        private string BaseUrl = "http://bash.im";
        protected string DataType = "";
        protected bool RatingExists = true;

        public List<Quote> GetQuotes()
        {
            var result = new List<Quote>();
            var pageContent = new HtmlDocument();
            pageContent.LoadHtml(Utils.GetPageContent(GetAddress()).Result.Replace("<br>", "\n").Replace("<br/>", "\n").Replace("<br />", "\n"));

            var quotesNode = pageContent.DocumentNode.Descendants().Where(x =>
                (x.Name == "div" && x.Attributes["class"] != null && x.Attributes["class"].Value.Equals("quote"))).ToList();

            foreach (var item in quotesNode)
            {
                try
                {
                    var rating = "???";
                    if (RatingExists)
                    {
                        rating = item.Descendants().Where(x =>
                            (x.Name == "span" && x.Attributes["class"] != null &&
                             x.Attributes["class"].Value.Equals("rating"))).ToList()[0].InnerText;
                    }

                    var date = item.Descendants().Where(x =>
                        (x.Name == "span" && x.Attributes["class"] != null && x.Attributes["class"].Value.Equals("date"))).ToList()[0].InnerText;
                    var id = item.Descendants().Where(x =>
                        (x.Name == "a") && x.Attributes["class"] != null && x.Attributes["class"].Value.Equals("id")).ToList()[0].InnerText;
                    var text = item.Descendants().Where(x =>
                        (x.Name == "div" && x.Attributes["class"] != null && x.Attributes["class"].Value.Equals("text"))).ToList()[0].InnerText;

                    result.Add(new Quote
                    {
                        CreatedAt = DateTime.Parse(date),
                        Id = id.Replace("#", ""),
                        Rating = rating == "???" ? BashIm.Rating.UNKNOWN : ((Rating) int.Parse(rating)),
                        Text = text
                    });
                } catch (Exception) {}
            }

            return result;
        }

        private string GetAddress()
        {
            return $"{BaseUrl}/{DataType}";
        }
    }
}