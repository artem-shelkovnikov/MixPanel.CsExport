using System;
using CsExport.Application.Logic.Results;
using CsExport.Core;
using CsExport.Core.Client;
using CsExport.Core.Settings;

namespace CsExport.Application.Logic.Commands
{
	public class RawExportCommand : ICommand
	{
		private readonly Date _from;
		private readonly Date _to;

		public RawExportCommand(Date @from, Date @to)
		{
			if (@from.GetDateTime() > to.GetDateTime())
				throw new ArgumentException("@from should represent a date less than @to", nameof(@from));

			_from = @from;
			_to = to;							
		}
			 
		public CommandResult Execute(ExecutionSettings settings)
		{
			var mixPanelClient = settings.MixPanelClient;
			var clientConfiguration = settings.ClientConfiguration;

			mixPanelClient.ExportRaw(clientConfiguration, _from, _to);

			return new SuccessResult();
		}
	}
}