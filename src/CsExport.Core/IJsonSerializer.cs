namespace CsExport.Core
{
	public interface IJsonSerializer
	{
		T Deserialize<T>(string webClientResponse);
	}
}