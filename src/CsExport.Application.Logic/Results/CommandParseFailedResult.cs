using CsExport.Application.Logic.IO;
using CsExport.Application.Logic.Parser.Utility;

namespace CsExport.Application.Logic.Results
{
	public class CommandParseFailedResult : CommandResult
	{
		private readonly ArgumentParseException _ex;

		public CommandParseFailedResult(ArgumentParseException ex)
		{
			_ex = ex;
		}
		public override void Handle(IOutput output)
		{
			output.Notify(_ex.Message);
		}
	}
}