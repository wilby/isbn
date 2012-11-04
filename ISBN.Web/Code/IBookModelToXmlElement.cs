using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ISBN.Web.Code
{
    public interface IBookModelToXmlElement
    {
        XElement BookModelToXmlElement();
    }
}
