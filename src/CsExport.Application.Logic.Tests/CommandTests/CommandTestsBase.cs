﻿using CsExport.Core.Client;
using CsExport.Core.Settings;
using Moq;

namespace CsExport.Application.Logic.Tests.CommandTests
{
	public abstract class CommandTestsBase
	{
		private Mock<IMixPanelClient> _mixPanelClientMock = new Mock<IMixPanelClient>();
		private Mock<IInputProvider> _inputProviderMock = new Mock<IInputProvider>();			
		private ClientConfiguration _clientConfiguration = new ClientConfiguration();		  

		protected Mock<IMixPanelClient> MixPanelClientMock { get { return _mixPanelClientMock;} }
		protected Mock<IInputProvider> InputProviderMock { get { return _inputProviderMock; } }
		protected ClientConfiguration ClientConfiguration { get { return _clientConfiguration; } }
		
		protected ExecutionSettings GetExecutionSettings()
		{
			return new ExecutionSettings
			{
				MixPanelClient = _mixPanelClientMock.Object,
				InputProvider = _inputProviderMock.Object,
				ClientConfiguration = _clientConfiguration
			};
		}
	}
}