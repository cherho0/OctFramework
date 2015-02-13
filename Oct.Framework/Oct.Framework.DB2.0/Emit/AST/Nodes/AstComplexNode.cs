using System.Collections.Generic;
using Oct.Framework.DB.Emit.AST.Interfaces;

namespace Oct.Framework.DB.Emit.AST.Nodes
{
    class AstComplexNode: IAstNode
    {
        public List<IAstNode> nodes = new List<IAstNode>();

        public void Compile(CompilationContext context)
        {
            foreach (IAstNode node in nodes)
            {
                if (node != null)
                {
                    node.Compile(context);
                }
            }
        }
    }
}