using CsExport.Core;

namespace CsExport.Application.Logic.CommandArguments
{
	public class RawExportCommandArguments : IArguments
	{
		public Date From { get; set; }
		public Date To { get; set; }

		public string[] Events { get; set; }   
	}
}