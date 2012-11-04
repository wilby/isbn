using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using ISBN.Web.Tests;

namespace ISBN.Web.Code
{
    public static class SMBootstrapper
    {        
        public static void BootstrapStructureMap(HttpServerUtility server)
        {
            // Initialize the static ObjectFactory container
            ObjectFactory.Initialize(x =>
            {
                x.For<IXmlToBookModelConverter>().Use<IsbnXmlToBookModelConverter>();
                x.For<IISBNWeb>().Use<ISBNWeb>();
                x.For<IRepository>().Use<Repository>()
                    .Ctor<IXmlToBookModelConverter>("converter").Is(new DbXmlToBookModelConverter())
                    .Ctor<string>("xmlDatbasePath").Is(HttpContext.Current.Server.MapPath(@"App_Data\ISBN.xml"));//server.MapPath(@"App_Data\ISBN.xml"));
            });
        }
        
    }
}