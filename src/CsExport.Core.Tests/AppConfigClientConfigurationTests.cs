using System;
using System.Configuration;
using Xunit;

namespace CsExport.Core.Tests
{
	public class AppConfigClientConfigurationTests
	{
		IClientConfiguration _clientConfiguration = new AppConfigClientConfiguration();

		[Fact]
		public void Get_ApiKey_when_called_and_value_is_set_in_appSettings_Then_gets_apiKey_from_appSettings()
		{
			var appSettingKey = "ApiKey";

			ConfigurationManager.AppSettings[appSettingKey] = "test_apiKey";

			var apiKey = _clientConfiguration.ApiKey;
			
			var appSettingValue = ConfigurationManager.AppSettings[appSettingKey];

			Assert.Equal(apiKey, appSettingValue);
		}

		[Fact]
		public void Get_ApiKey_when_called_and_value_is_not_set_in_appSettings_Then_throws_argumentNullException()
		{
			var appSettingKey = "ApiKey";

			ConfigurationManager.AppSettings[appSettingKey] = null;

			Assert.Throws<ArgumentNullException>(() => _clientConfiguration.ApiKey);
		}

		[Fact]
		public void Get_Secret_when_called_Then_gets_secret_from_appSettings()
		{
			var appSettingKey = "Secret";

			ConfigurationManager.AppSettings[appSettingKey] = "test_secret";
			
			var secret = _clientConfiguration.Secret;

			var appSettingValue = ConfigurationManager.AppSettings[appSettingKey];

			Assert.Equal(secret, appSettingValue);
		}

		[Fact]
		public void Get_Secret_when_called_and_value_is_not_set_in_appSettings_Then_throws_argumentNullException()
		{
			var appSettingKey = "Secret";

			ConfigurationManager.AppSettings[appSettingKey] = null;

			Assert.Throws<ArgumentNullException>(() => _clientConfiguration.Secret);
		}
	}
}