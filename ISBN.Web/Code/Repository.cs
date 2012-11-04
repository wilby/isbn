using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using ISBN.Web.Models;
using System.Xml;

namespace ISBN.Web.Code
{
    public class Repository : ISBN.Web.Code.IRepository
    {
        private IXmlToBookModelConverter _converter;
        private XDocument _db;
        private string _xmlPath;

        public Repository(IXmlToBookModelConverter converter,string xmlDatbasePath)
        {
            _xmlPath = xmlDatbasePath;
            _converter = converter;
            _db = XDocument.Load(xmlDatbasePath);
        }

        public BookModel GetBook(string isbn)
        {
            return _converter.ConvertXmlToBookModel(
                _db.Elements()
                .Where(x => x.Attributes().Where(y => y.Name == "isbn")
                    .FirstOrDefault().Value == isbn).FirstOrDefault());
        }

        public IEnumerable<BookModel> GetAllBooks()
        {
            List<BookModel> books = new List<BookModel>();
            var bookelements = _db.Descendants().Where(x => x.Name == "book");
            foreach (XElement el in bookelements)
            {
                books.Add(_converter.ConvertXmlToBookModel(el));
            }
            return books;
        }

        public void AddBook(BookModel book)
        {
            _db.Elements().First().Add(book.BookModelToXmlElement());            
        }

        public void DeleteBook(string isbn)
        {
            var toRemove = _db.Descendants("book").Where(x => (string)x.Attribute("isbn") == isbn || (string)x.Attribute("isbn13") == isbn);
            toRemove.Remove();
        }

        public void Save()
        {
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = false;
            xws.Indent = true;

            using(XmlWriter writer = XmlWriter.Create(_xmlPath, xws)) {                
                _db.Save(writer);
            }
        }
    }
}