using System;

namespace Oct.Framework.DB.Emit.MappingConfiguration
{
    public interface ICustomConverter
    {
        void Initialize(Type from, Type to, MapConfigBaseImpl mappingConfig);
    }
}
