using System;
using System.Collections.Generic;
using CsExport.Core.Client;
using CsExport.Core.Exceptions;
using CsExport.Core.Settings;
using Moq;
using Xunit;

namespace CsExport.Core.Tests
{
	public class MixPanelClientTests
	{
		private readonly IMixPanelClient _mixPanelClient;
		private readonly Mock<IWebClient> _webClientMock;
		private readonly Mock<IMixPanelEndpointConfiguration> _uriConfigurationMock;   
		private readonly Mock<ISigCalculator> _sigCalculatorMock;

		private const string TestUriString = "http://google.com";
		private const string TestApiKeyUriParamName = "test_api_key";
		private const string TestSigUriParamName = "test_sig";
		private const string TestFromDateUriParamName = "test_starts_at";
		private const string TestToDateUriParamName = "test_ends_at";

		private const string TestClientApiKey = "client_api_key_111222333444555666";  

		private const string TestSig = "sig123";
		private const string TestSecret = "secret_123654987456";

		private ClientConfiguration _clientConfiguration = new ClientConfiguration();
 
		private readonly Uri _testUri = new Uri(TestUriString);

		public MixPanelClientTests()
		{
			_webClientMock = new Mock<IWebClient>();
			_uriConfigurationMock = new Mock<IMixPanelEndpointConfiguration>();	   
			_sigCalculatorMock = new Mock<ISigCalculator>();
			_mixPanelClient = new MixPanelClient(_webClientMock.Object, _uriConfigurationMock.Object, _sigCalculatorMock.Object);

			_uriConfigurationMock.SetupGet(x => x.RawExportUri).Returns(_testUri);
			_uriConfigurationMock.SetupGet(x => x.ApiKeyParamName).Returns(TestApiKeyUriParamName);
			_uriConfigurationMock.SetupGet(x => x.SigParamName).Returns(TestSigUriParamName);
			_uriConfigurationMock.SetupGet(x => x.FromDateParamName).Returns(TestFromDateUriParamName);
			_uriConfigurationMock.SetupGet(x => x.ToDateParamName).Returns(TestToDateUriParamName);

			_clientConfiguration.UpdateCredentials(TestClientApiKey, TestSecret);	

			_sigCalculatorMock.Setup(x => x.Calculate(It.IsAny<IDictionary<string, string>>(), It.IsAny<string>())).Returns(TestSig);
		}

		[Fact]
		public void ExportRaw_When_called_with_valid_arguments_Then_uses_webClient_to_get_data()
		{
			_mixPanelClient.ExportRaw(_clientConfiguration, GetDefaultStartDate(), GetDefaultEndDate());

			_webClientMock.Verify(x => x.QueryUri(It.IsAny<Uri>(), It.IsAny<BasicAuthentication>()), Times.Once);
		}

		[Fact]
		public void ExportRaw_When_called_with_valid_arguments_Then_returns_response_from_webClient()
		{
			var webResponse = "test response";

			_webClientMock.Setup(x => x.QueryUri(It.IsAny<Uri>(), It.IsAny<BasicAuthentication>())).Returns(webResponse);

			var results = _mixPanelClient.ExportRaw(_clientConfiguration, GetDefaultStartDate(), GetDefaultEndDate());

			Assert.Equal(webResponse, results);
		}

		[Fact]
		public void ExportRaw_When_webClient_throws_exception_Then_throws_mixPanelClientException()
		{
			_webClientMock.Setup(x => x.QueryUri(It.IsAny<Uri>(), It.IsAny<BasicAuthentication>())).Throws<DummyWebClientException>();

			Assert.Throws<MixPanelClientException>(() => _mixPanelClient.ExportRaw(_clientConfiguration, GetDefaultStartDate(), GetDefaultEndDate()));
		}

		[Fact]
		public void ExportRaw_When_webClient_throws_exception_Then_throws_mixPanelClientException_with_correct_innerException()
		{
			_webClientMock.Setup(x => x.QueryUri(It.IsAny<Uri>(), It.IsAny<BasicAuthentication>())).Throws<DummyWebClientException>();

			try
			{
				_mixPanelClient.ExportRaw(_clientConfiguration, GetDefaultStartDate(), GetDefaultEndDate());
			}
			catch (Exception ex)
			{
				Assert.IsType<DummyWebClientException>(ex.InnerException);
			}
		}

		[Fact]
		public void ExportRaw_When_called_Then_uses_uri_from_uriConfiguration()
		{
			var result = _mixPanelClient.ExportRaw(_clientConfiguration, GetDefaultStartDate(), GetDefaultEndDate());

			_uriConfigurationMock.VerifyGet(x => x.RawExportUri, Times.Once);

		}

		[Fact]
		public void ExportRaw_When_called_Then_passes_fromDate_and_toDate_to_webClient()
		{
			var fromDate = GetDefaultStartDate();
			var toDate = GetDefaultEndDate();

			var expectedUrl = GetExpectedUrl(fromDate, toDate);

			var result = _mixPanelClient.ExportRaw(_clientConfiguration, fromDate, toDate);

			_webClientMock.Verify(x => x.QueryUri(expectedUrl, It.IsAny<BasicAuthentication>()), Times.Once);
		}	

		[Fact]
		public void ExportRaw_When_called_Then_passes_secret_to_basic_authentication_userName_and_leaves_password_empty()
		{
			var from = new Date(DateTime.UtcNow.AddDays(-1));
			var to = new Date(DateTime.UtcNow);

			var result = _mixPanelClient.ExportRaw(_clientConfiguration, from, to);

			_webClientMock.Verify(
				x =>
					x.QueryUri(It.IsAny<Uri>(),
						It.Is<BasicAuthentication>(y => y.UserName == _clientConfiguration.Secret && y.Password == null)));
		} 

		private Uri GetExpectedUrl(Date defaultStartDate, Date defaultEndDate)
		{
			var stringifiedUrl = string.Format("{0}?{1}={2}&{3}={4}", 
												_testUri, 
												TestFromDateUriParamName, defaultStartDate.ToString(), 
												TestToDateUriParamName, defaultEndDate.ToString());

			return new Uri(stringifiedUrl);
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