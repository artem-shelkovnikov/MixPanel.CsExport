using System;
using CsExport.Application.Logic.IO;
using CsExport.Application.Logic.Parser;
using CsExport.Application.Logic.Results;
using CsExport.Core.Exceptions;
using Moq;
using Xunit;

namespace CsExport.Application.Logic.Tests
{
	public class ExportConsoleApplicationTests
	{
		private readonly ConsoleApplication _application;
		private readonly Mock<ICommandParser> _commandParserMock = new Mock<ICommandParser>();
		private readonly Mock<IResultHandler> _resultHandlerMock = new Mock<IResultHandler>();
		private readonly Mock<IInput> _inputProviderMock = new Mock<IInput>();
		private readonly Mock<ICommand> _commandMock = new Mock<ICommand>();

		private const string ValidCommandText = "dummy";
		private const string InvalidCommandText = "invalid";

		public ExportConsoleApplicationTests()
		{
			_commandParserMock.Setup(x => x.ParseCommand(ValidCommandText)).Returns(_commandMock.Object);

			_application = new ConsoleApplication(_commandParserMock.Object,
			                                      _resultHandlerMock.Object,
			                                      _inputProviderMock.Object);
		}

		[Fact]
		public void ReceiveCommand_When_called_with_a_dummy_command_Then_uses_commandParser_to_parse_command()
		{
			_inputProviderMock.Setup(x => x.GetLine()).Returns(ValidCommandText);

			_application.ReadCommand();

			_commandParserMock.Verify(x => x.ParseCommand(ValidCommandText), Times.Once);
		}

		[Fact]
		public void ReceiveCommand_When_called_with_a_dummy_command_Then_writes_error_message_to_output()
		{
			_inputProviderMock.Setup(x => x.GetLine()).Returns(ValidCommandText);

			_application.ReadCommand();

			_resultHandlerMock.Verify(x => x.HandleResult(It.IsAny<CommandResult>()), Times.Once);
		}

		[Fact]
		public void ReceiveCommand_When_called_with_unrecognized_command_Then_writes_error_message_to_output()
		{
			_inputProviderMock.Setup(x => x.GetLine()).Returns(InvalidCommandText);

			_application.ReadCommand();

			_resultHandlerMock.Verify(x => x.HandleResult(It.IsAny<CommandNotFoundResult>()), Times.Once);
		}

		[Fact]
		public void ReceiveCommand_When_exception_is_thrown_by_component_Then_writes_output_for_commandTerminatedResult()
		{
			_inputProviderMock.Setup(x => x.GetLine()).Throws(new NotImplementedException());

			_application.ReadCommand();

			_resultHandlerMock.Verify(x => x.HandleResult(It.IsAny<CommandTerminatedResult>()), Times.Once);
		}

		[Fact]
		public void
			ReceiveCommand_When_unauthorized_exception_is_thrown_by_component_Then_writes_output_for_unauthorizedResult()
		{
			_inputProviderMock.Setup(x => x.GetLine()).Returns(ValidCommandText);
			_commandMock.Setup(x => x.Execute())
			            .Throws<MixPanelUnauthorizedException>();

			_application.ReadCommand();

			_resultHandlerMock.Verify(x => x.HandleResult(It.IsAny<UnauthorizedResult>()), Times.Once);
		}
	}
}