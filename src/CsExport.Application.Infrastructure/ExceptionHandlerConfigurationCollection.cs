using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CsExport.Application.Infrastructure
{
	public class ExceptionHandlerConfigurationCollection : IExceptionHandlerConfiguration
	{
		private readonly IDictionary<Type, Func<Exception, CommandResult>> _innerConfigurations =
			new ConcurrentDictionary<Type, Func<Exception, CommandResult>>();

		public bool ContainsValueForType<TException>() where TException : Exception
		{
			return ContainsValueForType(typeof(TException));
		}

		public bool ContainsValueForType(Type type)
		{
			if (TypeIsDerivedFromException(type) == false)
				throw TypeMustDeriveFromException();

			return _innerConfigurations.ContainsKey(type);
		}

		public Func<Exception, CommandResult> GetForType(Type type)
		{
			if (TypeIsDerivedFromException(type) == false)
				throw TypeMustDeriveFromException();

			return _innerConfigurations.ContainsKey(type)
				? _innerConfigurations[type]
				: null;
		}

		public Func<Exception, CommandResult> GetForType<TException>() where TException : Exception
		{
			return GetForType(typeof(TException));
		}

		public void AddOrUpdate<TRecord>(Func<Exception, CommandResult> value) where TRecord : Exception
		{
			_innerConfigurations[typeof(TRecord)] = value;
		}

		public void Remove<TRecord>() where TRecord : Exception
		{
			if (_innerConfigurations.ContainsKey(typeof(TRecord)) == false)
				throw new InvalidOperationException("Specified type does not exist");

			_innerConfigurations.Remove(typeof(TRecord));
		}

		private static bool TypeIsDerivedFromException(Type type)
		{
			return typeof(Exception).IsAssignableFrom(type) == false;
		}

		private static ArgumentException TypeMustDeriveFromException()
		{
			return new ArgumentException("Argument must derive from Exception class");
		}
	}
}