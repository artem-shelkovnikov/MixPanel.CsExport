using System;
using System.Linq.Expressions;
using CsExport.Application.Infrastructure.Utility;

namespace CsExport.Application.Infrastructure.FluentConfiguration
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

		protected void HasSignature(string signature)
		{
			_configuration.SetSignature(signature);
		}

		protected void HasDescription(string description)
		{
			_configuration.SetDescription(description);
		}

		protected ParameterConfiguration HasArgument<TProperty>(Expression<Func<TCommandArguments, TProperty>> argumentFunc)
		{
			var propertyInfo = PropertyHelper<TCommandArguments>.GetProperty(argumentFunc);

			return _configuration.AddParameterDefinition(propertyInfo);
		}
	}
}