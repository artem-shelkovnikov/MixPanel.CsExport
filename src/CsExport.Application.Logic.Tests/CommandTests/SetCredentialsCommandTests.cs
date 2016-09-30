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
			Assert.Throws<ArgumentException>(() => new SetCredentialsCommand(null));
		}

		[Fact]
		public void Execute_When_called_with_settings_Then_swaps_current_clientConfiguration_with_new()
		{										  
			var testSecret = "test secret";
			var command = new SetCredentialsCommand(testSecret);

			command.Execute(GetExecutionSettings());
																	
			Assert.Equal(testSecret, ClientConfiguration.Secret);
		}
	}
}