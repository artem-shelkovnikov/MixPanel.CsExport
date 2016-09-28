using System;
using CsExport.Application.Logic.Results;
using CsExport.Core.Client;
using CsExport.Core.Settings;

namespace CsExport.Application.Logic.Commands
{
	public class SetCredentialsCommand: ICommand
	{																 
		private string _apiKey;
		private string _secret;

		public SetCredentialsCommand(string apiKey, string secret)
		{
			if (string.IsNullOrWhiteSpace(apiKey))
				throw new ArgumentException("ApiKey cannot be empty", nameof(apiKey));

			if (string.IsNullOrWhiteSpace(secret))
				throw new ArgumentException("Secret cannot be empty", nameof(secret));

			_apiKey = apiKey;
			_secret = secret;
		}
		
		public CommandResult Execute(ExecutionSettings settings)
		{
			var clientConfiguration = settings.ClientConfiguration;

			clientConfiguration.UpdateCredentials(_apiKey, _secret);

			return new SuccessResult();
		}
	}
}