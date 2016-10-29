using CsExport.Application.Logic.Parser;
using Xunit;

namespace CsExport.Application.Logic.Tests.ParserTests
{
	public class DefaultParameterExtractorTests
	{
		readonly IParameterExtractor _parameterExtractor = new DefaultParameterExtractor();

		[Fact]
		public void ExtractValue_When_valid_input_containing_excepted_parameterName_is_passed_Then_extracts_value()
		{
			var parameterName = "test";
			var input = "-test=asd";

			var value = _parameterExtractor.ExtractValue(input, parameterName);

			Assert.Equal("asd", value);
		}

		[Fact]
		public void ExtractValue_When_valid_not_trimmed_input_containing_excepted_parameterName_is_passed_Then_extracts_value()
		{
			var parameterName = "test";
			var input = "   -test=asd   ";

			var value = _parameterExtractor.ExtractValue(input, parameterName);

			Assert.Equal("asd", value);
		}

		[Fact]
		public void
			ExtractValue_When_valid_input_with_space_after_equality_containing_excepted_parameterName_is_passed_Then_extracts_value
			()
		{
			var parameterName = "test";
			var input = "-test= asd";

			var value = _parameterExtractor.ExtractValue(input, parameterName);

			Assert.Equal(string.Empty, value);
		}

		[Fact]
		public void ExtractValue_When_valid_input_with_escaped_parameter_value_is_passed_Then_extracts_value_inside_quotes()
		{
			var parameterName = "test";
			var input = "-test=\" asd\"";

			var value = _parameterExtractor.ExtractValue(input, parameterName);

			Assert.Equal(" asd", value);
		}

		[Fact]
		public void
			ExtractValue_When_valid_input_with_incorrectly_escaped_parameter_value_is_passed_Then_extracts_value_inside_quotes()
		{
			var parameterName = "test";
			var input = "-test=\" as";

			var value = _parameterExtractor.ExtractValue(input, parameterName);

			Assert.Null(value);
		}

		[Fact]
		public void ExtractValue_When_valid_input_not_containing_expected_parameterName_is_passed_Then_extracts_value()
		{
			var parameterName = "sss";
			var input = "-test=asd -next=hello -bool";

			var value = _parameterExtractor.ExtractValue(input, parameterName);

			Assert.Null(value);
		}

		[Fact]
		public void
			ExtractValue_When_valid_input_containing_expected_parameterName_with_multiple_parameters_is_passed_Then_extracts_value
			()
		{
			var parameterName = "test";
			var input = "-flag -test=asd";

			var value = _parameterExtractor.ExtractValue(input, parameterName);

			Assert.Equal("asd", value);
		}

		[Fact]
		public void
			ExtractValue_When_valid_input_containing_same_expected_parameterName_several_times_is_passed_Then_extracts_first_value
			()
		{
			var parameterName = "test";
			var input = "-test=asd -test=next -test=asdasdasdfff";

			var value = _parameterExtractor.ExtractValue(input, parameterName);

			Assert.Equal("asd", value);
		}

		[Fact]
		public void ExtractValue_When_valid_input_with_flag_type_input_is_passed_Then_returns_empty_string()
		{
			var parameterName = "flag";
			var input = "-flag";

			var value = _parameterExtractor.ExtractValue(input, parameterName);

			Assert.Equal(string.Empty, value);
		}

		[Fact]
		public void
			ExtractValue_When_valid_input_with_flag_type_input_is_passed_with_space_before_value_separator_Then_returns_empty_string
			()
		{
			var parameterName = "flag";
			var input = "-flag =asd";

			var value = _parameterExtractor.ExtractValue(input, parameterName);

			Assert.Equal(string.Empty, value);
		}

		[Fact]
		public void
			ExtractValue_When_valid_input_with_flag_type_input_inline_after_binary_parameter_is_passed_Then_returns_empty_string()
		{
			var parameterName = "flag";
			var input = "-test=asdfgh -flag";

			var value = _parameterExtractor.ExtractValue(input, parameterName);

			Assert.Equal(string.Empty, value);
		}

		[Fact]
		public void
			ExtractValue_When_valid_input_with_flag_parameter_signature_passed_inside_escaped_parameter_value_Then_returns_null
			()
		{
			var parameterName = "flag";
			var input = "`-test=\"-flag\"";

			var value = _parameterExtractor.ExtractValue(input, parameterName);

			Assert.Null(value);
		}

		[Fact]
		public void
			ExtractValue_When_valid_input_with_flag_parameter_signature_passed_also_fter_escaped_parameter_value_Then_returns_empty_string
			()
		{
			var parameterName = "flag";
			var input = "`-test=\"-flag\" -flag";

			var value = _parameterExtractor.ExtractValue(input, parameterName);

			Assert.Equal(string.Empty, value);
		}

		[Fact]
		public void
			ExtractValue_When_valid_input_with_incorrectly_escaped_parameter_value_including_parameter_signature_Then_returns_null
			()
		{
			var parameterName = "flag";
			var input = "`-test=\"-flag";

			var value = _parameterExtractor.ExtractValue(input, parameterName);

			Assert.Null(value);
		}
	}
}