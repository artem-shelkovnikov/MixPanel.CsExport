using System;

namespace CsExport.Application.Logic
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