using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oct.Framework.DB.Attrbuites;
using Oct.Framework.DB.Base;

namespace DbTest.Entities
{
    [Entity("DemoTable")]
    public class DemoTable : BaseEntity<DemoTable>
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        public int Sex { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
