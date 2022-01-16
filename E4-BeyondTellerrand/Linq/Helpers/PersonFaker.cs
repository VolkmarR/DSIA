using Bogus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Text.Json;
using System.Linq;
using Demos.Linq2DB.DB;

namespace Demos.Helpers
{
    static class PersonFaker
    {
        public static List<Person> Build(int count = 100)
        {
            var id = 0;
            var address_id = 0;
            var addressFaker = new Faker<Address>("de")
                                .StrictMode(true)
                                .RuleFor(p => p.ID, f => ++address_id)
                                .RuleFor(p => p.StreetName, f => f.Address.StreetName())
                                .RuleFor(p => p.StreetNo, f => f.Address.BuildingNumber())
                                .RuleFor(p => p.ZipCode, f => f.Address.ZipCode())
                                .RuleFor(p => p.City, f => f.Address.City());


            var faker = new Faker<Person>("de")
                            .StrictMode(true)
                            .RuleFor(p => p.ID, f => ++id)
                            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
                            .RuleFor(p => p.LastName, f => f.Name.LastName())
                            .RuleFor(p => p.Addresses, f => addressFaker.Generate(f.Random.Int(0,5)))
                            .RuleFor(p => p.BirthDay, f => f.Date.Past(30, DateTime.Today.AddYears(-15)).Date);

            return faker.Generate(count);
        }

        public static void BuildXml(string fileName, int count = 100)
        {
            var data = Build(count);
            using var fileStream = new FileStream(fileName, FileMode.Create);
            new XmlSerializer(typeof(List<Person>)).Serialize(fileStream, data);
        }

        public static void BuildJson(string fileName, int count = 100)
        {
            var data = Build(count);
            File.WriteAllText(fileName, JsonSerializer.Serialize(data));
        }

        public static void BuildDB(int count = 100)
        {
            var data = Build(count);
            using var context = new DemoContext(false);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            foreach (var item in data)
                context.Persons.Add(new PersonDB { FirstName = item.FirstName, LastName = item.LastName, BirthDay = item.BirthDay, Addresses = item.Addresses?.Select(q => new AddressDB { StreetName = q.StreetName, StreetNo = q.StreetNo, City = q.City, ZipCode = q.ZipCode }).ToList() });
            context.SaveChanges();
        }
    }
}
