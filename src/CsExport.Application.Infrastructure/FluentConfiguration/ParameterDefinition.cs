using System.Reflection;
using CsExport.Application.Infrastructure.Utility;

namespace CsExport.Application.Infrastructure.FluentConfiguration
{
	public class ParameterDefinition
	{
		private string _signature;
		private string _description;
		private readonly PropertyInfo _propertyInfo;

		public ParameterDefinition(PropertyInfo propertyInfo)
		{
			_propertyInfo = propertyInfo;
		}

		public string Signature => _signature;
		public string Description => _description;
		public PropertyInfo PropertyInfo => _propertyInfo;

		internal void SetSignature(string signature)
		{
			SignatureValidation.Validate(signature);
			_signature = signature;
		}

		internal void SetDescription(string description)
		{
			_description = description;
		}
	}
}