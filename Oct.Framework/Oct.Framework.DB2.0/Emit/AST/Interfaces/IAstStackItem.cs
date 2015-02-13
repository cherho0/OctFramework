using System;

namespace Oct.Framework.DB.Emit.AST.Interfaces
{
    interface IAstStackItem: IAstNode
    {
        Type itemType { get; }
    }
}