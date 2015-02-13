using System;
using System.Reflection.Emit;
using Oct.Framework.DB.Emit.AST.Helpers;
using Oct.Framework.DB.Emit.AST.Interfaces;

namespace Oct.Framework.DB.Emit.AST.Nodes
{
    class AstReturn : IAstNode, IAstAddr
    {
        public Type returnType;
        public IAstRefOrValue returnValue;

        public void Compile(CompilationContext context)
        {
            returnValue.Compile(context);
            CompilationHelper.PrepareValueOnStack(context, returnType, returnValue.itemType);
            context.Emit(OpCodes.Ret);
        }

        public Type itemType
        {
            get { return returnType; }
        }
    }
}