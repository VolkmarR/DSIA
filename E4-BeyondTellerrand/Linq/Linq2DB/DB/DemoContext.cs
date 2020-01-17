using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Demos.Linq2DB.DB
{
    public class DemoContext : DbContext
    {
        static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public static DbContextOptions DefaultOptionsNoLogging = new DbContextOptionsBuilder<DemoContext>()
                                                    .UseSqlite(@"Data Source=DummyDB.db;")
                                                    .Options;

        static DbContextOptions DefaultOptions = new DbContextOptionsBuilder<DemoContext>()
                                                    .UseLoggerFactory(loggerFactory)
                                                    .UseSqlite(@"Data Source=DummyDB.db;")
                                                    .Options;

        public DemoContext(DbContextOptions options) : base(options)
        { }

        public DemoContext(bool logging = true) : base(logging ? DefaultOptions : DefaultOptionsNoLogging)
        { }

        public DbSet<PersonDB> Persons { get; set; }
        public DbSet<AddressDB> Addresses { get; set; }

        public bool Equals(DemoContext other)
        {
            throw new NotImplementedException();
        }
    }
}
