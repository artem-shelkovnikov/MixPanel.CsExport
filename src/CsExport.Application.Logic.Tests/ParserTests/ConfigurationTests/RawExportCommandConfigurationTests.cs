using CsExport.Application.Logic.Commands;
using CsExport.Application.Logic.Parser;
using CsExport.Application.Logic.Parser.Configuration;
using Xunit;

namespace CsExport.Application.Logic.Tests.ParserTests.ConfigurationTests
{
	public class RawExportCommandConfigurationTests
	{
		ICommandParserConfiguration _configuration = new RawExportCommandConfiguration();

		[Fact]
		public void TryParse_When_called_with_invalid_string_Then_returns_null()
		{
			var result = _configuration.TryParse("asdf");

			Assert.Null(result);
		}

		[Fact]
		public void TryParse_When_called_with_valid_arguments_Then_returns_command()
		{
			var result = _configuration.TryParse("raw-export -from=2016-01-01 -to=2016-01-03");
			Assert.NotNull(result);
			Assert.IsType<RawExportCommand>(result);
		}  
	}
}