using System.Linq;
using CsExport.Application.Logic.CommandArguments;
using CsExport.Application.Logic.Commands;
using CsExport.Application.Logic.Parser;
using CsExport.Application.Logic.Parser.Configuration;
using CsExport.Application.Logic.Parser.Utility;
using CsExport.Core;
using Xunit;

namespace CsExport.Application.Logic.Tests.ParserTests.ConfigurationTests
{
	public class RawExportCommandConfigurationTests
	{
		ICommandParserConfiguration _configuration = new RawExportCommandConfiguration();

		[Fact]
		public void TryParse_When_called_without_argumentsThen_returns_null_for_all_fields()
		{  
			var result = _configuration.TryParse(Enumerable.Empty<CommandArgument>()) as RawExportCommandArguments;

			Assert.NotNull(result);
			Assert.Null(result.From);
			Assert.Null(result.To);
			Assert.Null(result.Events);
		}

		[Fact]
		public void TryParse_When_called_with_valid_arguments_Then_returns_command()
		{
			var arguments = new[]
			{
				new CommandArgument {ArgumentName = "from", Value = "2016-01-01"},
				new CommandArgument {ArgumentName = "to", Value = "2016-01-03"}
			};

			var result = _configuration.TryParse(arguments) as RawExportCommandArguments;

			Assert.NotNull(result);
			Assert.Equal(new Date(2016, 1, 1), result.From);
			Assert.Equal(new Date(2016, 1, 3), result.To);
			Assert.Null(result.Events);
		}
	}
}