using System.IO;
using CsExport.Application.Infrastructure.IO;

namespace CsExport.Application.Logic
{
	internal class FileWriter : IFileWriter
	{
		public void WriteContent(string path, string fileName, string content)
		{
			File.WriteAllText(path + "\\" + fileName, content);
		}
	}
}