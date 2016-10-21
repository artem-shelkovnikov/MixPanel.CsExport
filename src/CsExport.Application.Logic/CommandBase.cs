using CsExport.Core.Settings;

namespace CsExport.Application.Logic
{
	public abstract class CommandBase<TArguments>
		: ICommand
		where TArguments : IArguments
	{
		private readonly TArguments _arguments;

		protected CommandBase(TArguments arguments)
		{
			_arguments = arguments;
		}	  

		protected abstract CommandResult ExecuteInner(ApplicationConfiguration applicationConfiguration, ClientConfiguration clientConfiguration, TArguments arguments);
		public CommandResult Execute(ApplicationConfiguration applicationConfiguration, ClientConfiguration clientConfiguration)
		{
			return ExecuteInner(applicationConfiguration, clientConfiguration, _arguments);
		}
	}
}