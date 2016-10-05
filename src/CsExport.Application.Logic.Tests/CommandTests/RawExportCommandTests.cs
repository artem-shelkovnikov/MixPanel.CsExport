using System;
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

			var result = command.Execute(GetExecutionSettings());

			Assert.IsType<SuccessResult>(result);
		}

		[Fact]
		public void Execute_When_called_with_valid_parameters_Then_calls_mixPanelClient()
		{
			var command = GetCommand();

			var result = command.Execute(GetExecutionSettings());

			MixPanelClientMock.Verify(x=>x.ExportRaw(ClientConfiguration, It.IsAny<Date>(), It.IsAny<Date>(), null), Times.Once);
		}

		[Fact]
		public void Execute_When_called_with_from_and_to_Then_passes_these_parameters_into_mixPanelClient_leaving_rest_parameters_null()
		{
			var @from = new Date(2010, 10, 31);
			var to = new Date(2011, 1, 1);

			var command = GetCommand(@from, to);

			command.Execute(GetExecutionSettings());
			
			MixPanelClientMock.Verify(x => x.ExportRaw(ClientConfiguration, @from, to, null), Times.Once);
		}

		[Fact]
		public void Execute_When_called_with_event_parameter_Then_passes_event_into_mixPanelClient()
		{
			var @from = new Date(2010, 10, 31);
			var to = new Date(2011, 1, 1);
			var @event = "some_event";

			var command = GetCommand(@from, to, new [] { @event });

			command.Execute(GetExecutionSettings());
			
			MixPanelClientMock.Verify(x => x.ExportRaw(ClientConfiguration, It.IsAny<Date>(), It.IsAny<Date>(), It.Is<string[]>(y=>y[0] == @event)), Times.Once);
		}

		[Fact]
		public void Execute_When_called_with_valid_parameters_Then_passes_string_received_from_mixPanelClient_to_fileWriter()
		{
			var exportContent = "some export content";
			MixPanelClientMock.Setup(x => x.ExportRaw(It.IsAny<ClientConfiguration>(), It.IsAny<Date>(), It.IsAny<Date>(), It.IsAny<string[]>()))
				.Returns(exportContent);

			var @from = new Date(2010, 10, 31);
			var to = new Date(2011, 1, 1);

			var command = GetCommand(@from, to);

			command.Execute(GetExecutionSettings());

			FileWriterMock.Verify(x=>x.WriteContent(ApplicationConfiguration.ExportPath, It.IsAny<string>(), exportContent));
		}

		[Fact]
		public void Ctor_When_called_with_wrong_date_range_Then_throws_argumentException()
		{
			var @from = new Date(2011, 1, 1);
			var to = new Date(2010, 10, 31);

			Assert.Throws<ArgumentException>(() => GetCommand(from, to));
		}

		[Fact]
		public void Execute_when_called_with_client_context_without_secret_specified_Then_returns_unauthorizedResult()
		{
			var command = GetCommand();					   
			var executionSettings = GetExecutionSettings();
			executionSettings.ClientConfiguration.UpdateCredentials(string.Empty);

			var result = command.Execute(executionSettings);

			Assert.IsType<UnauthorizedResult>(result);
		}						

		private RawExportCommand GetCommand(Date from, Date to, string[] events)
		{
			return new RawExportCommand(from, to, events);
		}							

		private RawExportCommand GetCommand(Date from, Date to)
		{
			return GetCommand(from, to, null);
		}	 

		private RawExportCommand GetCommand()
		{
			var @from = new Date(2011, 1, 1);
			var to = new Date(2011, 12, 31);

			return GetCommand(@from, to);
		}
	}
}