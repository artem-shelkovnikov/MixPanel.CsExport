using System;

namespace CsExport.Application.Infrastructure
{
	public interface IExceptionHandlerConfiguration
	{
		void AddOrUpdate<TRecord>(Func<Exception, CommandResult> value) where TRecord : Exception;
		void Remove<TRecord>() where TRecord : Exception;
	}
}