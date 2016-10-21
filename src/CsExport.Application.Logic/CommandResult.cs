using CsExport.Application.Logic.IO;

namespace CsExport.Application.Logic
{
	public abstract class CommandResult
	{
		public abstract void Handle(IOutput output);
	}
}