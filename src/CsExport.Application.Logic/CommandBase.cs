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

		protected abstract CommandResult ExecuteInner(TArguments arguments);

		public CommandResult Execute()
		{
			return ExecuteInner(_arguments);
		}
	}
}