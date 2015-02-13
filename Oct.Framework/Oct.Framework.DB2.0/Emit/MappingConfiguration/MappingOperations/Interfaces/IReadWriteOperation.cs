using System;

namespace Oct.Framework.DB.Emit.MappingConfiguration.MappingOperations.Interfaces
{
	interface IReadWriteOperation : IDestWriteOperation, ISrcReadOperation
	{
        Delegate NullSubstitutor { get; set; } // generic type: NullSubstitutor
        Delegate TargetConstructor { get; set; } // generic type: TargetConstructor
		Delegate Converter { get; set; }
	}
}
