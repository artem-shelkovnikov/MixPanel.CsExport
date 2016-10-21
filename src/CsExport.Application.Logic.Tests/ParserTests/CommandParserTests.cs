using System.Collections.Generic;
using System.Linq;
using CsExport.Application.Logic.Parser;
using CsExport.Application.Logic.Parser.Utility;
using Moq;
using Xunit;

namespace CsExport.Application.Logic.Tests.ParserTests
{
	public class CommandParserTests
	{
		private readonly ICommandParser _commandParser;
		private readonly Mock<ICommandParserConfigurationRegistry> _commandParserConfigurationRegistryMock = new Mock<ICommandParserConfigurationRegistry>();
		private readonly Mock<ICommandFactory> _commandFactoryMock = new Mock<ICommandFactory>();

		public CommandParserTests()
		{
			_commandParser = new CommandParser(_commandParserConfigurationRegistryMock.Object, _commandFactoryMock.Object);
		}

		[Fact]
		public void Parse_When_called_with_no_parserConfigurations_Then_returns_null()
		{
			var command = _commandParser.ParseCommand("test -non-existing");
			Assert.Null(command);
		}

		[Fact]
		public void Parse_When_called_with_existing_parser_configuration_Then_uses_parserConfiguration_tryParse_for_found_configuration()
		{
			var configurationMock = new Mock<ICommandParserConfiguration>();

			var input = "test-existing";
			_commandParserConfigurationRegistryMock.Setup(x => x.GetByName(input)).Returns(configurationMock.Object);

			var command = _commandParser.ParseCommand(input);

			configurationMock.Verify(x => x.TryParse(It.Is<IEnumerable<CommandArgument>>(y => y.Any() == false)), Times.Once); 
		} 

		[Fact]
		public void Parse_When_called_with_existing_parser_configurations_and_multiple_configurations_can_parse_command_Then_creates_command_using_arguments_received_from_parser()
		{
			var configurationMock = new Mock<ICommandParserConfiguration>();

			var argumentsMock = new Mock<IArguments>();	  

			var input = "test-existing";

			configurationMock.Setup(x => x.TryParse(It.IsAny<IEnumerable<CommandArgument>>())).Returns(argumentsMock.Object);
			_commandParserConfigurationRegistryMock.Setup(x => x.GetByName(input)).Returns(configurationMock.Object);
																									
			var command = _commandParser.ParseCommand(input);

			_commandFactoryMock.Verify(x => x.Create(argumentsMock.Object));
		}
	}
}