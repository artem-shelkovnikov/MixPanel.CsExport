using CsExport.Application.Logic.CommandArguments;

namespace CsExport.Application.Logic.Parser.Configuration
{
	public class RawExportCommandConfiguration : CommandConfiguration<RawExportCommandArguments>
	{
		public RawExportCommandConfiguration()
		{
			HasSignature("raw-export");

			HasDescription("Exports Raw data for specified date range");

			HasArgument(x => x.From)
				.WithSignature("from")
				.WithDescription("Starting date for raw export period");

			HasArgument(x => x.To)
				.WithSignature("to")
				.WithDescription("Ending date for raw export period");
		}
	}
}