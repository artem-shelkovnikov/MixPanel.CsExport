using CsExport.Application.Logic.IO;
using CsExport.Core.Client;
using CsExport.Core.Settings;
using Moq;

namespace CsExport.Application.Logic.Tests.CommandTests
{
	public abstract class CommandTestsBase
	{
		protected Mock<IMixPanelClient> MixPanelClientMock { get; } = new Mock<IMixPanelClient>();
		protected Mock<IInput> InputProviderMock { get; } = new Mock<IInput>();
		protected Mock<IFileWriter> FileWriterMock { get; } = new Mock<IFileWriter>();
		protected ClientConfiguration ClientConfiguration { get; } = new ClientConfiguration();
		protected ApplicationConfiguration ApplicationConfiguration { get; } = new ApplicationConfiguration();

		protected CommandTestsBase()
		{
			ClientConfiguration.UpdateCredentials("valid-secret");
		}	
	}
}