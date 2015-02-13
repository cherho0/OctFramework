using System.Reflection.Emit;
using Oct.Framework.DB.Emit.AST.Interfaces;

namespace Oct.Framework.DB.Emit.AST.Nodes
{
    class AstThrow: IAstNode
    {
        public IAstRef exception;

        public void Compile(CompilationContext context)
        {
            exception.Compile(context);
            context.Emit(OpCodes.Throw);
        }
    }
}