namespace CsExport.Application.Infrastructure
{
	public interface ICommandWithArguments<in TArguments> where TArguments : IArguments
	{
		CommandResult Execute(TArguments commandArguments);
	}
}