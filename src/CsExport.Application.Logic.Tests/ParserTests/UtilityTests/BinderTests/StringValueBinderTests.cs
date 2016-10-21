using CsExport.Application.Logic.Parser.Utility.ValueBinders;
using Xunit;

namespace CsExport.Application.Logic.Tests.ParserTests.UtilityTests.BinderTests
{
	public class StringValueBinderTests
	{
		[Fact]
		public void BindValue_When_value_is_passed_and_original_value_is_null_Then_sets_property_value()
		{
			var stubClass = new StubClass();
			var stubClassType = typeof(StubClass);
			var stringPropertyInfo = stubClassType.GetProperty("Property");
			var binder = new StringValueBinder(stubClass, stringPropertyInfo);

			var someValue = "Some Value";
			binder.BindValue(someValue);

			Assert.Equal(someValue, stubClass.Property);
		}

		[Fact]
		public void BindValue_When_null_is_passed_and_original_value_is_null_Then_leaves_property_value_as_null()
		{
			var stubClass = new StubClass();
			var stubClassType = typeof(StubClass);
			var stringPropertyInfo = stubClassType.GetProperty("Property");
			var binder = new StringValueBinder(stubClass, stringPropertyInfo);
							   
			binder.BindValue(null);

			Assert.Null(stubClass.Property);
		}

		[Fact]
		public void BindValue_When_value_is_passed_and_original_value_is_not_empty_Then_updates_property_value()
		{
			var stubClass = new StubClass {Property = "test"};
			var stubClassType = typeof(StubClass);
			var stringPropertyInfo = stubClassType.GetProperty("Property");
			var binder = new StringValueBinder(stubClass, stringPropertyInfo);

			var updatedValue = "Some Value";
			binder.BindValue(updatedValue);

			Assert.Equal(updatedValue, stubClass.Property);
		}

		private class StubClass
		{
			public string Property { get; set; }
		}
	}
}