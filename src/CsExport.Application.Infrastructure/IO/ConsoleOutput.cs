using System;

namespace CsExport.Application.Infrastructure.IO
{
	public class ConsoleOutput : IOutput
	{
		public void Notify(string message)
		{
			Console.WriteLine(message);
		}
	}
}