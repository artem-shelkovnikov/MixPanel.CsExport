using System.Reflection;

namespace CsExport.Application.Logic.Parser
{
	public class ParameterDefinition
	{
		public string Signature { get; internal set; }

		public string Description { get; internal set; }

		public PropertyInfo PropertyInfo { get; internal set; }
	}
}