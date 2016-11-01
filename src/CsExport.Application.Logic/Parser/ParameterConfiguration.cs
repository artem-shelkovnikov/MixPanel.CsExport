using System;
using System.Text.RegularExpressions;

namespace CsExport.Application.Logic.Parser
{
	public class ParameterConfiguration
	{
		private const int MaxSignatureLength = 30;
		private readonly ParameterDefinition _parameterDefinition;
		private static Regex _signatureValidationRegex = new Regex("^[a-z0-9-]+$");

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

			if (_signatureValidationRegex.Match(signature).Value != signature)
				throw new ArgumentException("Parameter signature must contain only lower case letter, numbers and dashes", nameof(signature));

			if (signature.Length > MaxSignatureLength)
				throw new ArgumentException($"Signature maximum length is {MaxSignatureLength}", nameof(signature));

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