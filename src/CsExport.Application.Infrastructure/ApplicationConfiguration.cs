using CsExport.Application.Infrastructure.Parser;

namespace CsExport.Application.Infrastructure
{
	public class ApplicationConfiguration
	{
		public ValueBinderProviderCollection ValueBinderProviderCollection { get; } = new ValueBinderProviderCollection();
	}
}