using System.Collections.Generic;
using Oct.Framework.DB.Emit.MappingConfiguration.MappingOperations.Interfaces;

namespace Oct.Framework.DB.Emit.MappingConfiguration.MappingOperations
{
    public class OperationsBlock: IComplexOperation
    {
        public List<IMappingOperation> Operations { get; set; }
    }
}
