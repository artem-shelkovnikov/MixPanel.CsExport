using System;

namespace CsExport.Application.Infrastructure.IO
{
	internal class ConsoleOutput : IOutput
	{
		public void Notify(string message)
		{
			Console.WriteLine(message);
		}
	}
}