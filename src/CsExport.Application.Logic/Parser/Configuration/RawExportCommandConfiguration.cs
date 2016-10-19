using System;
using System.Collections.Generic;
using System.Linq;
using CsExport.Application.Logic.CommandArguments;
using CsExport.Application.Logic.Commands;
using CsExport.Application.Logic.Parser.Utility;
using CsExport.Core;

namespace CsExport.Application.Logic.Parser.Configuration
{
	public class RawExportCommandConfiguration	: CommandParserConfigurationBase
	{  		 
		protected override ICommand ParseInner(string commandName, IEnumerable<CommandArgument> arguments)
		{
			if (commandName.Equals("raw-export", StringComparison.InvariantCultureIgnoreCase) == false)
				return null;

			DateTime from, to;
			string[] events;

			var fromArgument = arguments.FirstOrDefault(x => x.ArgumentName.Equals("from", StringComparison.InvariantCultureIgnoreCase));
			if (fromArgument == null)
				return null;

			if (DateTime.TryParse(fromArgument.Value, out from) == false)
				return null;


			var toArgument = arguments.FirstOrDefault(x => x.ArgumentName.Equals("to", StringComparison.InvariantCultureIgnoreCase));
			if (toArgument == null)
				return null;

			if (DateTime.TryParse(toArgument.Value, out to) == false)
				return null;

			var eventsArgument =
				arguments.FirstOrDefault(x => x.ArgumentName.Equals("events", StringComparison.InvariantCultureIgnoreCase));

			if (eventsArgument != null)
			{
				events = eventsArgument.Value.Split(';').Select(x => x.Trim()).ToArray();
			}
			else
			{
				events = new string[0];
			}


			return new RawExportCommand(new RawExportCommandArguments
			{
				Events = events,
				From = new Date(from),
				To = new Date(to)
			});
		}
	}
}