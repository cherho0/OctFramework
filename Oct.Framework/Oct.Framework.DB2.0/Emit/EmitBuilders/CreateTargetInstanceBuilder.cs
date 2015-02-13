using System;
using System.Reflection;
using System.Reflection.Emit;
using Oct.Framework.DB.Emit.AST;
using Oct.Framework.DB.Emit.AST.Helpers;
using Oct.Framework.DB.Emit.AST.Interfaces;
using Oct.Framework.DB.Emit.AST.Nodes;
using Oct.Framework.DB.Emit.Utils;

namespace Oct.Framework.DB.Emit.EmitBuilders
{
    class CreateTargetInstanceBuilder
    {
		public static void BuildCreateTargetInstanceMethod(Type type, TypeBuilder typeBuilder)
		{
			if (ReflectionUtils.IsNullable(type))
			{
				type = Nullable.GetUnderlyingType(type);
			}

			MethodBuilder methodBuilder = typeBuilder.DefineMethod(
				"CreateTargetInstance",
				MethodAttributes.Assembly | MethodAttributes.Virtual,
				typeof(object),
				null
				);

			ILGenerator ilGen = methodBuilder.GetILGenerator();
			CompilationContext context = new CompilationContext(ilGen);
			IAstRefOrValue returnValue;

			if (type.IsValueType)
			{
				LocalBuilder lb = ilGen.DeclareLocal(type);
				new AstInitializeLocalVariable(lb).Compile(context);

				returnValue =
					new AstBox()
					{
						value = AstBuildHelper.ReadLocalRV(lb)
					};
			}
			else
			{
				returnValue =
					ReflectionUtils.HasDefaultConstructor(type)
						?
							new AstNewObject()
							{
								objectType = type
							}
						:
							(IAstRefOrValue)new AstConstantNull();
			}
			new AstReturn()
			{
				returnType = type,
				returnValue = returnValue
			}.Compile(context);
		}
    }
}