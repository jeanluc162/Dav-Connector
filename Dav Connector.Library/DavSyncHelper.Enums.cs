using System;
using System.Collections.Generic;
using System.Text;

namespace Dav_Connector.Library
{
    public partial class DavSyncHelper
    {
        public enum SyncMode
        {
            RemoteToLocal = 0,
            LocalToRemote = 1,
            BothWays = 2
        }
    }
}
