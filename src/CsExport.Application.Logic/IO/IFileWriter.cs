namespace CsExport.Application.Logic
{
	public interface IFileWriter
	{
		void WriteContent(string path, string fileName, string content);
	}
}