using System;
using System.Data.Common;
using Database.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Database.UnitTestExtensions
{
    public class InMemoryDbContextFactory : IDisposable
    {
        private DbConnection _connection;

        private DbContextOptions<DatabaseContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlite(_connection).Options;
            //return new DbContextOptionsBuilder<DatabaseContext>()
                //.UseInMemoryDatabase(databaseName: "crmrevo").Options;
        }

        public DatabaseContext CreateContext()
        {
            if (_connection == null)
            {
                _connection = new SqliteConnection("DataSource=:memory:");
                _connection.Open();
            }

            var options = CreateOptions();
            using (var context = new DatabaseContext(options))
            {
                context.Database.EnsureCreated();
            }

            return new DatabaseContext(CreateOptions());
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
