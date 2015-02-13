﻿using System;
using System.Linq;

namespace Oct.Framework.DB.Emit.MappingConfiguration
{
    public class DefaultCustomConverterProvider: ICustomConverterProvider
    {
        private Type _converterType;

        public DefaultCustomConverterProvider(Type converterType)
        {
            _converterType = converterType;
        }

        public virtual CustomConverterDescriptor GetCustomConverterDescr(
			Type from, 
			Type to, 
			MapConfigBaseImpl mappingConfig)
        {
            return new CustomConverterDescriptor
            {
                ConverterClassTypeArguments = GetGenericArguments(from).Concat(GetGenericArguments(to)).ToArray(),
                ConverterImplementation = _converterType,
                ConversionMethodName = "Convert"
            };
        }

        public static Type[] GetGenericArguments(Type type)
        {
            if (type.IsArray)
            {
                return new[] { type.GetElementType() };
            }
            if (type.IsGenericType)
            {
                return type.GetGenericArguments();
            }
            return type
                .GetInterfaces()
                .Where(i => i.IsGenericType)
                .Select(i => i.GetGenericArguments())
                .Where(a => a.Length == 1)
                .Select(a => a[0])
                .ToArray();
        }
    }
}
