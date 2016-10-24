using System;
using CsExport.Application.Logic.CommandArguments;
using CsExport.Application.Logic.Results;
using CsExport.Core.Client;
using CsExport.Core.Settings;

namespace CsExport.Application.Logic.Commands
{
	public class SetCredentialsCommand : ICommandWithArguments<SetCredentialsCommandArguments>
	{
		private readonly IMixPanelClient _mixPanelClient;

		public SetCredentialsCommand(IMixPanelClient mixPanelClient)
		{
			_mixPanelClient = mixPanelClient;
		}

		public CommandResult Execute(ApplicationConfiguration applicationConfiguration,
		                             ClientConfiguration clientConfiguration,
		                             SetCredentialsCommandArguments arguments)
		{
			if (arguments == null)
				throw new ArgumentNullException(nameof(arguments));

			if (string.IsNullOrWhiteSpace(arguments.Secret))
				throw new ArgumentException("Secret cannot be empty", nameof(arguments.Secret));


			var updatedConfiguration = new ClientConfiguration();
			updatedConfiguration.UpdateCredentials(arguments.Secret);

			if (_mixPanelClient.VerifyCredentials(updatedConfiguration))
			{
				clientConfiguration.UpdateCredentials(arguments.Secret);
				return new SuccessResult();
			}

			return new UnauthorizedResult();
		}
	}
}