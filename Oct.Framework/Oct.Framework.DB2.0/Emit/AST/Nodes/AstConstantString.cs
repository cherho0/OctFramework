using System;
using System.Reflection.Emit;
using Oct.Framework.DB.Emit.AST.Interfaces;

namespace Oct.Framework.DB.Emit.AST.Nodes
{
    class AstConstantString: IAstRef
    {
        public string str;

        #region IAstStackItem Members

        public Type itemType
        {
            get 
            {
                return typeof(string);
            }
        }

        #endregion

        #region IAstNode Members

        public void Compile(CompilationContext context)
        {
            context.Emit(OpCodes.Ldstr, str);
        }

        #endregion
    }
}