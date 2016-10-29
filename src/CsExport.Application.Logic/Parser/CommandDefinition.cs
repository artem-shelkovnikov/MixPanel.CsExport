using System;
using System.Collections.Generic;

namespace CsExport.Application.Logic.Parser
{
	public class CommandDefinition
	{
		private readonly List<ParameterDefinition> _parameters = new List<ParameterDefinition>();
		public string Signature { get; internal set; }
		public string Description { get; internal set; }
		public Type Type { get; private set; }
		public IEnumerable<ParameterDefinition> Parameters => _parameters;


		public CommandDefinition(Type commandArgumentsType)
		{
			Type = commandArgumentsType;
		}

		internal void AddParameterDefinition(ParameterDefinition parameterDefinition)
		{
			_parameters.Add(parameterDefinition);
		}
	}
}