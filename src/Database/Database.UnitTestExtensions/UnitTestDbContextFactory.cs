using System;
using System.Data.Common;
using Database.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Database.UnitTestExtensions
{
    public class UnitTestDbContextFactory : IDisposable
    {
        private string _connectionString = @"Server=192.168.2.27;Database=crmrevo_dev;User Id=crmv;Password=apt@ven2018;";

        private DbContextOptions<DatabaseContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlServer(_connectionString, opts => {
                    opts.CommandTimeout(60 * 60 * 24);
                    opts.EnableRetryOnFailure();
                }).Options;
        }

        private DbContextOptions<DbQueryContext> CreateDbQueryOptions()
        {
            return new DbContextOptionsBuilder<DbQueryContext>()
                .UseSqlServer(_connectionString, opts => {
                    opts.CommandTimeout(60 * 60 * 24);
                    opts.EnableRetryOnFailure();
                }).Options;
        }

        public DatabaseContext CreateContext()
        {
            var db = new DatabaseContext(CreateOptions());

            return db;
        }

        public DbQueryContext CreateDbQueryContext()
        {
            var dbQuery = new DbQueryContext(CreateDbQueryOptions());

            return dbQuery;
        }

        public void Dispose()
        {
        }
    }
}
