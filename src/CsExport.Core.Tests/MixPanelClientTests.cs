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
		private readonly Mock<IClientConfiguration> _clientConfigurationMock;
		private readonly Mock<ISigCalculator> _sigCalculatorMock;

		private const string TestUriString = "http://google.com";
		private const string TestApiKeyUriParamName = "test_api_key";
		private const string TestSigUriParamName = "test_sig";
		private const string TestFromDateUriParamName = "test_starts_at";
		private const string TestToDateUriParamName = "test_ends_at";

		private const string TestClientApiKey = "client_api_key_111222333444555666";  

		private const string TestSig = "sig123";
		private const string TestSecret = "secret_123654987456";
 
		private readonly Uri _testUri = new Uri(TestUriString);

		public MixPanelClientTests()
		{
			_webClientMock = new Mock<IWebClient>();
			_uriConfigurationMock = new Mock<IMixPanelEndpointConfiguration>();
			_clientConfigurationMock = new Mock<IClientConfiguration>();
			_sigCalculatorMock = new Mock<ISigCalculator>();
			_mixPanelClient = new MixPanelClient(_webClientMock.Object, _uriConfigurationMock.Object, _clientConfigurationMock.Object, _sigCalculatorMock.Object);

			_uriConfigurationMock.SetupGet(x => x.RawExportUri).Returns(_testUri);
			_uriConfigurationMock.SetupGet(x => x.ApiKeyParamName).Returns(TestApiKeyUriParamName);
			_uriConfigurationMock.SetupGet(x => x.SigParamName).Returns(TestSigUriParamName);
			_uriConfigurationMock.SetupGet(x => x.FromDateParamName).Returns(TestFromDateUriParamName);
			_uriConfigurationMock.SetupGet(x => x.ToDateParamName).Returns(TestToDateUriParamName);

			_clientConfigurationMock.SetupGet(x => x.ApiKey).Returns(TestClientApiKey);
			_clientConfigurationMock.SetupGet(x => x.Secret).Returns(TestSecret);

			_sigCalculatorMock.Setup(x => x.Calculate(It.IsAny<IDictionary<string, string>>(), It.IsAny<string>())).Returns(TestSig);
		}

		[Fact]
		public void ExportRaw_When_called_with_valid_arguments_Then_uses_webClient_to_get_data()
		{
			_mixPanelClient.ExportRaw(GetDefaultStartDate(), GetDefaultEndDate());

			_webClientMock.Verify(x => x.QueryUri(_testUri, It.IsAny<IDictionary<string, string>>()), Times.Once);
		}

		[Fact]
		public void ExportRaw_When_called_with_valid_arguments_Then_returns_response_from_webClient()
		{
			var webResponse = "test response";

			_webClientMock.Setup(x => x.QueryUri(_testUri, It.IsAny<IDictionary<string, string>>())).Returns(webResponse);

			var results = _mixPanelClient.ExportRaw(GetDefaultStartDate(), GetDefaultEndDate());

			Assert.Equal(webResponse, results);
		}

		[Fact]
		public void ExportRaw_When_webClient_throws_exception_Then_throws_mixPanelClientException()
		{
			_webClientMock.Setup(x => x.QueryUri(_testUri, It.IsAny<IDictionary<string, string>>())).Throws<DummyWebClientException>();

			Assert.Throws<MixPanelClientException>(() => _mixPanelClient.ExportRaw(GetDefaultStartDate(), GetDefaultEndDate()));
		}

		[Fact]
		public void ExportRaw_When_webClient_throws_exception_Then_throws_mixPanelClientException_with_correct_innerException()
		{
			_webClientMock.Setup(x => x.QueryUri(_testUri, It.IsAny<IDictionary<string, string>>())).Throws<DummyWebClientException>();

			try
			{
				_mixPanelClient.ExportRaw(GetDefaultStartDate(), GetDefaultEndDate());
			}
			catch (Exception ex)
			{
				Assert.IsType<DummyWebClientException>(ex.InnerException);
			}
		}

		[Fact]
		public void ExportRaw_When_called_Then_uses_uri_from_uriConfiguration()
		{
			var result = _mixPanelClient.ExportRaw(GetDefaultStartDate(), GetDefaultEndDate());

			_uriConfigurationMock.VerifyGet(x => x.RawExportUri, Times.Once);

		}

		[Fact]
		public void ExportRaw_When_called_Then_passes_uri_from_uriConfiguration_to_webClient()
		{
			var rawExportUri = _testUri;

			var result = _mixPanelClient.ExportRaw(GetDefaultStartDate(), GetDefaultEndDate());

			_webClientMock.Verify(x => x.QueryUri(rawExportUri, It.IsAny<IDictionary<string, string>>()), Times.Once);
		}

		[Fact]
		public void ExportRaw_When_called_Then_takes_apiKey_from_clientConfiguration()
		{
			var result = _mixPanelClient.ExportRaw(GetDefaultStartDate(), GetDefaultEndDate());

			_clientConfigurationMock.VerifyGet(x => x.ApiKey, Times.Once);
		}

		[Fact]
		public void ExportRaw_When_called_Then_passes_client_apiKey_to_webClient_as_parameter()
		{
			var result = _mixPanelClient.ExportRaw(GetDefaultStartDate(), GetDefaultEndDate());

			_webClientMock.Verify(x => x.QueryUri(_testUri, It.Is<IDictionary<string, string>>(y => y.ContainsKey(TestApiKeyUriParamName) && y[TestApiKeyUriParamName] == TestClientApiKey)), Times.Once);
		}

		[Fact]
		public void ExportRaw_When_called_and_apiKey_is_null_Then_throws_exception()
		{
			_clientConfigurationMock.SetupGet(x => x.ApiKey).Returns((string)null);
			Assert.Throws<MixPanelClientException>(() => _mixPanelClient.ExportRaw(GetDefaultStartDate(), GetDefaultEndDate()));
		}

		[Fact]
		public void ExportRaw_When_called_with_valid_parameters_Then_calculates_sig_using_sigCalculator_and_apiKey()
		{
			var result = _mixPanelClient.ExportRaw(GetDefaultStartDate(), GetDefaultEndDate());
			
			_sigCalculatorMock.Verify(x=>x.Calculate(It.Is<IDictionary<string, string>>(y=>y[TestApiKeyUriParamName] == TestClientApiKey), It.IsAny<string>()), Times.Once);
		}

		[Fact]
		public void ExportRaw_When_called_With_valid_parameters_Then_adds_sig_to_payload_passed_to_webClient()
		{
			var result = _mixPanelClient.ExportRaw(GetDefaultStartDate(), GetDefaultEndDate());

			_webClientMock.Verify(x => x.QueryUri(_testUri, It.Is<IDictionary<string, string>>(y => y.ContainsKey(TestSigUriParamName) && y[TestSigUriParamName] == TestSig)), Times.Once);
		}

		[Fact]
		public void ExportRaw_When_called_with_valid_parameters_Then_passes_secret_to_sigCalculator()
		{
			var result = _mixPanelClient.ExportRaw(GetDefaultStartDate(), GetDefaultEndDate());

			_sigCalculatorMock.Verify(x=>x.Calculate(It.IsAny<IDictionary<string, string>>(), TestSecret), Times.Once);
		}

		[Fact]
		public void ExportRaw_When_called_with_required_parameters_Then_uses_them_in_sigCalculator_for_calculating_sig()
		{
			var from = new Date();
			var to = new Date(DateTime.UtcNow);

			var result = _mixPanelClient.ExportRaw(from, to);

			_sigCalculatorMock.Verify(
				x =>
					x.Calculate(
						It.Is<IDictionary<string, string>>(y => y.ContainsKey(TestFromDateUriParamName) && y[TestFromDateUriParamName] == from.ToString() &&
						                                        y.ContainsKey(TestToDateUriParamName) && y[TestToDateUriParamName] == to.ToString()),
						It.IsAny<string>()), Times.Once);
		}

		[Fact]
		public void ExportRaw_When_called_with_required_parameters_Then_passes_them_to_webClient_queryUri_method()
		{
			var from = new Date();
			var to = new Date(DateTime.UtcNow);

			var result = _mixPanelClient.ExportRaw(from, to);

			_webClientMock.Verify(
				x =>
					x.QueryUri(_testUri,
						It.Is<IDictionary<string, string>>(y => y.ContainsKey(TestFromDateUriParamName) && y[TestFromDateUriParamName] == from.ToString() &&
						                                        y.ContainsKey(TestToDateUriParamName) && y[TestToDateUriParamName] == to.ToString())), Times.Once);
		}

		private Date GetDefaultStartDate()
		{
			return new Date(2010, 1, 5);
		}

		private Date GetDefaultEndDate()
		{
			return new Date(2011, 2, 10);
		}

		private class DummyWebClientException : Exception
		{

		}
	}
}