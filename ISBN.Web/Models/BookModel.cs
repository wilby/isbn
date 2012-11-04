using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ISBN.Web.Code;
using System.Xml.Linq;

namespace ISBN.Web.Models
{
    public class BookModel : IBookModelToXmlElement
    {
        public string ISBN { get; set; }
        public string ISBN13 { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public Publisher Publisher { get; set; }

        public System.Xml.Linq.XElement BookModelToXmlElement()
        {
            var x = new XElement("book",
                new XAttribute("isbn", this.ISBN),
                new XAttribute("isbn13", this.ISBN13),
                new XElement("title", this.Title),
                new XElement("author", this.Author),
                new XElement("publisher", new XAttribute("publisher_id", this.Publisher.Id)) { Value = this.Publisher.Name });
            return x;
        }
    }
}