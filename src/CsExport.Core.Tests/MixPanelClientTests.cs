using System;
using System.Collections.Generic;
using CsExport.Core.Exceptions;
using Moq;
using Xunit;

namespace CsExport.Core.Tests
{
	public class MixPanelClientTests
	{
		private readonly IMixPanelClient _mixPanelClient;
		private readonly Mock<IWebClient> _webClientMock;
		private readonly Mock<IMixPanelEndpointConfiguration> _uriConfigurationMock;

		private const string TestUriString = "http://google.com";

		private Uri _testUri = new Uri(TestUriString);

		public MixPanelClientTests()
		{
			_webClientMock = new Mock<IWebClient>();
			_uriConfigurationMock = new Mock<IMixPanelEndpointConfiguration>();
			_mixPanelClient = new MixPanelClient(_webClientMock.Object, _uriConfigurationMock.Object);

			_uriConfigurationMock.Setup(x => x.RawExportUri).Returns(_testUri);
		}  

		[Fact]
		public void ExportRaw_When_called_with_valid_arguments_Then_uses_webClient_to_get_data()
		{
			_mixPanelClient.ExportRaw();

			_webClientMock.Verify(x=>x.QueryUri(_testUri), Times.Once);
		}

		[Fact]
		public void ExportRaw_When_called_with_valid_arguments_Then_returns_response_from_webClient()
		{
			var webResponse = "test response";

			_webClientMock.Setup(x => x.QueryUri(_testUri)).Returns(webResponse);

			var results = _mixPanelClient.ExportRaw();
			
			Assert.Equal(webResponse, results);					
		} 

		[Fact]
		public void ExportRaw_When_webClient_throws_exception_Then_throws_mixPanelClientException()
		{
			_webClientMock.Setup(x => x.QueryUri(_testUri)).Throws<DummyWebClientException>();

			Assert.Throws<MixPanelClientException>(() => _mixPanelClient.ExportRaw());
		}

		[Fact]
		public void ExportRaw_When_webClient_throws_exception_Then_throws_mixPanelClientException_with_correct_innerException()
		{
			_webClientMock.Setup(x => x.QueryUri(_testUri)).Throws<DummyWebClientException>();

			try
			{
				_mixPanelClient.ExportRaw();
			}
			catch (Exception ex)
			{
				Assert.IsType<DummyWebClientException>(ex.InnerException);
			}
		} 

		[Fact]
		public void ExportRaw_When_called_Then_uses_uri_from_uriConfiguration()
		{
			var result = _mixPanelClient.ExportRaw();

			_uriConfigurationMock.VerifyGet(x => x.RawExportUri, Times.Once);

		}

		[Fact]
		public void ExportRaw_When_called_Then_passes_uri_from_uriConfiguration_to_webClient()
		{
			var rawExportUri = _testUri;

			var result = _mixPanelClient.ExportRaw();

			_webClientMock.Verify(x=>x.QueryUri(rawExportUri), Times.Once);

		}

		private class DummyWebClientException : Exception
		{
			
		}
	}
}