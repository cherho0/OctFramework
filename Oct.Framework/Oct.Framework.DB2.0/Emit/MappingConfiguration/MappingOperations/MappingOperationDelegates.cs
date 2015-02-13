﻿namespace Oct.Framework.DB.Emit.MappingConfiguration.MappingOperations
{
    public delegate TResult NullSubstitutor<TResult>(object state);
    public delegate TResult TargetConstructor<TResult>();
	public delegate TResult ValueConverter<TValue, TResult>(TValue value, object state);
	public delegate TValue ValuesPostProcessor<TValue>(TValue value, object state);
}
