using CsExport.Application.Infrastructure.IO;

namespace CsExport.Application.Infrastructure
{
	public abstract class CommandResult
	{
		public abstract void Handle(IOutput output);
	}
}