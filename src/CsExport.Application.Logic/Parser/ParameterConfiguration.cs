using System;

namespace CsExport.Application.Logic.Parser
{
	public class ParameterConfiguration
	{
		private readonly ParameterDefinition _parameterDefinition;

		internal ParameterConfiguration(ParameterDefinition parameterDefinition)
		{
			if (parameterDefinition == null)
				throw new ArgumentNullException(nameof(parameterDefinition));

			if (parameterDefinition.PropertyInfo == null)
				throw new ArgumentNullException(nameof(parameterDefinition.PropertyInfo));

			_parameterDefinition = parameterDefinition;
		}

		public ParameterConfiguration WithSignature(string signature)
		{
			if (signature == null)
				throw new ArgumentNullException(nameof(signature));

			_parameterDefinition.Signature = signature;

			return this;
		}

		public ParameterConfiguration WithDescription(string description)
		{
			if (description == null)
				throw new ArgumentNullException(nameof(description));

			_parameterDefinition.Description = description;

			return this;
		}
	}
}