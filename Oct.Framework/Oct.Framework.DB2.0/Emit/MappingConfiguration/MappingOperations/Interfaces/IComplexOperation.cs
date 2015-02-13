using System.Collections.Generic;

namespace Oct.Framework.DB.Emit.MappingConfiguration.MappingOperations.Interfaces
{
    interface IComplexOperation: IMappingOperation
    {
        List<IMappingOperation> Operations { get; set; }
    }
}
