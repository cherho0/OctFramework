using System;
using System.Reflection.Emit;
using Oct.Framework.DB.Emit.AST.Interfaces;

namespace Oct.Framework.DB.Emit.AST.Nodes
{
	class AstTypeof: IAstRef
	{
		public Type type;

		#region IAstStackItem Members

		public Type itemType
		{
			get 
			{
				return typeof(Type);
			}
		}

		#endregion

		#region IAstNode Members

		public void Compile(CompilationContext context)
		{
			context.Emit(OpCodes.Ldtoken, type);
			context.EmitCall(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle"));
		}

		#endregion
	}
}
