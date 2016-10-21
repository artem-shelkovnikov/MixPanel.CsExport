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
		public void Ctor_When_called_with_null_arguments_Then_throws()
		{										
			var command = new SetCredentialsCommand(MixPanelClientMock.Object);
													  
			Assert.Throws<ArgumentNullException>(() => command.Execute(ApplicationConfiguration, ClientConfiguration, null));
		}

		[Fact]
		public void Execute_When_called_with_settings_Then_swaps_current_clientConfiguration_with_new()
		{								
			var command = GetCommand();
			var arguments = GetArguments(ValidSecret);

			command.Execute(ApplicationConfiguration, ClientConfiguration, arguments);
																	
			Assert.Equal(ValidSecret, ClientConfiguration.Secret);
		}

		[Fact]
		public void Execute_When_called_with_settings_Then_returns_successResult()
		{										
			var command = GetCommand();
			var arguments = GetArguments(ValidSecret);

			var result = command.Execute(ApplicationConfiguration, ClientConfiguration, arguments);

			Assert.IsType<SuccessResult>(result);
		}

		[Fact]
		public void Execute_When_called_with_invalid_secret_Then_returns_unauthorizedResponse()
		{										 
			var command = GetCommand();
			var arguments = GetArguments(InvalidSecret);

			var result = command.Execute(ApplicationConfiguration, ClientConfiguration, arguments);

			Assert.IsType<UnauthorizedResult>(result);

		}	

		private SetCredentialsCommand GetCommand()
		{
			return new SetCredentialsCommand(MixPanelClientMock.Object);
		}

		private SetCredentialsCommandArguments GetArguments(string secret)
		{
			return new SetCredentialsCommandArguments {Secret = secret};
		}
	}
}