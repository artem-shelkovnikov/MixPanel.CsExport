using System;
using CsExport.Application.Logic.Commands;
using Xunit;

namespace CsExport.Application.Logic.Tests.CommandTests
{
	public class SetCredentialsCommandTests	: CommandTestsBase
	{
		[Fact]
		public void Ctor_When_called_with_null_Then_throws()
		{
			Assert.Throws<ArgumentException>(() => new SetCredentialsCommand(null, "test"));
			Assert.Throws<ArgumentException>(() => new SetCredentialsCommand("test", null));
		}

		[Fact]
		public void Execute_When_called_with_settings_Then_swaps_current_clientConfiguration_with_new()
		{
			var testApiKey = "test api key";
			var testSecret = "test secret";
			var command = new SetCredentialsCommand(testApiKey, testSecret);

			command.Execute(GetExecutionSettings());

			Assert.Equal(testApiKey, ClientConfiguration.ApiKey);
			Assert.Equal(testSecret, ClientConfiguration.Secret);
		}
	}
}