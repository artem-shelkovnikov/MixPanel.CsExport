using System.Linq;
using CsExport.Application.Logic.Parser;
using Moq;
using Xunit;

namespace CsExport.Application.Logic.Tests.ParserTests
{
	public class CommandParserTests
	{
		private readonly ICommandParser _commandParser;
		private readonly Mock<ICommandParserConfigurationRegistry> _commandParserConfigurationRegistryMock = new Mock<ICommandParserConfigurationRegistry>();

		public CommandParserTests()
		{
			_commandParser = new CommandParser(_commandParserConfigurationRegistryMock.Object);
		}

		[Fact]
		public void Parse_When_called_with_no_parserConfigurations_Then_returns_null()
		{
			var command = _commandParser.ParseCommand("test non-existing");
			Assert.Null(command);
		}

		[Fact]
		public void Parse_When_called_with_existing_parser_configurations_Then_uses_parserConfiguration_tryParse_for_each_configuration()
		{
			var configurationMocks = new[]
			{
				new Mock<ICommandParserConfiguration>(),
				new Mock<ICommandParserConfiguration>(),
				new Mock<ICommandParserConfiguration>()
			};
													   
			_commandParserConfigurationRegistryMock.Setup(x => x.GetAll()).Returns(configurationMocks.Select(x=>x.Object));

			var input = "test-existing";
			var command = _commandParser.ParseCommand(input);

			foreach (var configurationMock in configurationMocks)
			{
				configurationMock.Verify(x=>x.TryParse(input), Times.Once);
			}
		}

		[Fact]
		public void Parse_When_called_with_existing_parser_configurations_and_multiple_configurations_can_parse_command_Then_uses_first_configuration_only()
		{
			var configurationMocks = new[]
			{
				new Mock<ICommandParserConfiguration>(),
				new Mock<ICommandParserConfiguration>(),
				new Mock<ICommandParserConfiguration>()
			};

			foreach (var configurationMock in configurationMocks)
			{
				configurationMock.Setup(x => x.TryParse(It.IsAny<string>())).Returns(new Mock<ICommand>().Object);
			}

			_commandParserConfigurationRegistryMock.Setup(x => x.GetAll()).Returns(configurationMocks.Select(x => x.Object));
			
			var input = "test-existing";
			var command = _commandParser.ParseCommand(input);

			configurationMocks.First().Verify(x=>x.TryParse(input), Times.Once);
			configurationMocks.ElementAt(1).Verify(x=>x.TryParse(input), Times.Never);
			configurationMocks.ElementAt(2).Verify(x=>x.TryParse(input), Times.Never);
		}

		[Fact]
		public void Parse_When_called_with_existing_parser_configurations_and_multiple_configurations_can_parse_command_Then_returns_command_parsed_by_configuration()
		{
			var configurationMocks = new[]
			{
				new Mock<ICommandParserConfiguration>(),
				new Mock<ICommandParserConfiguration>(),
				new Mock<ICommandParserConfiguration>()
			};

			var commandMock = new Mock<ICommand>();
			
			foreach (var configurationMock in configurationMocks)
			{
				configurationMock.Setup(x => x.TryParse(It.IsAny<string>())).Returns(commandMock.Object);
			}

			_commandParserConfigurationRegistryMock.Setup(x => x.GetAll()).Returns(configurationMocks.Select(x => x.Object));
			
			var input = "test-existing";
			var command = _commandParser.ParseCommand(input);

			Assert.Equal(commandMock.Object, command);
		}
	}
}