namespace CsExport.Application.Logic
{
	public abstract class CommandResult
	{
		public abstract void Handle(IOutput output);
	}
}