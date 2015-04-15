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