using CsExport.Core.Client;
using CsExport.Core.Settings;
using Moq;

namespace CsExport.Application.Logic.Tests.CommandTests
{
	public abstract class CommandTestsBase
	{
		private Mock<IMixPanelClient> _mixPanelClientMock = new Mock<IMixPanelClient>();
		private Mock<IInput> _inputProviderMock = new Mock<IInput>();			
		private Mock<IFileWriter> _fileWriterMock = new Mock<IFileWriter>();			
		private ClientConfiguration _clientConfiguration = new ClientConfiguration();		  
		private ApplicationConfiguration _applicationConfiguration = new ApplicationConfiguration();		  

		protected Mock<IMixPanelClient> MixPanelClientMock { get { return _mixPanelClientMock;} }
		protected Mock<IInput> InputProviderMock { get { return _inputProviderMock; } }
		protected Mock<IFileWriter> FileWriterMock { get { return _fileWriterMock; } }
		protected ClientConfiguration ClientConfiguration { get { return _clientConfiguration; } }
		protected ApplicationConfiguration ApplicationConfiguration { get { return _applicationConfiguration; } }

		protected CommandTestsBase()
		{
			_clientConfiguration.UpdateCredentials("valid-secret");
		}	
	}
}