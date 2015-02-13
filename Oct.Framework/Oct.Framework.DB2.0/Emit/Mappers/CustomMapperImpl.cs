using System;
using Oct.Framework.DB.Emit.MappingConfiguration;

namespace Oct.Framework.DB.Emit.Mappers
{
    internal abstract class CustomMapperImpl: ObjectsMapperBaseImpl
    {
        public CustomMapperImpl(
            ObjectMapperManager mapperMannager, 
            Type TypeFrom, 
            Type TypeTo, 
            IMappingConfigurator mappingConfigurator,
			object[] storedObjects)
        {
			Initialize(mapperMannager, TypeFrom, TypeTo, mappingConfigurator, storedObjects);
        }
    }
}