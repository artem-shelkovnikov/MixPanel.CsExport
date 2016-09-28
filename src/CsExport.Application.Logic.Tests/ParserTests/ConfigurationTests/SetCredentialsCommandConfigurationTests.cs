using CsExport.Application.Logic.Commands;
using CsExport.Application.Logic.Parser;
using CsExport.Application.Logic.Parser.Configuration;
using Xunit;

namespace CsExport.Application.Logic.Tests.ParserTests.ConfigurationTests
{
	public class SetCredentialsCommandConfigurationTests
	{
		 ICommandParserConfiguration _configuration = new SetCredentialsCommandConfiguration();

		[Fact]
		public void TryParse_When_called_with_invalid_string_Then_returns_null()
		{
			var result = _configuration.TryParse("asdf");

			Assert.Null(result);
		}

		[Fact]
		public void TryParse_When_called_with_valid_arguments_Then_returns_command()
		{
			var result = _configuration.TryParse("set-credentials -apiKey=api -secret=test");
			Assert.NotNull(result);
			Assert.IsType<SetCredentialsCommand>(result);
		}

		[Fact]
		public void TryParse_When_called_with_valid_arguments_with_inconsistent_case_Then_returns_command()
		{
			var result = _configuration.TryParse("sEt-CrEdEnTialS -APiKey=api -SecRet=test");
			Assert.NotNull(result);
			Assert.IsType<SetCredentialsCommand>(result);
		}

		[Fact]
		public void TryParse_When_called_with_missing_argument_Then_returns_null()
		{
			var result = _configuration.TryParse("sEt-CrEdEnTialS -APiKey=api");
			Assert.Null(result);						   
		}
	}
}