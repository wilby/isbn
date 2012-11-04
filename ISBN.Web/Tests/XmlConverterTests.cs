using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using ISBN.Web.Code;
using System.Net;
using System.Text;
using System.IO;
using ISBN.Web.Models;

namespace ISBN.Web.Tests
{
    [TestFixture]
    public class XmlConverterTests
    {
        private static string _xml;

        [Test]
        public void TestCanGetFromWebandIntoBookModel()
        {
            var converter = new IsbnXmlToBookModelConverter();
            BookModel model = converter.ConvertXmlToBookModel(GetIsbnXml("9780596009250"));

            Assert.AreEqual("0596009259", model.ISBN);
            Assert.AreEqual("9780596009250", model.ISBN13);
            Assert.AreEqual("Programming Python", model.Title);
            Assert.AreEqual("Mark Lutz", model.Author);
            Assert.AreEqual("oreilly", model.Publisher.Id);
            Assert.AreEqual("Sebastopol, CA : O'Reilly, c2006.", model.Publisher.Name);

        }

        [Test]
        public void TestCanConvertDbXmlToBookModel()
        {
            var converter = new DbXmlToBookModelConverter();
            BookModel model = converter.ConvertXmlToBookModel(GetDbXml());

            Assert.AreEqual("97804465569", model.ISBN);
            Assert.AreEqual("9780446556996", model.ISBN13);
            Assert.AreEqual("Wilby's Book", model.Title);
            Assert.AreEqual("Wilby Jackson", model.Author);
            Assert.AreEqual("Jackson", model.Publisher.Id);
            Assert.AreEqual("Jackson Publishing", model.Publisher.Name);
        }

        private string GetDbXml()
        {
            return @"<?xml version=""1.0"" encoding=""utf-8"" ?>
                    <books>
                      <book isbn=""97804465569"" isbn13=""9780446556996"" >
                        <title>Wilby's Book</title>
                        <author>Wilby Jackson</author>
                        <publisher publisher_id=""Jackson"">Jackson Publishing</publisher>    
                      </book>
                    </books>";
        }

        private string GetIsbnXml(string isbn)
        {
            string _url = "http://isbndb.com/api/books.xml?access_key=DCUG6OGK&index1=isbn&value1={0}";

            if (string.IsNullOrEmpty(_xml))
            {
                HttpWebRequest browser;
                browser = (HttpWebRequest)HttpWebRequest.Create(string.Format(_url, isbn));
                browser.Accept = "application/xml";
                browser.Method = "GET";

                WebResponse webresponse = browser.GetResponse();

                Encoding enc = System.Text.Encoding.GetEncoding(1252);
                StreamReader loResponseStream = new
                  StreamReader(webresponse.GetResponseStream(), enc);

                _xml = loResponseStream.ReadToEnd();

                loResponseStream.Close();
                webresponse.Close();
            }
            return _xml;
        
        }
    }
}