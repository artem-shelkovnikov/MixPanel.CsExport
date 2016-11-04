using System.IO;

namespace CsExport.Application.Logic
{
	public class FileWriter : IFileWriter
	{
		public void WriteContent(string path, string fileName, string content)
		{
			File.WriteAllText(path + "\\" + fileName, content);
		}
	}
}