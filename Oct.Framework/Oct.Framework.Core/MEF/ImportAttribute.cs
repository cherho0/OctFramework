
namespace Oct.Framework.Core.MEF
{
    public class ImportAttribute : System.ComponentModel.Composition.ImportAttribute
    {
        public ImportAttribute()
        {
            AllowDefault = true;
            AllowRecomposition = true;
        }
    }
}
