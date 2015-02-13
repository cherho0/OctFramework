using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oct.Framework.DB.Attrbuites;
using Oct.Framework.DB.Base;

namespace DbTest.Entities
{
    [Entity("TestTs")]
    public class TestTs : BaseEntity<TestTs>
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        public string DD { get; set; }
    }
}
