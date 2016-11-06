using System;
using CsExport.Application.Infrastructure.IO;
using CsExport.Application.Infrastructure.Parser;
using CsExport.Application.Infrastructure.Results;
using Moq;
using Xunit;

namespace CsExport.Application.Infrastructure.Tests
{
	public class ExportConsoleApplicationTests
	{
		private readonly ConsoleApplication _application;
		private readonly Mock<ICommandParser> _commandParserMock = new Mock<ICommandParser>();
		private readonly Mock<IResultHandler> _resultHandlerMock = new Mock<IResultHandler>();
		private readonly Mock<IExceptionHandler> _exceptionHandlerMock = new Mock<IExceptionHandler>();
		private readonly Mock<IInput> _inputProviderMock = new Mock<IInput>();
		private readonly Mock<ICommand> _commandMock = new Mock<ICommand>();
		private readonly StubResult _exceptionHandlerResult = new StubResult();

		private const string ValidCommandText = "dummy";
		private const string InvalidCommandText = "invalid";

		public ExportConsoleApplicationTests()
		{
			_commandParserMock.Setup(x => x.ParseCommand(ValidCommandText)).Returns(_commandMock.Object);

			_application = new ConsoleApplication(_commandParserMock.Object,
			                                      _resultHandlerMock.Object,
			                                      _inputProviderMock.Object,
			                                      _exceptionHandlerMock.Object);
			_exceptionHandlerMock.Setup(x => x.HandleException(It.IsAny<Exception>())).Returns(_exceptionHandlerResult);
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
		public void
			ReceiveCommand_When_exception_is_thrown_by_component_Then_exceptionHandler_calls_HandleException_instead_of_throwing()
		{
			var expectedException = new NotImplementedException();
			_inputProviderMock.Setup(x => x.GetLine()).Throws(expectedException);

			_application.ReadCommand();

			_exceptionHandlerMock.Verify(x => x.HandleException(expectedException), Times.Once);
		}

		[Fact]
		public void
			ReceiveCommand_When_exception_is_thrown_by_component_Then_handles_result_returned_by_exceptionHandler()
		{
			_inputProviderMock.Setup(x => x.GetLine()).Returns(ValidCommandText);
			_commandMock.Setup(x => x.Execute())
			            .Throws<NotImplementedException>();

			_application.ReadCommand();

			_resultHandlerMock.Verify(x => x.HandleResult(_exceptionHandlerResult), Times.Once);
		}

		private class StubResult : CommandResult
		{
			public override void Handle(IOutput output)
			{
				;
			}
		}
	}
}