using CsExport.Application.Infrastructure.IO;

namespace CsExport.Application.Infrastructure
{
	public class ResultHandler : IResultHandler
	{
		private readonly IOutput _output;

		public ResultHandler(IOutput output)
		{
			_output = output;
		}

		public void HandleResult(CommandResult commandResult)
		{
			commandResult.Handle(_output);
		}
	}
}