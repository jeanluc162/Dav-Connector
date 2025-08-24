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

namespace Dav_Connector.Library.Model
{
    public class DavConnectorDbContext:DbContext
    {
        public delegate String GetEncryptionPasswordDelegate();
        public static GetEncryptionPasswordDelegate GetEncryptionPassword { get; set; }

        public static String DbPath = null;

        public DbSet<Account> Accounts { get; set; }
        public DbSet<SyncType> SyncTypes { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqliteConnectionStringBuilder connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DbPath ?? ":memory:";

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(connectionStringBuilder.ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
