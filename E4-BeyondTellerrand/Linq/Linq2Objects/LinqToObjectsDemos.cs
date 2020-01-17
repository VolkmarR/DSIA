using Demos.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Demos.Linq2Objects
{
    class LinqToObjectsDemos
    {
        readonly List<Person> Data;

        public LinqToObjectsDemos()
        {
            Data = PersonFaker.Build();
        }

        public void Execute1()
        {
            // Linq Abfrage auf eine Liste von Objekten
            var result = from q in Data
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
            // Linq Ausdruck (Lambda Style) auf eine Liste von Objekten
            var result = Data
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
            var result = Data
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
