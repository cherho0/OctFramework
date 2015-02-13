namespace Oct.Framework.DB.Emit.MappingConfiguration.MappingOperations.Interfaces
{
	public interface IDestOperation : IMappingOperation
	{
		MemberDescriptor Destination { get; set; }
	}
}
