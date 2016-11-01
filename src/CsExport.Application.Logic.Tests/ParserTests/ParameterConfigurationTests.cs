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

		[Fact]
		public void WithSignature_When_called_for_lowercase_alзha_signature_Then_sets_parameterDefinition_signature()
		{
			var signature = "signature";
			var parameterDefinition = GetValidParameterDefinition();

			var parameterConfiguration =
				new ParameterConfiguration(parameterDefinition);

			parameterConfiguration.WithSignature(signature);

			Assert.Equal(signature, parameterDefinition.Signature);
		}

		[Fact]
		public void WithSignature_When_called_for_lowercase_alpha_signature_with_dashes_Then_sets_parameterDefinition_signature()
		{
			var signature = "signature-with-dash";
			var parameterDefinition = GetValidParameterDefinition();

			var parameterConfiguration =
				new ParameterConfiguration(parameterDefinition);

			parameterConfiguration.WithSignature(signature);

			Assert.Equal(signature, parameterDefinition.Signature);
		}

		[Fact]
		public void WithSignature_When_called_for_signature_containing_space_Then_throws_argumentException()
		{
			var signature = "signature with space";
			var parameterDefinition = GetValidParameterDefinition();

			var parameterConfiguration =
				new ParameterConfiguration(parameterDefinition);

			Assert.Throws<ArgumentException>(() => parameterConfiguration.WithSignature(signature));
		}

		[Fact]
		public void WithSignature_When_called_for_signature_containing_special_character_Then_throws_argumentException()
		{
			var signature = "signature#with#hashtags";
			var parameterDefinition = GetValidParameterDefinition();

			var parameterConfiguration =
				new ParameterConfiguration(parameterDefinition);

			Assert.Throws<ArgumentException>(() => parameterConfiguration.WithSignature(signature));
		}

		[Fact]
		public void WithSignature_When_called_for_too_long_signature_Then_throws_argumentException()
		{
			var signature = "too-long-parameter-signature-with-too-many-letters-and-words";
			var parameterDefinition = GetValidParameterDefinition();

			var parameterConfiguration =
				new ParameterConfiguration(parameterDefinition);

			Assert.Throws<ArgumentException>(() => parameterConfiguration.WithSignature(signature));
		}

		[Fact]
		public void WithSignature_When_called_for_null_Then_throws_argumentNullException()
		{											  
			var parameterDefinition = GetValidParameterDefinition();

			var parameterConfiguration =
				new ParameterConfiguration(parameterDefinition);

			Assert.Throws<ArgumentNullException>(() => parameterConfiguration.WithSignature(null));
		}

		private static ParameterDefinition GetValidParameterDefinition()
		{
			return new ParameterDefinition
			{
				PropertyInfo = typeof(StubClass).GetProperty(nameof(StubClass.StringProperty))
			};
		}

		private class StubClass
		{
			public string StringProperty { get; set; }
		}
	}
}