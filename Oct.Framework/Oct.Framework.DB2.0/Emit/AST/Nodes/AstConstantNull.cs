using System;
using System.Reflection.Emit;
using Oct.Framework.DB.Emit.AST.Interfaces;

namespace Oct.Framework.DB.Emit.AST.Nodes
{
    class AstConstantNull : IAstRefOrValue
    {
        #region IAstReturnValueNode Members

        public Type itemType
        {
            get { return typeof(object); }
        }

        #endregion

        #region IAstNode Members

        public void Compile(CompilationContext context)
        {
            context.Emit(OpCodes.Ldnull);
        }

        #endregion
    }
}