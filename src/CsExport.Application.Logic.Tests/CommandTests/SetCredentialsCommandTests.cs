using System;
using CsExport.Application.Logic.CommandArguments;
using CsExport.Application.Logic.Commands;
using CsExport.Application.Logic.Results;
using CsExport.Core.Settings;
using Moq;
using Xunit;

namespace CsExport.Application.Logic.Tests.CommandTests
{
	public class SetCredentialsCommandTests	: CommandTestsBase
	{
		private const string ValidSecret = "test secret";
		private const string InvalidSecret = "invalid secret";

		public SetCredentialsCommandTests()
		{
			MixPanelClientMock.Setup(x => x.VerifyCredentials(It.Is<ClientConfiguration>(y => y.Secret == ValidSecret))).Returns(true);
			MixPanelClientMock.Setup(x => x.VerifyCredentials(It.Is<ClientConfiguration>(y => y.Secret == InvalidSecret))).Returns(false);
		}

		[Fact]
		public void Ctor_When_called_with_null_Then_throws()
		{																					  
			Assert.Throws<ArgumentNullException>(() => new SetCredentialsCommand(null));
		}

		[Fact]
		public void Execute_When_called_with_settings_Then_swaps_current_clientConfiguration_with_new()
		{										  
			var testSecret = ValidSecret;
			var command = new SetCredentialsCommand(GetCommandArguments(testSecret));

			command.Execute(GetExecutionSettings());
																	
			Assert.Equal(testSecret, ClientConfiguration.Secret);
		}

		[Fact]
		public void Execute_When_called_with_settings_Then_returns_successResult()
		{										  
			var testSecret = ValidSecret;
			var command = new SetCredentialsCommand(GetCommandArguments(testSecret));

			var result = command.Execute(GetExecutionSettings());

			Assert.IsType<SuccessResult>(result);
		}

		[Fact]
		public void Execute_When_called_with_invalid_secret_Then_returns_unauthorizedResponse()
		{
			var invalidSecret = InvalidSecret;
			var command = new SetCredentialsCommand(GetCommandArguments(invalidSecret));	  

			var result = command.Execute(GetExecutionSettings());

			Assert.IsType<UnauthorizedResult>(result);

		}

		private SetCredentialsCommandArguments GetCommandArguments(string secret)
		{
			return new SetCredentialsCommandArguments {Secret = secret};
		}
	}
}