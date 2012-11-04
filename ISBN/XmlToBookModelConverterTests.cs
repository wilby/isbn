using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ISBN.Web.Code;
using ISBN.Web.Models;
using System.Net;
using System.IO;

namespace ISBN
{
    [TestClass]
    public class XmlToBookModelConverterTests
    {
        [TestMethod]
        public void TestGetsISBNFromWeb()
        {
            XmlToBookModelConverter converter = new XmlToBookModelConverter();
            BookModel model = converter.ConvertXmlToBookModel(GetXml("9780596009250"));

            Assert.AreEqual("Programming Python", model.Title);
        }

        private string GetXml(string isbn)
        {
            string _url = "http://isbndb.com/api/books.xml?access_key=DCUG6OGK&index1=isbn&value1={0}";

            HttpWebRequest browser;
            browser = (HttpWebRequest)HttpWebRequest.Create(string.Format(_url, isbn));
            browser.Accept = "application/xml";
            browser.Method = "GET";

            WebResponse webresponse = browser.GetResponse();

            Encoding enc = System.Text.Encoding.GetEncoding(1252);
            StreamReader loResponseStream = new
              StreamReader(webresponse.GetResponseStream(), enc);

            string response = loResponseStream.ReadToEnd();

            loResponseStream.Close();
            webresponse.Close();

            return response;
        }
    }
}
