using System;
using Oct.Framework.DB.Emit.AST.Interfaces;

namespace Oct.Framework.DB.Emit.AST.Nodes
{
	class AstNewNullable: IAstValue
	{
		private Type _nullableType;
		public Type itemType
		{
			get 
			{
				return _nullableType;
			}
		}
		public void Compile(CompilationContext context)
		{
			throw new NotImplementedException();
		}
	}
}
