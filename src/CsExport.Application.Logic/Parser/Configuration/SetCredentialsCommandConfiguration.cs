using System;
using System.Linq;
using CsExport.Application.Logic.Commands;

namespace CsExport.Application.Logic.Parser.Configuration
{
	public class SetCredentialsCommandConfiguration	: CommandParserConfigurationBase
	{  
		protected override ICommand ParseInner(CommandStructure commandStructure)
		{
			if (commandStructure.CommandName.Equals("set-credentials", StringComparison.InvariantCultureIgnoreCase) == false)
				return null;	   

			var secretArgument = commandStructure.Arguments.FirstOrDefault(x => x.ArgumentName.Equals("secret", StringComparison.InvariantCultureIgnoreCase));
			if (secretArgument == null)
				return null;
			var secret = secretArgument.Value;

			return new SetCredentialsCommand(secret);
		}
	}
}