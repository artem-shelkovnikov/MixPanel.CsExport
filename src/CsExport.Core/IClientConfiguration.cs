namespace CsExport.Core
{
	public interface IClientConfiguration
	{
		string ApiKey { get; }
		string Secret { get; }
	}
}