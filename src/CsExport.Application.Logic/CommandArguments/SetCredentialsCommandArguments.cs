using CsExport.Application.Infrastructure;

namespace CsExport.Application.Logic.CommandArguments
{
	public class SetCredentialsCommandArguments : IArguments
	{
		public string Secret { get; set; }
	}
}