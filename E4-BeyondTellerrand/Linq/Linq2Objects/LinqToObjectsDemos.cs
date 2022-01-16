namespace Demos.Linq2Objects;

class LinqToObjectsDemos
{
    readonly List<Person> Data;

    public LinqToObjectsDemos()
    {
        Data = PersonFaker.Build();
    }

    public void Execute1()
    {
        Console.WriteLine("--- LinqToObjectsDemos.Execute1 ---");

        // Linq Abfrage auf eine Liste von Objekten
        var result = from q in Data
                     where q.ID < 5
                     select new
                     {
                         q.ID,
                         Name = $"{q.FirstName} {q.LastName}",
                         q.BirthDay
                     };

        Console.WriteLine(ObjectDumper.Dump(result.ToList()));
    }

    public void Execute2()
    {
        Console.WriteLine("--- LinqToObjectsDemos.Execute2 ---");

        // Linq Ausdruck (Lambda Style) auf eine Liste von Objekten
        var result = Data
                       .Where(q => q.ID < 5)
                       .Select(q => new
                       {
                           q.ID,
                           Name = $"{q.FirstName} {q.LastName}",
                           q.BirthDay
                       });

        Console.WriteLine(ObjectDumper.Dump(result.ToList()));
    }

    public void Execute3()
    {
        Console.WriteLine("--- LinqToObjectsDemos.Execute3 ---");

        var result = Data
                        .Where(q => q.ID < 5)
                        .Select(q => new
                        {
                            q.ID,
                            Name = $"{q.FirstName} {q.LastName}",
                            CurrentSteet = q.Addresses
                                            .OrderByDescending(s => s.ID)
                                            .Select(s => $"{s.StreetName} {s.StreetNo}")
                                            .FirstOrDefault()
                        });

        Console.WriteLine(ObjectDumper.Dump(result.ToList()));
    }
}
