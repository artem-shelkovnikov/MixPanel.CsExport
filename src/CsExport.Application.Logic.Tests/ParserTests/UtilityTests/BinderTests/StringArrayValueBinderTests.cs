using CsExport.Application.Logic.Parser.Utility.ValueBinders;
using Xunit;

namespace CsExport.Application.Logic.Tests.ParserTests.UtilityTests.BinderTests
{
	public class StringArrayValueBinderTests
	{
		[Fact]
		public void BindValue_When_value_is_passed_and_original_value_is_null_Then_sets_property_value()
		{
			var stubClass = new StubClass();
			var stubClassType = typeof(StubClass);
			var arrayPropertyInfo = stubClassType.GetProperty("Property");
			var binder = new StringArrayValueBinder(stubClass, arrayPropertyInfo);

			var someValue = "values;1;3;";
			binder.BindValue(someValue);

			Assert.Equal(4, stubClass.Property.Length);
			foreach (var str in someValue.Split(';'))
			{
				Assert.Contains(str, stubClass.Property);
			}
		}

		[Fact]
		public void BindValue_When_null_is_passed_and_original_value_is_null_Then_leaves_property_value_as_null()
		{
			var stubClass = new StubClass();
			var stubClassType = typeof(StubClass);
			var arrayPropertyInfo = stubClassType.GetProperty("Property");
			var binder = new StringArrayValueBinder(stubClass, arrayPropertyInfo);
							   
			binder.BindValue(null);

			Assert.Null(stubClass.Property);
		}

		[Fact]
		public void BindValue_When_value_is_passed_and_original_value_is_not_empty_Then_updates_property_value()
		{
			var stubClass = new StubClass {Property = new [] {"test", "second"}};
			var stubClassType = typeof(StubClass);
			var arrayPropertyInfo = stubClassType.GetProperty("Property");
			var binder = new StringArrayValueBinder(stubClass, arrayPropertyInfo);

			var updatedValue = "values;1;3;";
			binder.BindValue(updatedValue);

			Assert.Equal(4, stubClass.Property.Length);
			foreach (var str in updatedValue.Split(';'))
			{
				Assert.Contains(str, stubClass.Property);
			}
		}

		private class StubClass
		{
			public string[] Property { get; set; }
		}
	}
}