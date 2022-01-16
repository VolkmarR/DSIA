using System.Text.Json;

namespace Demos.Linq2Json;

class LinqToJsonDemos
{
    readonly JsonDocument Json;
    public LinqToJsonDemos()
    {
        var fileName = "DummyData.json";
        PersonFaker.BuildJson(fileName);

        Json = JsonDocument.Parse(File.ReadAllText(fileName));
    }

    public void Execute1()
    {
        Console.WriteLine("--- LinqToJsonDemos.Execute1 ---");

        // Linq Ausdruck (Lambda Style) auf eine XML Datei
        var result = Json.RootElement.EnumerateArray()
                                .Where(q => q.GetProperty("ID").GetInt32() < 5)
                                .Select(q => new
                                {
                                    ID = q.GetProperty("ID").GetInt32(),
                                    Name = $"{q.GetProperty("FirstName").GetString()} {q.GetProperty("LastName").GetString()}",
                                    BirthDay = q.GetProperty("BirthDay").GetDateTime()
                                });

        Console.WriteLine(ObjectDumper.Dump(result.ToList()));
    }

    public void Execute2()
    {
        Console.WriteLine("--- LinqToJsonDemos.Execute2 ---");

        var result = Json.RootElement.EnumerateArray()
                                .Where(q => q.GetProperty("ID").GetInt32() < 5)
                                .Select(q => new
                                {
                                    ID = q.GetProperty("ID").GetInt32(),
                                    Name = $"{q.GetProperty("FirstName").GetString()} {q.GetProperty("LastName").GetString()}",
                                    CurrentSteet = q.GetProperty("Addresses").EnumerateArray()
                                                        .OrderByDescending(s => s.GetProperty("ID").GetInt32())
                                                        .Select(s => $"{s.GetProperty("StreetName").GetString()} {s.GetProperty("StreetNo").GetString()}")
                                                        .FirstOrDefault()
                                });

        Console.WriteLine(ObjectDumper.Dump(result.ToList()));
    }
}
