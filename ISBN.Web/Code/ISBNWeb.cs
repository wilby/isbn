using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ISBN.Web.Models;
using System.Net;
using System.Text;
using System.IO;
using StructureMap;

namespace ISBN.Web.Code
{
    public class ISBNWeb : ISBN.Web.Code.IISBNWeb
    {
        private string _url = "http://isbndb.com/api/books.xml?access_key=DCUG6OGK&index1=isbn&value1={0}";
        private HttpWebRequest _browser;
        private IXmlToBookModelConverter _converter;

        public ISBNWeb()
        {
            _converter = ObjectFactory.GetInstance<IXmlToBookModelConverter>();
        }

        public BookModel GetBook(string isbn)
        {
            return XmlToBookModel(GetXml(isbn));
        }

        private string GetXml(string isbn)
        {
            _browser = (HttpWebRequest)HttpWebRequest.Create(string.Format(_url, isbn));
            _browser.Accept = "application/xml";
            _browser.Method = "GET";

            WebResponse webresponse = _browser.GetResponse();

            Encoding enc = System.Text.Encoding.GetEncoding(1252);
            StreamReader loResponseStream = new
              StreamReader(webresponse.GetResponseStream(), enc);

            string response = loResponseStream.ReadToEnd();

            loResponseStream.Close();
            webresponse.Close();

            return response;
        }

        private BookModel XmlToBookModel(string xml)
        {
            return _converter.ConvertXmlToBookModel(xml);
        }
    }
}