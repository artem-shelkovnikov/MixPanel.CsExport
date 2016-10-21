using System.Linq;
using CsExport.Application.Logic.CommandArguments;
using CsExport.Application.Logic.Parser;
using CsExport.Application.Logic.Parser.Configuration;
using CsExport.Application.Logic.Parser.Utility;
using Xunit;

namespace CsExport.Application.Logic.Tests.ParserTests.ConfigurationTests
{
	public class SetCredentialsCommandConfigurationTests
	{
		readonly ICommandParserConfiguration _configuration = new SetCredentialsCommandConfiguration();

		[Fact]
		public void TryParse_When_called_with_invalid_arguments_Then_returns_arguments_object_without_secret_set()
		{																					 
			var result = (SetCredentialsCommandArguments) _configuration.TryParse(Enumerable.Empty<CommandArgument>());

			Assert.Null(result.Secret);
		}

		[Fact]
		public void TryParse_When_called_with_valid_arguments_Then_returns_command()
		{
			var arguments = new[]
			{
				new CommandArgument {ArgumentName = "secret", Value = "test"}
			};

			var result = _configuration.TryParse(arguments) as SetCredentialsCommandArguments;
			Assert.NotNull(result);
			Assert.Equal(arguments.Single().Value, result.Secret);
		}

		[Fact]
		public void TryParse_When_called_with_valid_arguments_with_inconsistent_case_Then_returns_command()
		{
			var arguments = new[]
			{
				new CommandArgument {ArgumentName = "SecREt", Value = "test"}
			};
															 
			var result = _configuration.TryParse(arguments) as SetCredentialsCommandArguments;

			Assert.NotNull(result);
			Assert.Equal(arguments.Single().Value, result.Secret);
		}
	}
}