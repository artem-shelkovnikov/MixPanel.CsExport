using System;
using System.Web;
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

		private const string TestSecret = "secret_123654987456";

		private readonly ClientConfiguration _clientConfiguration = new ClientConfiguration();
 																	

		public MixPanelClientTests()
		{
			_webClientMock = new Mock<IWebClient>();  	   
			_mixPanelClient = new MixPanelClient(_webClientMock.Object); 

			_clientConfiguration.UpdateCredentials(TestSecret);													   
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
		public void ExportRaw_When_called_Then_passes_fromDate_and_toDate_to_webClient()
		{
			var fromDate = GetDefaultStartDate();
			var toDate = GetDefaultEndDate();

			var expectedUrl = GetExpectedUrl(fromDate, toDate);

			var result = _mixPanelClient.ExportRaw(_clientConfiguration, fromDate, toDate);

			_webClientMock.Verify(x => x.QueryUri(expectedUrl, It.IsAny<BasicAuthentication>()), Times.Once);
		}	

		[Fact]
		public void ExportRaw_When_called_with_event_Then_passes_event_as_query_parameter_to_webClient()
		{
			var fromDate = GetDefaultStartDate();
			var toDate = GetDefaultEndDate();
			var @event = "some event";

			var expectedUrl = GetExpectedUrl(fromDate, toDate, @event);

			var result = _mixPanelClient.ExportRaw(_clientConfiguration, fromDate, toDate, new [] { @event });

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
			var stringifiedUrl = string.Format("{0}?from_date={1}&to_date={2}", 
												MixPanelEndpointConfiguration.RawExportUrl, 
												defaultStartDate.ToString(),
												defaultEndDate.ToString());

			return new Uri(stringifiedUrl);
		} 

		private Uri GetExpectedUrl(Date defaultStartDate, Date defaultEndDate, string @event)
		{
			var stringifiedUrl = string.Format("{0}?from_date={1}&to_date={2}&event=[\"{3}\"]",
												MixPanelEndpointConfiguration.RawExportUrl,
												defaultStartDate.ToString(),
												defaultEndDate.ToString(),
												HttpUtility.UrlEncode(@event));

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