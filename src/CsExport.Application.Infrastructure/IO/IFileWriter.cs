namespace CsExport.Application.Infrastructure.IO
{
	public interface IFileWriter
	{
		void WriteContent(string path, string fileName, string content);
	}
}