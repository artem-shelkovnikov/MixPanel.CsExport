using System;

namespace CsExport.Application.Logic
{
	public class ConsoleOutput : IOutput
	{
		public void Notify(string message)
		{
			Console.WriteLine(message);
			Console.WriteLine();
		}
	}
}