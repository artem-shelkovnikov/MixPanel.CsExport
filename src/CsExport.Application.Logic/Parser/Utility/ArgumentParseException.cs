using System;

namespace CsExport.Application.Logic.Parser.Utility
{
	public class ArgumentParseException : Exception
	{
		public ArgumentParseException(string argumentDefinition) : base(
			$"Unable to parse command argument : {argumentDefinition}")
		{
			
		}
	}
}