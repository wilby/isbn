using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ISBN.Web.Models;
using System.Xml.Linq;


namespace ISBN.Web.Code
{
    public class IsbnXmlToBookModelConverter : ISBN.Web.Code.IXmlToBookModelConverter
    {
        public IsbnXmlToBookModelConverter() {}

        public BookModel ConvertXmlToBookModel(string xml) {

            XDocument xmldoc = XDocument.Parse(xml);
            XElement book = xmldoc.Descendants().Elements().Where(x => x.Name == "BookData").FirstOrDefault();
            return ConvertXmlToBookModel(book);
           
        }

        public BookModel ConvertXmlToBookModel(XElement book)
        {
            if (book == null)
                return null;
            XElement publisher = book.Elements().Where(x => x.Name == "PublisherText").FirstOrDefault();
            return new BookModel()
            {
                ISBN = book.Attributes().Where(x => x.Name == "isbn").FirstOrDefault().Value,
                ISBN13 = book.Attributes().Where(x => x.Name == "isbn13").FirstOrDefault().Value,
                Title = book.Elements().Where(x => x.Name == "Title").FirstOrDefault().Value,
                Author = book.Elements().Where(x => x.Name == "AuthorsText").FirstOrDefault().Value,
                Publisher = new Publisher()
                {
                    Id = publisher.Attributes().Where(x => x.Name == "publisher_id").FirstOrDefault().Value,
                    Name = publisher.Value
                }
            };
        }

    }


}