﻿using System;
using Oct.Framework.DB.Emit.MappingConfiguration.MappingOperations.Interfaces;

namespace Oct.Framework.DB.Emit.MappingConfiguration.MappingOperations
{
	public class RootMappingOperation: IRootMappingOperation
	{
		#region IRootMappingOperation Members
		public Type From { get; set; }
		public Type To { get; set; }
        public Delegate NullSubstitutor { get; set; }
        public Delegate TargetConstructor { get; set; }
        public Delegate Converter { get; set; }
		public bool ShallowCopy { get; set; }
        public Delegate ValuesPostProcessor { get; set; }
		#endregion

		public RootMappingOperation(Type from, Type to)
		{
			From = from;
			To = to;
		}
	}
}
