using System;
using System.Linq;
using CsExport.Application.Logic.Commands;
using CsExport.Core;

namespace CsExport.Application.Logic.Parser.Configuration
{
	public class RawExportCommandConfiguration	: CommandParserConfigurationBase
	{  
		protected override ICommand ParseInner(CommandStructure commandStructure)
		{
			if (commandStructure.CommandName.Equals("raw-export", StringComparison.InvariantCultureIgnoreCase) == false)
				return null;

			DateTime from, to;

			var fromArgument = commandStructure.Arguments.FirstOrDefault(x => x.ArgumentName.Equals("from", StringComparison.InvariantCultureIgnoreCase));
			if (fromArgument == null)
				return null;

			if (DateTime.TryParse(fromArgument.Value, out from) == false)
				return null;
											

			var toArgument = commandStructure.Arguments.FirstOrDefault(x => x.ArgumentName.Equals("to", StringComparison.InvariantCultureIgnoreCase));
			if (toArgument == null)
				return null;

			if (DateTime.TryParse(fromArgument.Value, out to) == false)
				return null;			   

			return new RawExportCommand(new Date(from), new Date(to));
		}
	}
}