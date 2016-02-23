using System;
using System.Configuration;

namespace CsExport.Core
{
	public class AppConfigClientConfiguration : IClientConfiguration
	{
		public string ApiKey { get { return GetValue("ApiKey"); } }
		public string Secret { get { return GetValue("Secret"); } }

		private string GetValue(string settingName)
		{
			var value = ConfigurationManager.AppSettings[settingName];

			if (string.IsNullOrWhiteSpace(value))
				throw new ArgumentNullException(value, "Setting has no value in AppSettings");

			return value;
		}
	}
}