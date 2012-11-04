using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ISBN.Web.Models;
using ISBN.Web.Code;
using System.Xml.Linq;

namespace ISBN.Web.Tests
{
    public class DbXmlToBookModelConverter : IXmlToBookModelConverter
    {
        public BookModel ConvertXmlToBookModel(string xml)
        {
            XDocument xmldoc = XDocument.Parse(xml);
            XElement book = xmldoc.Elements().Where(x => x.Name == "book").FirstOrDefault();

            return ConvertXmlToBookModel(book);     
        }

        public BookModel ConvertXmlToBookModel(XElement book)
        {
            XElement publisher = book.Elements().Where(x => x.Name == "publisher").FirstOrDefault();
            return new BookModel()
            {
                ISBN = book.Attributes().Where(x => x.Name == "isbn").FirstOrDefault().Value,
                ISBN13 = book.Attributes().Where(x => x.Name == "isbn13").FirstOrDefault().Value,
                Title = book.Elements().Where(x => x.Name == "title").FirstOrDefault().Value,
                Author = book.Elements().Where(x => x.Name == "author").FirstOrDefault().Value,
                Publisher = new Publisher()
                {
                    Id = publisher.Attributes().Where(x => x.Name == "publisher_id").FirstOrDefault().Value,
                    Name = publisher.Value
                }
            };            
        }
    }
}
