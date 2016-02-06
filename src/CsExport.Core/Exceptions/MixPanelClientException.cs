using System;

namespace CsExport.Core.Exceptions
{
	public class MixPanelClientException : Exception
	{
		private const string ErrorMessage = "Failed to get data from MixPanel. See innerException for details.";

		public MixPanelClientException() : base(ErrorMessage)
		{
			
		}

		public MixPanelClientException(Exception innerException) : base(ErrorMessage, innerException)
		{
			
		}
	}
}