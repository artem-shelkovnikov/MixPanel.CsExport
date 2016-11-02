using System.IO;

namespace CsExport.Application.Infrastructure.IO
{
	public class FileWriter : IFileWriter
	{
		public void WriteContent(string path, string fileName, string content)
		{
			File.WriteAllText(path + "\\" + fileName, content);
		}
	}
}