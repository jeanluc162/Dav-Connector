using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dav_Connector.Library.Model
{
    public class AccountType:EntityBase
    {
        public static readonly Guid CardDav = Guid.Parse("31585b9b-7549-4d77-828f-670d617c5fdd");

        public String Name { get; set; }
    }
}
