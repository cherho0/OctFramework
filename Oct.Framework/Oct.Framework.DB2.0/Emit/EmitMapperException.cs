﻿using System;
using Oct.Framework.DB.Emit.MappingConfiguration.MappingOperations.Interfaces;

namespace Oct.Framework.DB.Emit
{
    public class EmitMapperException: ApplicationException
    {
		public IMappingOperation _mappingOperation = null;
        public EmitMapperException()
        { 
        }

        public EmitMapperException(string message)
            : base(message)
        {
        }

        public EmitMapperException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public EmitMapperException(string message, Exception innerException, IMappingOperation mappingOperation)
            : base(
                BuildMessage(message, mappingOperation), 
                innerException
                )
        {
			_mappingOperation = mappingOperation;
        }

        private static string BuildMessage(string message, IMappingOperation mappingOperation)
        {
            return message + " " + mappingOperation.ToString();
        }

    }
}