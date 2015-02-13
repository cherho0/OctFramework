using System;
using System.Reflection.Emit;
using Oct.Framework.DB.Emit.AST.Interfaces;

namespace Oct.Framework.DB.Emit.AST.Nodes
{
    class AstBox : IAstRef
    {
        public IAstRefOrValue value;

        #region IAstReturnValueNode Members

        public Type itemType
        {
            get 
            {
                return value.itemType;  
            }
        }

        #endregion

        #region IAstNode Members

        public void Compile(CompilationContext context)
        {
            value.Compile(context);

            if (value.itemType.IsValueType)
            {
                context.Emit(OpCodes.Box, itemType);
            }
        }

        #endregion
    }
}