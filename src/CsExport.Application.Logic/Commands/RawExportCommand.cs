using System;
using CsExport.Application.Logic.CommandArguments;
using CsExport.Application.Logic.Results;

namespace CsExport.Application.Logic.Commands
{
	public class RawExportCommand : ICommand
	{
		private readonly RawExportCommandArguments _arguments;	   

		public RawExportCommand(RawExportCommandArguments arguments)
		{
			if (arguments == null)
				throw new ArgumentNullException(nameof(arguments));

			if (arguments.From.GetDateTime() > arguments.To.GetDateTime())
				throw new ArgumentException("@from should represent a date less than @to", nameof(arguments.From));

			_arguments = arguments;	 
		}
			 
		public CommandResult Execute(ExecutionSettings settings)
		{
			var mixPanelClient = settings.MixPanelClient;
			var clientConfiguration = settings.ClientConfiguration;
			var applicationConfiguration = settings.ApplicationConfiguration;
			var fileWriter = settings.FileWriter;

			if (clientConfiguration == null || string.IsNullOrWhiteSpace(clientConfiguration.Secret))
				return new UnauthorizedResult();

			var content = mixPanelClient.ExportRaw(clientConfiguration, _arguments.From, _arguments.To, _arguments.Events);

			fileWriter.WriteContent(applicationConfiguration.ExportPath, string.Format("{0}-raw-export.txt", DateTime.Now.ToFileTimeUtc()), content);
			 
			return new SuccessResult();
		}

		public override bool Equals(object obj)
		{
			var source = this;
			var target = obj as RawExportCommand;

			if (target == null)
				return false;

			return source._arguments.Equals(target._arguments);
		}

		public override int GetHashCode()
		{
			return _arguments.GetHashCode();
		}
	}
}