using System;
using System.Reflection.Emit;
using Oct.Framework.DB.Emit.AST.Interfaces;

namespace Oct.Framework.DB.Emit.AST.Nodes
{
	class AstExprNot : IAstValue
	{
		IAstRefOrValue _value;

		public Type itemType
		{
			get { return typeof(Int32); }
		}

        public AstExprNot(IAstRefOrValue value)
		{
			_value = value;
		}

		public void Compile(CompilationContext context)
		{
			context.Emit(OpCodes.Ldc_I4_0);
			_value.Compile(context);
			context.Emit(OpCodes.Ceq);
		}
	}
}
