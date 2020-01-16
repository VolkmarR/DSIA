using Demos.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;

namespace Demos.Linq2Xml
{
    class LinqToXmlDemos
    {
        readonly XDocument Xml;

        public LinqToXmlDemos()
        {
            var fileName = "DummyData.xml";
            PersonFaker.BuildXml(fileName);

            Xml = XDocument.Load(fileName);
        }

        public void Execute1()
        {
            // Linq Abfrage auf eine XML Datei
            var result = from q in Xml.Root.Elements("Person")
                         where (int)q.Element("ID") < 5
                         select new
                         {
                             ID = (int)q.Element("ID"),
                             Name = $"{(string)q.Element("FirstName")} {(string)q.Element("LastName")}",
                             BirthDay = (DateTime)q.Element("BirthDay")
                         };


            Console.WriteLine(ObjectDumper.Dump(result.ToList()));
        }

        public void Execute2()
        {
            // Linq Ausdruck (Lambda Style) auf eine XML Datei
            var result = Xml.Root.Elements("Person")
                                    .Where(q => (int)q.Element("ID") < 5)
                                    .Select(q => new
                                    {
                                        ID = (int)q.Element("ID"),
                                        Name = $"{(string)q.Element("FirstName")} {(string)q.Element("LastName")}",
                                        BirthDay = (DateTime)q.Element("BirthDay")
                                    });

            Console.WriteLine(ObjectDumper.Dump(result.ToList()));
        }

        public void Execute3()
        {
            var result = Xml.Root.Elements("Person")
                                    .Where(q => (int)q.Element("ID") < 5)
                                    .Select(q => new
                                    {
                                        ID = (int)q.Element("ID"),
                                        Name = $"{(string)q.Element("FirstName")} {(string)q.Element("LastName")}",
                                        CurrentSteet = q.Descendants("Address")
                                                            .OrderByDescending(s => (int)s.Element("ID"))
                                                            .Select(s => $"{(string)s.Element("StreetName")} {(string)s.Element("StreetNo")}")
                                                            .FirstOrDefault()
                                    });

            Console.WriteLine(ObjectDumper.Dump(result.ToList()));
        }

    }
}
