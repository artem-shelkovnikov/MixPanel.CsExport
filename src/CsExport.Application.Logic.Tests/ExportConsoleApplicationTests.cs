using System;
using CsExport.Application.Logic.Parser;
using CsExport.Application.Logic.Results;
using CsExport.Core.Client;
using Moq;
using Xunit;

namespace CsExport.Application.Logic.Tests
{
	public class ExportConsoleApplicationTests
	{
		private readonly ExportConsoleApplication _application;
		private readonly Mock<ICommandParser> _commandParserMock = new Mock<ICommandParser>();
		private readonly Mock<IResultHandler> _resultHandlerMock = new Mock<IResultHandler>();
		private readonly Mock<IMixPanelClient> _mixPanelClientMock = new Mock<IMixPanelClient>();
		private readonly Mock<IInputProvider> _inputProviderMock = new Mock<IInputProvider>();

		private const string ValidCommandText = "dummy";
		private const string InvalidCommandText = "invalid";

		public ExportConsoleApplicationTests()
		{
			_commandParserMock.Setup(x => x.ParseCommand(ValidCommandText)).Returns(new DummyCommand());

			_application = new ExportConsoleApplication(_commandParserMock.Object, _mixPanelClientMock.Object, _resultHandlerMock.Object, _inputProviderMock.Object);
		}

		[Fact]
		public void ReceiveCommand_When_called_with_a_dummy_command_Then_uses_commandParser_to_parse_command()
		{
			_inputProviderMock.Setup(x => x.GetInput()).Returns(ValidCommandText);

			_application.ReceiveCommand();

			_commandParserMock.Verify(x=>x.ParseCommand(ValidCommandText), Times.Once);
		}

		[Fact]
		public void ReceiveCommand_When_called_with_a_dummy_command_Then_writes_error_message_to_output()
		{
			_inputProviderMock.Setup(x => x.GetInput()).Returns(ValidCommandText);

			_application.ReceiveCommand();

			_resultHandlerMock.Verify(x=>x.HandleResult(It.IsAny<CommandResult>()), Times.Once);
		}

		[Fact]
		public void ReceiveCommand_When_called_with_unrecognized_command_Then_writes_error_message_to_output()
		{
			_inputProviderMock.Setup(x => x.GetInput()).Returns(InvalidCommandText);

			_application.ReceiveCommand();

			_resultHandlerMock.Verify(x=>x.HandleResult(It.IsAny<CommandNotFoundResult>()), Times.Once);
		}

		[Fact]
		public void ReceiveCommand_When_exception_is_thrown_by_component_Then_writes_error_message_to_output()
		{
			_inputProviderMock.Setup(x => x.GetInput()).Throws(new NotImplementedException());

			_application.ReceiveCommand();

			_resultHandlerMock.Verify(x=>x.HandleResult(It.IsAny<CommandTerminatedResult>()), Times.Once);
		}

		private class DummyCommand : ICommand
		{
			public CommandResult Execute(IMixPanelClient client, IInputProvider inputProvider)
			{
				return new SuccessResult();
			}
		}
	}
}