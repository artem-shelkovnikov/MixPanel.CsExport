using System.Reflection;
using System.Text;

namespace CsExport.Application.Infrastructure.Parser
{
	public static class SignatureHelper
	{
		public static string GetDefaultPropertySignature(PropertyInfo propertyInfo)
		{
			var propertyName = propertyInfo.Name;

			var parameterName = new StringBuilder();
			for (int index = 0; index < propertyName.Length; index++)
			{
				var character = propertyName[index];
				if (index == 0
				    || char.IsUpper(character) == false)
					parameterName.Append(char.ToLowerInvariant(character));
				else
					parameterName.AppendFormat("-{0}", char.ToLowerInvariant(character));
			}

			return parameterName.ToString();
		}
	}
}