namespace CsExport.Application.Logic.IO
{
	public interface IFileWriter
	{
		void WriteContent(string path, string fileName, string content);
	}
}