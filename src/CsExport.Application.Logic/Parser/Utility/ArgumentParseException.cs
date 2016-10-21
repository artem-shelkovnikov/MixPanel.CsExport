using System;

namespace CsExport.Application.Logic.Parser.Utility
{
	public class ArgumentParseException : Exception
	{
		public ArgumentParseException() : base("Failed to parse input")
		{
			
		}

		public ArgumentParseException(string input) : base(string.Format("Failed to parse input: {0}", input))
		{
			
		}
	}
}