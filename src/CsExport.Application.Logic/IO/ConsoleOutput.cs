using System;

namespace CsExport.Application.Logic.IO
{
	public class ConsoleOutput : IOutput
	{
		public void Notify(string message)
		{
			Console.WriteLine(message);
		}
	}
}