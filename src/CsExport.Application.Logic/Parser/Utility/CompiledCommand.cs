using CsExport.Core.Settings;

namespace CsExport.Application.Logic.Parser.Utility
{
	public class CompiledCommand<TCommand, TCommandArguments> : ICommand
		where TCommand : ICommandWithArguments<TCommandArguments>
		where TCommandArguments : IArguments
	{
		private readonly TCommand _command;
		private readonly TCommandArguments _commandArguments;

		public CompiledCommand(TCommand command, TCommandArguments commandArguments)
		{
			_command = command;
			_commandArguments = commandArguments;
		}

		public CommandResult Execute()
		{
			return _command.Execute(_commandArguments);
		}
	}
}