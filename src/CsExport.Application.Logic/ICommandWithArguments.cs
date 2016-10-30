namespace CsExport.Application.Logic
{
	public interface ICommandWithArguments<in TArguments> where TArguments : IArguments
	{
		CommandResult Execute(TArguments commandArguments);
	}
}