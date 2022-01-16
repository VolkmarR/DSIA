namespace Demos.Linq2DB;

class LinqToDBDemos
{
    public void Execute1()
    {
        Console.WriteLine("--- LinqToDBDemos.Execute1 ---");

        using var context = new DemoContext();

        // Linq Abfrage auf eine Liste von Objekten
        var result = from q in context.Persons
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
        Console.WriteLine("--- LinqToDBDemos.Execute2 ---");

        using var context = new DemoContext();

        // Linq Ausdruck (Lambda Style) auf eine Liste von Objekten
        var result = context.Persons
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
        Console.WriteLine("--- LinqToDBDemos.Execute3 ---");

        using var context = new DemoContext();

        var result = context.Persons
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
