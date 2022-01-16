using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Demos.Linq2DB.DB;

public class DemoContext : DbContext
{
    static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

    static readonly DbContextOptions DefaultOptionsNoLogging = new DbContextOptionsBuilder<DemoContext>()
                                                .UseSqlite("Data Source=DummyDB.db;")
                                                .Options;

    static readonly DbContextOptions DefaultOptions = new DbContextOptionsBuilder<DemoContext>()
                                                .UseLoggerFactory(loggerFactory)
                                                .UseSqlite("Data Source=DummyDB.db;")
                                                .Options;

    public DemoContext(DbContextOptions options) : base(options)
    { }

    public DemoContext(bool logging = true) : base(logging ? DefaultOptions : DefaultOptionsNoLogging)
    { }

    public DbSet<PersonDB> Persons { get; set; }
    public DbSet<AddressDB> Addresses { get; set; }
}
