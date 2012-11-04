using System;
using System.Xml.Linq;
namespace ISBN.Web.Code
{
    public interface IXmlToBookModelConverter
    {
        ISBN.Web.Models.BookModel ConvertXmlToBookModel(string xml);
        ISBN.Web.Models.BookModel ConvertXmlToBookModel(XElement book);
    }
}
