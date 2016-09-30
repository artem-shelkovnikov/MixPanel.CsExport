using System;

namespace CsExport.Application.Logic.Results
{
	public class CommandTerminatedResult : CommandResult
	{
		private readonly Exception _ex;

		public CommandTerminatedResult(Exception ex)
		{
			_ex = ex;
		}

		public override void Handle(IOutput output)
		{
			output.Notify(String.Format("Execution stopped with an exception <{0}>, Message: {1}", _ex.GetType().FullName, _ex.Message));
		}
	}
}