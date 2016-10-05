using System.Linq;
using CsExport.Application.Logic.Commands;
using CsExport.Application.Logic.Parser;
using CsExport.Application.Logic.Parser.Configuration;
using CsExport.Core;
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

		[Fact]
		public void TryParse_When_called_with_valid_arguments_Then_sets_these_arguments_to_correct_command_fields()
		{
			var @from = new Date(2016, 1, 1);
			var to = new Date(2016, 1, 3);
			var events = "first; second; third";
			var commandText = string.Format("raw-export -from={0} -to={1} -events={2}", from, to, events);
			
			var result = _configuration.TryParse(commandText);
			
			var command = new RawExportCommand(from, to, events.Split(';').Select(x=>x.Trim()).ToArray());
			Assert.Equal(command, result);
			
		}  
	}
}