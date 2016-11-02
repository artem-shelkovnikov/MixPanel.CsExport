using System;
using System.Collections.Generic;
using System.Reflection;

namespace CsExport.Application.Logic.Parser
{
	public class CommandDefinition
	{
		private readonly List<ParameterDefinition> _parameters = new List<ParameterDefinition>();
		private readonly Type _commandType;
		private string _signature = string.Empty;
		private string _description = string.Empty;

		public string Signature => _signature;
		public string Description => _description;
		public Type Type => _commandType;
		public IEnumerable<ParameterDefinition> Parameters => _parameters;


		public CommandDefinition(Type commandArgumentsType)
		{
			_commandType = commandArgumentsType;
		}

		internal void SetSignature(string signature)
		{
			SignatureValidation.Validate(signature);
			_signature = signature;
		}

		internal void SetDescription(string description)
		{
			_description = description;
		}

		internal ParameterConfiguration AddParameterDefinition(PropertyInfo propertyInfo)
		{
			var parameterDefinition = new ParameterDefinition(propertyInfo);

			parameterDefinition.SetSignature(SignatureHelper.GetDefaultPropertySignature(propertyInfo));

			var propertyConfiguration = new ParameterConfiguration(parameterDefinition);

			_parameters.Add(parameterDefinition);

			return propertyConfiguration;
		}
	}
}