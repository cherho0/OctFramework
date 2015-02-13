using System;
using System.Reflection.Emit;
using Oct.Framework.DB.Emit.AST.Interfaces;

namespace Oct.Framework.DB.Emit.AST.Nodes
{
    class AstUnbox : IAstValue
    {
        public Type unboxedType;
        public IAstRef refObj;

        public Type itemType
        {
            get { return unboxedType; }
        }

        public void Compile(CompilationContext context)
        {
            refObj.Compile(context);
            context.Emit(OpCodes.Unbox_Any, unboxedType);
        }
    }
}