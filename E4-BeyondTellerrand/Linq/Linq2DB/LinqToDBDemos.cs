using Demos.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Demos.Linq2DB.DB;

namespace Demos.Linq2DB
{
    class LinqToDBDemos
    {
        public LinqToDBDemos()
        {
            PersonFaker.BuildDB();
        }

        public void Execute1()
        {
            var context = new DemoContext();

            // Linq Abfrage auf eine Liste von Objekten
            var result = from q in context.Persons
                         where q.ID < 5
                         select new
                         {
                             ID = q.ID,
                             Name = $"{q.FirstName} {q.LastName}",
                             BirthDay = q.BirthDay
                         };

            Console.WriteLine(ObjectDumper.Dump(result.ToList()));
        }

        public void Execute2()
        {
            var context = new DemoContext();

            // Linq Ausdruck (Lambda Style) auf eine Liste von Objekten
            var result = context.Persons
                           .Where(q => q.ID < 5)
                           .Select(q => new
                           {
                               ID = q.ID,
                               Name = $"{q.FirstName} {q.LastName}",
                               BirthDay = q.BirthDay
                           });

            Console.WriteLine(ObjectDumper.Dump(result.ToList()));
        }

        public void Execute3()
        {
            var context = new DemoContext();

            var result = context.Persons
                            .Where(q => q.ID < 5)
                            .Select(q => new
                            {
                                ID = q.ID,
                                Name = $"{q.FirstName} {q.LastName}",
                                CurrentSteet = q.Addresses
                                                .OrderByDescending(s => s.ID)
                                                .Select(s => $"{s.StreetName} {s.StreetNo}")
                                                .FirstOrDefault()
                            });

            Console.WriteLine(ObjectDumper.Dump(result.ToList()));
        }

    }
}
