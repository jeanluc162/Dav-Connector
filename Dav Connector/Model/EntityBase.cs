using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dav_Connector.Model
{
    abstract class EntityBase:BindableBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}
