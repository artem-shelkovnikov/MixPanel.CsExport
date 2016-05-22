namespace CsExport.Core.Client
{
	public interface IJsonSerializer
	{
		T Deserialize<T>(string webClientResponse);
	}
}