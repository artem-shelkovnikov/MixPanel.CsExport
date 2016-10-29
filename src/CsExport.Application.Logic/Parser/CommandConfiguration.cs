using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using CsExport.Application.Logic.Parser.Utility;

namespace CsExport.Application.Logic.Parser
{
	public abstract class CommandConfiguration<TCommandArguments> : ICommandConfiguration
		where TCommandArguments : IArguments, new()
	{
		private readonly CommandDefinition _configuration;

		protected CommandConfiguration()
		{
			_configuration = new CommandDefinition(typeof(TCommandArguments));
		}

		internal CommandDefinition Configuration => _configuration;

		protected void HasSignature(string commandSignature)
		{
			_configuration.Signature = commandSignature;
		}

		protected void HasDescription(string description)
		{
			_configuration.Description = description;
		}

		protected ParameterConfiguration HasArgument<TProperty>(Expression<Func<TCommandArguments, TProperty>> argumentFunc)
		{
			var propertyInfo = PropertyHelper<TCommandArguments>.GetProperty(argumentFunc);

			var parameterDefinition = new ParameterDefinition
			{
				Signature = GetDefaultName(propertyInfo),
				PropertyInfo = propertyInfo
			};

			var propertyConfiguration = new ParameterConfiguration(parameterDefinition);

			_configuration.AddParameterDefinition(parameterDefinition);

			return propertyConfiguration;
		}

		private string GetDefaultName(PropertyInfo propertyInfo)
		{
			var propertyName = propertyInfo.Name;

			var parameterName = new StringBuilder();
			for (int index = 0; index < propertyName.Length; index++)
			{
				var character = propertyName[index];
				if (index == 0
				    || char.IsUpper(character) == false)
					parameterName.Append(char.ToLowerInvariant(character));
				else
					parameterName.AppendFormat("-{0}", char.ToLowerInvariant(character));
			}

			return parameterName.ToString();
		}
	}
}