using System;
using CsExport.Application.Logic.Parser;
using Xunit;

namespace CsExport.Application.Logic.Tests.ParserTests
{
	public class ParameterConfigurationTests
	{
		[Fact]
		public void Ctor_When_null_definition_is_passed_Then_throws_argunentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new ParameterConfiguration(null));
		}

		[Fact]
		public void Ctor_When_called_for_parameterDefinition_without_propertyInfo_Then_throws()
		{
			Assert.Throws<ArgumentNullException>(() => new ParameterConfiguration(new ParameterDefinition()));
		}
	}
}