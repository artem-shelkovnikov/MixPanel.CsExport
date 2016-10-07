using System;

namespace CsExport.Application.Logic.Parser.Utility
{
	public class ArgumentParseException : Exception
	{
		public ArgumentParseException() : base("Unable to parse some of command arguments")
		{
			
		}

		public ArgumentParseException(string argumentDefinition) : base(
			$"Unable to parse command argument : {argumentDefinition}")
		{
			
		}
	}
}