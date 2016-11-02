namespace CsExport.Application.Infrastructure.Parser
{
	public interface IParameterExtractor
	{
		string ExtractValue(string input, string parameterName);
	}
}