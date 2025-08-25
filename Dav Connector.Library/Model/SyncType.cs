using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dav_Connector.Library.Model
{
    public class SyncType:EntityBase
    {
        public static readonly Guid RemoteToLocal = Guid.Parse("24dd7f72-335a-48ec-8ce7-7204bb3359b4");
        public static readonly Guid LocalToRemote = Guid.Parse("5023eecd-324a-4112-899b-1ec3f4bf7c53");
        public static readonly Guid BothWays = Guid.Parse("2b52f274-5f3b-4c8d-82e0-20ef84f492fb");

        public String Name { get; set; }
    }
}
