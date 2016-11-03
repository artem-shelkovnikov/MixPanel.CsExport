namespace CsExport.Application.Logic
{
	public class ApplicationConfiguration
	{
		public string ExportPath { get; set; }

		public ApplicationConfiguration()
		{
			ExportPath = ".";
		}
	}
}