using System;
using CsExport.Application.Infrastructure.IO;

namespace CsExport.Application.Infrastructure.Results
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