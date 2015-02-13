using System;
using System.Reflection.Emit;
using Oct.Framework.DB.Emit.AST.Interfaces;

namespace Oct.Framework.DB.Emit.AST.Nodes
{
	/// <summary>
	/// Generates "value ?? ifNullValue" expression.
	/// </summary>
	class AstIfNull : IAstRefOrValue
	{
		IAstRef _value;
		IAstRefOrValue _ifNullValue;

		public Type itemType
		{
			get 
			{
				return _value.itemType;
			}
		}

		public AstIfNull(IAstRef value, IAstRefOrValue ifNullValue)
		{
			_value = value;
			_ifNullValue = ifNullValue;
			if (!_value.itemType.IsAssignableFrom(_ifNullValue.itemType))
			{
				throw new EmitMapperException("Incorrect ifnull expression");
			}
		}

		public void Compile(CompilationContext context)
		{
			Label ifNotNullLabel = context.ilGenerator.DefineLabel();
			_value.Compile(context);
			context.Emit(OpCodes.Dup);
			context.Emit(OpCodes.Brtrue_S, ifNotNullLabel);
			context.Emit(OpCodes.Pop);
			_ifNullValue.Compile(context);
			context.ilGenerator.MarkLabel(ifNotNullLabel);
		}
	}
}
