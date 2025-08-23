using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Windows.Storage;

namespace Dav_Connector.Model
{
    class DavConnectorDbContext:DbContext
    {
        static DavConnectorDbContext()
        {
            SqliteConnectionStringBuilder connectionStringBuilder = new SqliteConnectionStringBuilder();
            try
            {
                ApplicationData.Current.LocalFolder.CreateFileAsync(DbName, CreationCollisionOption.OpenIfExists).AsTask().GetAwaiter().GetResult();
                connectionStringBuilder.DataSource = Path.Combine(ApplicationData.Current.LocalFolder.Path, DbName);

                DbConnectionString = connectionStringBuilder.ConnectionString;
            }
            catch(InvalidOperationException)
            {
                connectionStringBuilder.DataSource = ":memory:";
            }

            DbConnectionString = connectionStringBuilder.ConnectionString;
        }
        protected const String DbName = "DavConnectorDb.sqlite";
        protected static readonly String DbConnectionString;

        public DbSet<Account> Accounts { get; set; }
        public DbSet<SyncType> SyncTypes { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(DbConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
