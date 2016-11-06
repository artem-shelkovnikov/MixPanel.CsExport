using CsExport.Application.Infrastructure;
using CsExport.Application.Infrastructure.Builtin.CommandArguments;
using CsExport.Application.Infrastructure.Builtin.Commands;
using CsExport.Application.Infrastructure.DependancyControl;
using CsExport.Application.Logic;
using CsExport.Application.Logic.CommandArguments;
using CsExport.Application.Logic.Commands;
using CsExport.Core.Client;
using CsExport.Core.Settings;

namespace CsExport.Application.Console.Infrastructure
{
	public class ApplicationDependancyRegistration : DependancyConfiguration
	{
		protected override void Register(IDependancyContainer dependancyContainer)
		{
			dependancyContainer.Register<ICommandWithArguments<HelpCommandArguments>, HelpCommand>();
			dependancyContainer.Register<ICommandWithArguments<RawExportCommandArguments>, RawExportCommand>();
			dependancyContainer.Register<ICommandWithArguments<SetCredentialsCommandArguments>, SetCredentialsCommand>();

			dependancyContainer.Register<IMixPanelClient, MixPanelClient>();
			dependancyContainer.Register<IFileWriter, FileWriter>();
			dependancyContainer.Register<IWebClient, DefaultWebClient>();
			dependancyContainer.RegisterInstance(new ClientConfiguration());
		}
	}
}