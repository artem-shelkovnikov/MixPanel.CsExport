using System;

namespace CsExport.Application.Infrastructure.IO
{
	public class ConsoleInput : IInput
	{
		public string GetLine()
		{
			Console.Write(">");
			return Console.ReadLine();
		}
	}
}