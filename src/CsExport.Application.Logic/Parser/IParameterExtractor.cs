namespace CsExport.Application.Logic.Parser
{
	public interface IParameterExtractor
	{
		string ExtractValue(string input, string parameterName);
	}
}