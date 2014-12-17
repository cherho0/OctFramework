using System;

namespace Oct.Framework.Core.MEF
{
    public class ExportAttribute : System.ComponentModel.Composition.ExportAttribute
    {
        public ExportAttribute(Type contractType) :
            base(contractType) { }

        public ExportAttribute(string contractName, Type contractType) :
            base(contractName, contractType) { }
    }
}
