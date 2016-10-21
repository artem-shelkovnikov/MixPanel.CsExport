using CsExport.Application.Logic.Parser.Utility;
using CsExport.Application.Logic.Parser.Utility.ValueBinders;
using CsExport.Core;
using Xunit;

namespace CsExport.Application.Logic.Tests.ParserTests.UtilityTests.BinderTests
{
	public class DateValueBinderTests
	{
		[Fact]
		public void BindValue_When_value_is_passed_and_original_value_is_null_Then_sets_property_value()
		{
			var stubClass = new StubClass();
			var stubClassType = typeof(StubClass);
			var datePropertyInfo = stubClassType.GetProperty("Property");
			var binder = new DateValueBinder(stubClass, datePropertyInfo);

			var someValue = "2015-10-09";
			binder.BindValue(someValue);

			Assert.Equal(new Date(2015, 10, 9), stubClass.Property);
		}

		[Fact]
		public void BindValue_When_null_is_passed_and_original_value_is_null_Then_leaves_property_value_as_null()
		{
			var stubClass = new StubClass();
			var stubClassType = typeof(StubClass);
			var stringPropertyInfo = stubClassType.GetProperty("Property");
			var binder = new DateValueBinder(stubClass, stringPropertyInfo);
							   
			binder.BindValue(null);

			Assert.Null(stubClass.Property);
		}

		[Fact]
		public void BindValue_When_value_is_passed_and_original_value_is_not_empty_Then_updates_property_value()
		{
			var stubClass = new StubClass {Property = new Date(2015, 1, 3)};
			var stubClassType = typeof(StubClass);
			var datePropertyInfo = stubClassType.GetProperty("Property");
			var binder = new DateValueBinder(stubClass, datePropertyInfo);

			var updatedValue = "2015-10-09";
			binder.BindValue(updatedValue);

			Assert.Equal(new Date(2015, 10, 9), stubClass.Property);
		}

		[Fact]
		public void BindValue_When_invalid_string_is_passed_Then_throws_exception()
		{
			var stubClass = new StubClass {Property = new Date(2015, 1, 3)};
			var stubClassType = typeof(StubClass);
			var datePropertyInfo = stubClassType.GetProperty("Property");
			var binder = new DateValueBinder(stubClass, datePropertyInfo);

			var updatedValue = "asdsdf";
			
			Assert.Throws<ParameterBindingException>(()=> binder.BindValue(updatedValue));
		}

		private class StubClass
		{
			public Date Property { get; set; }
		}
	}
}