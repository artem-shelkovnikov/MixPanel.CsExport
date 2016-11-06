using System;

namespace CsExport.Application.Infrastructure
{
	internal class ExceptionHandler : IExceptionHandler
	{
		private readonly ExceptionHandlerConfigurationCollection _handlerConfigurationCollection;

		public ExceptionHandler(ExceptionHandlerConfigurationCollection handlerConfigurationCollection)
		{
			_handlerConfigurationCollection = handlerConfigurationCollection;
		}

		public CommandResult HandleException(Exception ex)
		{
			if (_handlerConfigurationCollection.ContainsValueForType(ex.GetType()))
				return _handlerConfigurationCollection.GetForType(ex.GetType())(ex);

			return _handlerConfigurationCollection.GetForType(typeof(Exception))(ex);
		}
	}
}