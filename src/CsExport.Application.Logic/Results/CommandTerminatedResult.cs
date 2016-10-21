using System;
using CsExport.Application.Logic.IO;

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
			output.Notify($"Execution stopped with an exception <{_ex.GetType().FullName}>, Message: {_ex.Message}");
		}
	}
}