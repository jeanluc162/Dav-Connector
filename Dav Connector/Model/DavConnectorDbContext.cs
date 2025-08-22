using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Dav_Connector.Model
{
    class DavConnectorDbContext:DbContext
    {
        static DavConnectorDbContext()
        {
            try
            {
                ApplicationData.Current.LocalFolder.CreateFileAsync(DbName, CreationCollisionOption.OpenIfExists).AsTask().GetAwaiter().GetResult();
                DbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DbName);
            }
            catch(InvalidOperationException)
            {
                DbPath = ":memory:";
            }
        }
        protected const String DbName = "DavConnectorDb.sqlite";
        protected static readonly String DbPath;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

        public DbSet<Account> Accounts { get; set; }
    }
}
