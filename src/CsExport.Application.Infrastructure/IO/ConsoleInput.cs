using System;

namespace CsExport.Application.Infrastructure.IO
{
	internal class ConsoleInput : IInput
	{
		public string GetLine()
		{
			Console.Write(">");
			return Console.ReadLine();
		}
	}
}