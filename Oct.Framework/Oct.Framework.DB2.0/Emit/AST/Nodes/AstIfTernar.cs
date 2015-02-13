using System;
using System.Reflection.Emit;
using Oct.Framework.DB.Emit.AST.Interfaces;

namespace Oct.Framework.DB.Emit.AST.Nodes
{
    class AstIfTernar : IAstRefOrValue
    {
        public IAstRefOrValue condition;
        public IAstRefOrValue trueBranch;
        public IAstRefOrValue falseBranch;

        #region IAstNode Members

        public Type itemType
        {
            get 
            {
                return trueBranch.itemType;
            }
        }

        public AstIfTernar(IAstRefOrValue condition, IAstRefOrValue trueBranch, IAstRefOrValue falseBranch)
        {
            if (trueBranch.itemType != falseBranch.itemType)
            {
                throw new EmitMapperException("Types mismatch");
            }

            this.condition = condition;
            this.trueBranch = trueBranch;
            this.falseBranch = falseBranch;
        }

        public void Compile(CompilationContext context)
        {
            Label elseLabel = context.ilGenerator.DefineLabel();
            Label endIfLabel = context.ilGenerator.DefineLabel();

            condition.Compile(context);
            context.Emit(OpCodes.Brfalse, elseLabel);

            if (trueBranch != null)
            {
                trueBranch.Compile(context);
            }
            if (falseBranch != null)
            {
                context.Emit(OpCodes.Br, endIfLabel);
            }

            context.ilGenerator.MarkLabel(elseLabel);
            if (falseBranch != null)
            {
                falseBranch.Compile(context);
            }
            context.ilGenerator.MarkLabel(endIfLabel);
        }

        #endregion
    }
}
