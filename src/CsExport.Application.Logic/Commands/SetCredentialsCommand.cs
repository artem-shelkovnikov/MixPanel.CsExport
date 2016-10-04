using System;
using CsExport.Application.Logic.Results;
using CsExport.Core.Settings;

namespace CsExport.Application.Logic.Commands
{
	public class SetCredentialsCommand: ICommand
	{										
		private readonly string _secret;

		public SetCredentialsCommand(string secret)
		{																			   
			if (string.IsNullOrWhiteSpace(secret))
				throw new ArgumentException("Secret cannot be empty", nameof(secret));
									
			_secret = secret;
		}
		
		public CommandResult Execute(ExecutionSettings settings)
		{
			var clientConfiguration = settings.ClientConfiguration;
			var mixPanelClient = settings.MixPanelClient;

			var updatedConfiguration = new ClientConfiguration();
			updatedConfiguration.UpdateCredentials(_secret);

			if (mixPanelClient.VerifyCredentials(updatedConfiguration))
			{
				clientConfiguration.UpdateCredentials(_secret);
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

			return source._secret.Equals(target._secret);
		}

		public override int GetHashCode()
		{
			return  _secret.GetHashCode();
		}
	}
}