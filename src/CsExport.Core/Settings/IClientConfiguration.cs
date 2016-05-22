namespace CsExport.Core.Settings
{
	public interface IClientConfiguration
	{
		string ApiKey { get; }
		string Secret { get; }
	}
}