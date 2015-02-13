using Oct.Framework.DB.Emit.MappingConfiguration.MappingOperations.Interfaces;

namespace Oct.Framework.DB.Emit.MappingConfiguration.MappingOperations
{
	public delegate void ValueSetter(object obj, object value, object state);
	public class SrcReadOperation : ISrcReadOperation
	{
		public ValueSetter Setter { get; set; }
		public MemberDescriptor Source { get; set; }

        public override string ToString()
        {
            return "SrcReadOperation. Source member:" + Source.ToString();
        }
	}
}
