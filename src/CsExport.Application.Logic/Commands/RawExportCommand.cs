using System;
using CsExport.Application.Logic.CommandArguments;
using CsExport.Application.Logic.IO;
using CsExport.Application.Logic.Results;
using CsExport.Core.Client;
using CsExport.Core.Settings;

namespace CsExport.Application.Logic.Commands
{
	public class RawExportCommand : ICommandWithArguments<RawExportCommandArguments>
	{
		private readonly IMixPanelClient _mixPanelClient;						
		private readonly IFileWriter _fileWriter;							   

		public RawExportCommand(IMixPanelClient mixPanelClient, IFileWriter fileWriter)
		{
			_mixPanelClient = mixPanelClient;						  
			_fileWriter = fileWriter;  
		}
			 
		public CommandResult Execute(ApplicationConfiguration applicationConfiguration, ClientConfiguration clientConfiguration, RawExportCommandArguments arguments)
		{
			if (arguments == null)
				throw new ArgumentNullException(nameof(arguments));

			if (arguments.From.GetDateTime() > arguments.To.GetDateTime())
				throw new ArgumentException("@from should represent a date less than @to", nameof(arguments.From));	   

			if (clientConfiguration == null || string.IsNullOrWhiteSpace(clientConfiguration.Secret))
				return new UnauthorizedResult();

			var content = _mixPanelClient.ExportRaw(clientConfiguration, arguments.From, arguments.To, arguments.Events);

			_fileWriter.WriteContent(applicationConfiguration.ExportPath, $"{DateTime.Now.ToFileTimeUtc()}-raw-export.txt", content);
			 
			return new SuccessResult();
		}
	}
}