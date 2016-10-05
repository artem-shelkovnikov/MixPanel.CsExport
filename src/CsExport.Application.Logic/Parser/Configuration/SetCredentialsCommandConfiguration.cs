using System;
using System.Collections.Generic;
using System.Linq;
using CsExport.Application.Logic.Commands;
using CsExport.Application.Logic.Parser.Utility;

namespace CsExport.Application.Logic.Parser.Configuration
{
	public class SetCredentialsCommandConfiguration	: CommandParserConfigurationBase
	{  		   

		protected override ICommand ParseInner(string commandName, IEnumerable<CommandArgument> arguments)
		{
			if (commandName.Equals("set-credentials", StringComparison.InvariantCultureIgnoreCase) == false)
				return null;

			var secretArgument = arguments.FirstOrDefault(x => x.ArgumentName.Equals("secret", StringComparison.InvariantCultureIgnoreCase));
			if (secretArgument == null)
				return null;
			var secret = secretArgument.Value;

			return new SetCredentialsCommand(secret);
		}
	}
}