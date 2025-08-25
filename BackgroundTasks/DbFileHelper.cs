using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace BackgroundTasks
{
    public static class DbFileHelper
    {
        private const String DbName = "DavConnectorDb.sqlite";
        public static String EnsureAndGetDbPath()
        {
            ApplicationData.Current.LocalFolder.CreateFileAsync(DbName, CreationCollisionOption.OpenIfExists).AsTask().GetAwaiter().GetResult();
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, DbName);
        }
    }
}
