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
		private readonly Mock<IJsonSerializer> _jsonSerializerMock;
		private readonly Mock<IMixPanelEndpointConfiguration> _uriConfigurationMock;

		private const string TestUriString = "http://google.com";

		private Uri _testUri = new Uri(TestUriString);

		public MixPanelClientTests()
		{
			_webClientMock = new Mock<IWebClient>();
			_jsonSerializerMock = new Mock<IJsonSerializer>();
			_uriConfigurationMock = new Mock<IMixPanelEndpointConfiguration>();
			_mixPanelClient = new MixPanelClient(_webClientMock.Object, _jsonSerializerMock.Object, _uriConfigurationMock.Object);

			_uriConfigurationMock.Setup(x => x.RawExportUri).Returns(_testUri);
		}  

		[Fact]
		public void ExportRaw_When_called_with_valid_arguments_Then_uses_webClient_to_get_data()
		{
			_mixPanelClient.ExportRaw();

			_webClientMock.Verify(x=>x.QueryUri(_testUri), Times.Once);
		}

		[Fact]
		public void ExportRaw_When_called_with_valid_arguments_Then_returns_not_null_result()
		{
			var results = _mixPanelClient.ExportRaw();
			
			Assert.NotNull(results);					
		}

		[Fact]
		public void ExportRaw_When_called_with_valid_arguments_Then_uses_jsonDeserializer_for_data_received_from_webClient()
		{
			var webClientResponse = "web_client_response";
			_webClientMock.Setup(x => x.QueryUri(_testUri)).Returns(webClientResponse);

			var results = _mixPanelClient.ExportRaw();

			_jsonSerializerMock.Verify(x=>x.Deserialize<IEnumerable<ExportResult>>(webClientResponse), Times.Once);
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
		public void ExportRaw_When_jsonSerializer_throws_exception_Then_throws_mixPanelClientException()
		{
			_webClientMock.Setup(x => x.QueryUri(_testUri)).Throws<DummyJsonSerializerException>();

			Assert.Throws<MixPanelClientException>(() => _mixPanelClient.ExportRaw());
		}

		[Fact]
		public void ExportRaw_When_jsonSerializer_throws_exception_Then_throws_mixPanelClientException_with_correct_innerException()
		{
			_webClientMock.Setup(x => x.QueryUri(_testUri)).Throws<DummyJsonSerializerException>();

			try
			{
				_mixPanelClient.ExportRaw();
			}
			catch (Exception ex)
			{
				Assert.IsType<DummyJsonSerializerException>(ex.InnerException);
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

		private class DummyJsonSerializerException : Exception
		{
			
		}


		private class DummyWebClientException : Exception
		{
			
		}
	}
}