using System.Linq;
using CsExport.Application.Infrastructure.Parser;
using CsExport.Application.Infrastructure.Parser.Utility;
using Moq;
using Xunit;

namespace CsExport.Application.Infrastructure.Tests.ParserTests
{
	public class CommandArgumentParserTests
	{
		private readonly ICommandArgumentParser _commandArgumentParser;

		private readonly Mock<IReflectionPropertyBinderFactory> _reflectionPropertyBinderFactory =
			new Mock<IReflectionPropertyBinderFactory>();

		private readonly Mock<IParameterExtractor> _parameterExtractorMock = new Mock<IParameterExtractor>();

		public CommandArgumentParserTests()
		{
			_commandArgumentParser = new CommandArgumentParser(_reflectionPropertyBinderFactory.Object,
			                                                   _parameterExtractorMock.Object);
		}


		[Fact]
		public void CanParse_When_empty_input_string_is_passed_Then_returns_false()
		{
			var result = _commandArgumentParser.CanParse(string.Empty, GetStubCommandDefinition());

			Assert.False(result);
		}

		[Fact]
		public void CanParse_When_null_input_string_is_passed_Then_returns_false()
		{
			var result = _commandArgumentParser.CanParse(null, GetStubCommandDefinition());

			Assert.False(result);
		}

		[Fact]
		public void CanParse_When_valid_input_and_null_commandDefinition_is_passed_Then_returns_false()
		{
			var result = _commandArgumentParser.CanParse("command", null);

			Assert.False(result);
		}

		[Fact]
		public void CanParse_When_valid_input_and_commandDefinition_with_matching_signature_Then_returns_true()
		{
			var result = _commandArgumentParser.CanParse("command", GetStubCommandDefinition());

			Assert.True(result);
		}

		[Fact]
		public void CanParse_When_valid_input_with_arguments_and_commandDefinition_with_matching_signature_Then_returns_true()
		{
			var result = _commandArgumentParser.CanParse("command asd", GetStubCommandDefinition());

			Assert.True(result);
		}

		[Fact]
		public void
			CanParse_When_valid_input_with_spaces_before_command_name_and_commandDefinition_with_matching_signature_without_spaces_Then_returns_false
			()
		{
			var result = _commandArgumentParser.CanParse("  command", GetStubCommandDefinition());

			Assert.False(result);
		}

		[Fact]
		public void Parse_When_invalid_input_is_passed_Then_returns_null()
		{
			var arguments = _commandArgumentParser.Parse("invalid", GetStubCommandDefinition());

			Assert.Null(arguments);
		}

		[Fact]
		public void Parse_When_valid_input_is_passed_Then_returns_arguments_instance_of_expected_type()
		{
			var arguments = _commandArgumentParser.Parse("command", GetStubCommandDefinition());

			Assert.NotNull(arguments);
			Assert.IsType<StubArguments>(arguments);
		}

		[Fact]
		public void
			Parse_When_valid_input_without_parameters_is_passed_Then_returns_arguments_instance_with_default_field_values()
		{
			var arguments = (StubArguments) _commandArgumentParser.Parse("command", GetStubCommandDefinition());

			Assert.Equal(default(string), arguments.String);
			Assert.Equal(default(bool), arguments.Flag);
		}

		[Fact]
		public void Parse_When_valid_input_with_parameter_is_passed_Then_uses_binder_factory_for_each_defined_property()
		{
			var commandDefinition = GetStubCommandDefinition();
			var boolPropertyInfo =
				commandDefinition.Parameters.First(y => y.PropertyInfo.Name == nameof(StubArguments.Flag)).PropertyInfo;
			var stringPropertyInfo =
				commandDefinition.Parameters.First(y => y.PropertyInfo.Name == nameof(StubArguments.String)).PropertyInfo;

			var result = _commandArgumentParser.Parse("command flag", commandDefinition);

			_reflectionPropertyBinderFactory.Verify(x => x.CreateForProperty(It.IsAny<StubArguments>(), boolPropertyInfo),
			                                        Times.Once);
			_reflectionPropertyBinderFactory.Verify(x => x.CreateForProperty(It.IsAny<StubArguments>(), stringPropertyInfo),
			                                        Times.Once);
		}

		[Fact]
		public void
			Parse_When_valid_input_with_parameter_is_passed_Then_uses_parameterExtractor_to_get_value_for_parameter_passing_input_with_stripped_command_signature_and_trimmed
			()
		{
			var commandDefinition = GetStubCommandDefinition();
			var boolPropertyParameterDefinition =
				commandDefinition.Parameters.First(y => y.PropertyInfo.Name == nameof(StubArguments.Flag));
			var stringPropertyParameterDefinition =
				commandDefinition.Parameters.First(y => y.PropertyInfo.Name == nameof(StubArguments.String));

			var result = _commandArgumentParser.Parse("command -flag", commandDefinition);

			_parameterExtractorMock.Verify(x => x.ExtractValue("-flag", boolPropertyParameterDefinition.Signature), Times.Once);
			_parameterExtractorMock.Verify(x => x.ExtractValue("-flag", stringPropertyParameterDefinition.Signature), Times.Once);
		}

		[Fact]
		public void
			Parse_When_valid_input_with_parameter_is_passed_and_its_value_is_extracted_from_input_Then_tries_to_bind_that_value_to_property_value
			()
		{
			var commandDefinition = GetStubCommandDefinition();
			var boolPropertyParameterDefinition =
				commandDefinition.Parameters.First(y => y.PropertyInfo.Name == nameof(StubArguments.Flag));
			var boolPropertyInfo = boolPropertyParameterDefinition.PropertyInfo;
			var reflectionPropertyValueBinderMock = new Mock<IReflectionPropertyValueBinder>();
			_reflectionPropertyBinderFactory.Setup(x => x.CreateForProperty(It.IsAny<StubArguments>(), boolPropertyInfo))
			                                .Returns(reflectionPropertyValueBinderMock.Object);
			var stringifiedPropertyValue = "val";

			_parameterExtractorMock.Setup(x => x.ExtractValue("-flag", boolPropertyParameterDefinition.Signature))
			                       .Returns(stringifiedPropertyValue);

			var result = _commandArgumentParser.Parse("command -flag", commandDefinition);

			reflectionPropertyValueBinderMock.Verify(x => x.BindValue(stringifiedPropertyValue), Times.Once);
		}

		[Fact]
		public void
			Parse_When_valid_input_with_parameter_that_returns_null_extracted_value_Then_passes_null_to_propertyValueBinder()
		{
			var commandDefinition = GetStubCommandDefinition();
			var boolPropertyParameterDefinition =
				commandDefinition.Parameters.First(y => y.PropertyInfo.Name == nameof(StubArguments.Flag));
			var boolPropertyInfo = boolPropertyParameterDefinition.PropertyInfo;
			var reflectionPropertyValueBinderMock = new Mock<IReflectionPropertyValueBinder>();
			_reflectionPropertyBinderFactory.Setup(x => x.CreateForProperty(It.IsAny<StubArguments>(), boolPropertyInfo))
			                                .Returns(reflectionPropertyValueBinderMock.Object);
			string stringifiedPropertyValue = null;

			_parameterExtractorMock.Setup(x => x.ExtractValue("-flag", boolPropertyParameterDefinition.Signature))
			                       .Returns(stringifiedPropertyValue);

			var result = _commandArgumentParser.Parse("command -flag", commandDefinition);

			reflectionPropertyValueBinderMock.Verify(x => x.BindValue(stringifiedPropertyValue), Times.Once);
		}

		private class StubArguments : IArguments
		{
			public string String { get; set; }
			public bool Flag { get; set; }
		}

		private CommandDefinition GetStubCommandDefinition()
		{
			var argumentsType = typeof(StubArguments);
			var stringArgumentsPropertyInfo = argumentsType.GetProperty(nameof(StubArguments.String));
			var boolArgumentsPropertyInfo = argumentsType.GetProperty(nameof(StubArguments.Flag));

			var commandDefinition = new CommandDefinition(typeof(StubArguments));
			commandDefinition.SetSignature("command");
			commandDefinition.AddParameterDefinition(stringArgumentsPropertyInfo);
			commandDefinition.AddParameterDefinition(boolArgumentsPropertyInfo);

			return commandDefinition;
		}
	}
}