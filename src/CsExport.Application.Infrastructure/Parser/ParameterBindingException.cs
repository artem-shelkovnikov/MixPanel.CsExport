using System;

namespace CsExport.Application.Infrastructure.Parser
{
	public class ParameterBindingException : Exception
	{
		public string ParameterName { get; }

		public ParameterBindingException()
		{
		}

		public ParameterBindingException(Exception innerException) : base("Failed to bind one of parameters", innerException)
		{
		}

		public ParameterBindingException(string parameterName) : base($"Failed to bind parameter: {parameterName}")
		{
			ParameterName = parameterName;
		}
	}
}