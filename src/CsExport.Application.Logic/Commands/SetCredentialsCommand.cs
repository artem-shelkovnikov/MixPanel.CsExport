using System;
using CsExport.Application.Logic.CommandArguments;
using CsExport.Application.Logic.Results;
using CsExport.Core.Client;
using CsExport.Core.Settings;

namespace CsExport.Application.Logic.Commands
{
	public class SetCredentialsCommand : ICommandWithArguments<SetCredentialsCommandArguments>
	{
		private readonly ClientConfiguration _clientConfiguration;
		private readonly IMixPanelClient _mixPanelClient;

		public SetCredentialsCommand(ClientConfiguration clientConfiguration, IMixPanelClient mixPanelClient)
		{
			_clientConfiguration = clientConfiguration;
			_mixPanelClient = mixPanelClient;
		}

		public CommandResult Execute(SetCredentialsCommandArguments arguments)
		{
			if (arguments == null)
				throw new ArgumentNullException(nameof(arguments));

			if (string.IsNullOrWhiteSpace(arguments.Secret))
				throw new ArgumentException("Secret cannot be empty", nameof(arguments.Secret));


			var updatedConfiguration = new ClientConfiguration();
			updatedConfiguration.UpdateCredentials(arguments.Secret);

			if (_mixPanelClient.VerifyCredentials(updatedConfiguration))
			{
				_clientConfiguration.UpdateCredentials(arguments.Secret);
				return new SuccessResult();
			}

			return new UnauthorizedResult();
		}
	}
}