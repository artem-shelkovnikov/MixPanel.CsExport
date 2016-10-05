using System;
using System.Collections.Generic;
using CsExport.Application.Logic.Commands;
using CsExport.Application.Logic.Parser.Utility;

namespace CsExport.Application.Logic.Parser.Configuration
{
	public class HelpCommandConfiguration	: CommandParserConfigurationBase
	{  		 
		protected override ICommand ParseInner(string commandName, IEnumerable<CommandArgument> arguments)
		{
			if (commandName.Equals("help", StringComparison.InvariantCultureIgnoreCase) == false)
				return null;

			return new HelpCommand();
		}
	}
}