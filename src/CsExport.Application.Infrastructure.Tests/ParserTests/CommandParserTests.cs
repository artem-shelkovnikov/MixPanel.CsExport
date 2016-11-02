using CsExport.Application.Infrastructure.Parser;
using Moq;
using Xunit;

namespace CsExport.Application.Infrastructure.Tests.ParserTests
{
	public class CommandParserTests
	{
		private readonly ICommandParser _commandParser;

		private readonly Mock<ICommandConfigurationRegistry> _commandParserConfigurationRegistryMock =
			new Mock<ICommandConfigurationRegistry>();

		private readonly Mock<ICommandFactory> _commandFactoryMock = new Mock<ICommandFactory>();
		private readonly Mock<ICommandArgumentParser> _commandArgumentParserMock = new Mock<ICommandArgumentParser>();


		public CommandParserTests()
		{
			_commandParser = new CommandParser(_commandParserConfigurationRegistryMock.Object,
			                                   _commandFactoryMock.Object,
			                                   _commandArgumentParserMock.Object);
		}

		[Fact]
		public void Parse_When_called_with_no_parserConfigurations_Then_returns_null()
		{
			var command = _commandParser.ParseCommand("test -non-existing");
			Assert.Null(command);
		}

		[Fact]
		public void
			Parse_When_called_for_parserConfiguration_that_cannot_parse_input_Then_returns_null_without_calling_commandArgumentParser_tryParse
			()
		{
			var input = "test -non-existing";
			var definition = new CommandDefinition(typeof(string));

			_commandParserConfigurationRegistryMock.Setup(x => x.GetAll()).Returns(new[] { definition });
			_commandArgumentParserMock.Setup(x => x.CanParse(input, definition)).Returns(false);

			var command = _commandParser.ParseCommand(input);

			Assert.Null(command);
			_commandArgumentParserMock.Verify(x => x.Parse(input, It.IsAny<CommandDefinition>()), Times.Never);
		}

		[Fact]
		public void Parse_When_called_for_parserConfiguration_that_can_parse_input_Then_calls_commandArgumentParser_tryParse()
		{
			var input = "test -non-existing";
			var definition = new CommandDefinition(typeof(string));

			_commandParserConfigurationRegistryMock.Setup(x => x.GetAll()).Returns(new[] { definition });
			_commandArgumentParserMock.Setup(x => x.CanParse(input, definition)).Returns(true);

			var command = _commandParser.ParseCommand(input);

			_commandArgumentParserMock.Verify(x => x.Parse(input, definition), Times.Once);
		}

		[Fact]
		public void
			Parse_When_called_for_parserConfiguration_that_can_parse_input_and_parses_successfully_Then_calls_commandFactory_create_with_correct_arguments
			()
		{
			var input = "test -non-existing";
			var definition = new CommandDefinition(typeof(string));
			var argumentsMock = new Mock<IArguments>();

			_commandParserConfigurationRegistryMock.Setup(x => x.GetAll()).Returns(new[] { definition });
			_commandArgumentParserMock.Setup(x => x.CanParse(input, definition)).Returns(true);
			_commandArgumentParserMock.Setup(x => x.Parse(input, definition)).Returns(argumentsMock.Object);

			var command = _commandParser.ParseCommand(input);

			_commandFactoryMock.Verify(x => x.Create(argumentsMock.Object), Times.Once);
		}

		[Fact]
		public void
			Parse_When_called_for_parserConfiguration_that_can_parse_input_and_parses_successfully_Then_returns_command_returned_from_commandFactory
			()
		{
			var input = "test -non-existing";
			var definition = new CommandDefinition(typeof(string));
			var argumentsMock = new Mock<IArguments>();
			var commandMock = new Mock<ICommand>();

			_commandParserConfigurationRegistryMock.Setup(x => x.GetAll()).Returns(new[] { definition });
			_commandArgumentParserMock.Setup(x => x.CanParse(input, definition)).Returns(true);
			_commandArgumentParserMock.Setup(x => x.Parse(input, definition)).Returns(argumentsMock.Object);
			_commandFactoryMock.Setup(x => x.Create(argumentsMock.Object)).Returns(commandMock.Object);

			var command = _commandParser.ParseCommand(input);

			Assert.Equal(commandMock.Object, command);
		}
	}
}