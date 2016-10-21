using System;
using CsExport.Application.Logic.CommandArguments;
using CsExport.Application.Logic.Commands;
using CsExport.Application.Logic.Results;
using CsExport.Core;
using CsExport.Core.Settings;
using Moq;
using Xunit;

namespace CsExport.Application.Logic.Tests.CommandTests
{
	public class RawExportCommandTests : CommandTestsBase
	{  
		[Fact]
		public void Execute_When_called_with_valid_parameters_Then_returns_correct_result_type()
		{
			var command = GetCommand();
			var arguments = GetArguments();

			var result = command.Execute(ApplicationConfiguration, ClientConfiguration, arguments);

			Assert.IsType<SuccessResult>(result);
		}

		[Fact]
		public void Execute_When_called_with_valid_parameters_Then_calls_mixPanelClient()
		{
			var command = GetCommand();
			var arguments = GetArguments();

			var result = command.Execute(ApplicationConfiguration, ClientConfiguration, arguments);

			MixPanelClientMock.Verify(x=>x.ExportRaw(ClientConfiguration, It.IsAny<Date>(), It.IsAny<Date>(), null), Times.Once);
		}

		[Fact]
		public void Execute_When_called_with_from_and_to_Then_passes_these_parameters_into_mixPanelClient_leaving_rest_parameters_null()
		{
			var from = new Date(2010, 10, 31);
			var to = new Date(2011, 1, 1);

			var command = GetCommand();
			var arguments = GetArguments(from, to);

			var result = command.Execute(ApplicationConfiguration, ClientConfiguration, arguments);

			MixPanelClientMock.Verify(x => x.ExportRaw(ClientConfiguration, from, to, null), Times.Once);
		}

		[Fact]
		public void Execute_When_called_with_event_parameter_Then_passes_event_into_mixPanelClient()
		{
			var from = new Date(2010, 10, 31);
			var to = new Date(2011, 1, 1);
			var @event = "some_event";

			var command = GetCommand();
			var arguments = GetArguments(from, to, new[] {@event});

			var result = command.Execute(ApplicationConfiguration, ClientConfiguration, arguments);

			MixPanelClientMock.Verify(x => x.ExportRaw(ClientConfiguration, It.IsAny<Date>(), It.IsAny<Date>(), It.Is<string[]>(y=>y[0] == @event)), Times.Once);
		}

		[Fact]
		public void Execute_When_called_with_valid_parameters_Then_passes_string_received_from_mixPanelClient_to_fileWriter()
		{
			var exportContent = "some export content";
			MixPanelClientMock.Setup(x => x.ExportRaw(It.IsAny<ClientConfiguration>(), It.IsAny<Date>(), It.IsAny<Date>(), It.IsAny<string[]>()))
				.Returns(exportContent);

			var from = new Date(2010, 10, 31);
			var to = new Date(2011, 1, 1);

			var command = GetCommand();
			var arguments = GetArguments(from, to);

			var result = command.Execute(ApplicationConfiguration, ClientConfiguration, arguments);

			FileWriterMock.Verify(x=>x.WriteContent(ApplicationConfiguration.ExportPath, It.IsAny<string>(), exportContent));
		}

		[Fact]
		public void Ctor_When_called_with_wrong_date_range_Then_throws_argumentException()
		{
			var from = new Date(2011, 1, 1);
			var to = new Date(2010, 10, 31);
			var arguments = GetArguments(from, to);

			var command = GetCommand();

			Assert.Throws<ArgumentException>(() => command.Execute(ApplicationConfiguration, ClientConfiguration, arguments));
		}

		[Fact]
		public void Execute_when_called_with_client_context_without_secret_specified_Then_returns_unauthorizedResult()
		{
			var command = GetCommand();
			var arguments = GetArguments();					 
			ClientConfiguration.UpdateCredentials(string.Empty);

			var result = command.Execute(ApplicationConfiguration, ClientConfiguration, arguments);

			Assert.IsType<UnauthorizedResult>(result);
		}						

		private RawExportCommand GetCommand()
		{
			return new RawExportCommand(MixPanelClientMock.Object, FileWriterMock.Object);
		}			 

		private RawExportCommandArguments GetArguments(Date from = null, Date to = null, string[] events = null)
		{
			return new RawExportCommandArguments
			{
				Events = events,
				From = from ?? new Date(2011, 1, 1),
				To = to	?? new Date(2011, 12, 31)
			};
		}	
	}
}