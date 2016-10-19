using System;
using CsExport.Application.Logic.CommandArguments;
using CsExport.Application.Logic.Results;
using CsExport.Core.Settings;

namespace CsExport.Application.Logic.Commands
{
	public class SetCredentialsCommand: ICommand
	{
		private readonly SetCredentialsCommandArguments _arguments;		

		public SetCredentialsCommand(SetCredentialsCommandArguments arguments)
		{
			if (arguments == null)
				throw new ArgumentNullException(nameof(arguments));

			if (string.IsNullOrWhiteSpace(arguments.Secret))
				throw new ArgumentException("Secret cannot be empty", nameof(arguments.Secret));

			_arguments = arguments;
		}
		
		public CommandResult Execute(ExecutionSettings settings)
		{
			var clientConfiguration = settings.ClientConfiguration;
			var mixPanelClient = settings.MixPanelClient;

			var updatedConfiguration = new ClientConfiguration();
			updatedConfiguration.UpdateCredentials(_arguments.Secret);

			if (mixPanelClient.VerifyCredentials(updatedConfiguration))
			{
				clientConfiguration.UpdateCredentials(_arguments.Secret);
				return new SuccessResult();
			}

			return new UnauthorizedResult();
		}

		public override bool Equals(object obj)
		{
			var source = this;
			var target = obj as SetCredentialsCommand;

			if (target == null)
				return false;

			return source._arguments.Equals(target._arguments);
		}

		public override int GetHashCode()
		{
			return  _arguments.GetHashCode();
		}
	}
}