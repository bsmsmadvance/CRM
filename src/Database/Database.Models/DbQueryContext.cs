using System;
using Database.Models.DbQueries;
using Microsoft.EntityFrameworkCore;

namespace Database.Models
{
    public class DbQueryContext : DbContext
    {
        public DbQueryContext(DbContextOptions<DbQueryContext> options)
           : base(options)
        {

        }

        public DbQuery<MasterCenterResult> MasterCenterResults { get; set; }

    }
}
