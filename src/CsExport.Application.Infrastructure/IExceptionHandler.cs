using System;

namespace CsExport.Application.Infrastructure
{
	public interface IExceptionHandler
	{
		CommandResult HandleException(Exception ex);
	}
}