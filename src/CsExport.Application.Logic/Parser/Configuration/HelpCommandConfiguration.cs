using System;
using CsExport.Application.Logic.Commands;

namespace CsExport.Application.Logic.Parser.Configuration
{
	public class HelpCommandConfiguration	: CommandParserConfigurationBase
	{  
		protected override ICommand ParseInner(CommandStructure commandStructure)
		{
			if (commandStructure.CommandName.Equals("help", StringComparison.InvariantCultureIgnoreCase) == false)
				return null;		   

			return new HelpCommand();
		}
	}
}