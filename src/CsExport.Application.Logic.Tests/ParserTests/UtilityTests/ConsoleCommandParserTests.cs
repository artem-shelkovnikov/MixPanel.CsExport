using System.Collections.Generic;
using System.Linq;
using CsExport.Application.Logic.Parser.Utility;
using Xunit;

namespace CsExport.Application.Logic.Tests.ParserTests.UtilityTests
{
	public class ConsoleCommandParserTests
	{
		private ConsoleCommandParser _commandParser	 = new ConsoleCommandParser();

		[Fact]
		public void Parse_When_only_command_name_is_passed_Then_returns_definition_only_with_command_name()
		{
			var input = "some-command";

			var result = _commandParser.Parse(input);

			Assert.Equal(input, result.Name);
			Assert.Empty(result.Arguments);
		}

		[Fact]
		public void
			Parse_when_command_with_parameter_with_value_is_passed_Then_returns_definition_with_correct_parameter()
		{
			var input = "some-command -hello=world";

			var result = _commandParser.Parse(input);
			var parameter = result.Arguments.Single();

			Assert.Equal("hello", parameter.ArgumentName);
			Assert.Equal("world", parameter.Value);
		}

		[Fact]
		public void
			Parse_when_command_with_parameter_without_value_is_passed_Then_returns_definition_with_correct_parameter_without_value()
		{
			var input = "some-command -hello";

			var result = _commandParser.Parse(input);
			var parameter = result.Arguments.Single();

			Assert.Equal("hello", parameter.ArgumentName);
			Assert.Null(parameter.Value);
		}

		[Fact]
		public void Parse_When_command_with_parameter_with_dashed_is_passed_Then_returns_correct_command_definition()
		{
			var input = "some-command -value=with-dashes-inside";

			var result = _commandParser.Parse(input);
			var parameter = result.Arguments.Single();

			Assert.Equal("value", parameter.ArgumentName);
			Assert.Equal("with-dashes-inside", parameter.Value);	  
		}

		[Fact]
		public void Parse_When_command_with_parameter_with_spaces_and_dashes_is_passed_Then_returns_correct_command_definition()
		{
			var input = "some-command -value=with spaces-and dashes inside";

			var result = _commandParser.Parse(input);
			var parameter = result.Arguments.Single();

			Assert.Equal("value", parameter.ArgumentName);
			Assert.Equal("with spaces-and dashes inside", parameter.Value);	  
		}

		[Fact]
		public void Parse_When_command_multiple_different_parameters_is_passed_Then_returns_definition_with_correct_quantity()
		{
			var input = "some-command  -value=with-dashes-inside -hello=what,a,wonderful world -valueless";

			var result = _commandParser.Parse(input);

			Assert.Equal(3, result.Arguments.Count());	   
		}

		[Fact]
		public void Parse_When_command_multiple_different_parameters_is_passed_Then_returns_definition_with_correct_parameter_keys_and_values()
		{
			var parameters = new Dictionary<string, string>
			{
				{"value", "with-dashes-inside" },
				{"hello", "what,a,wonderful world" },
				{"valueless", null }
			};

			var formattedParameters = parameters.Select(
				x => x.Value == null
					? string.Format("-{0}", x.Key)
					: string.Format("-{0}={1}", x.Key, x.Value));

			var input = "some-command  " + string.Join(" ", formattedParameters);

			var result = _commandParser.Parse(input);

			foreach (var parameter in parameters)
			{
				Assert.Equal(1, result.Arguments.Count(y=>y.ArgumentName == parameter.Key && y.Value == parameter.Value));
			}
		}

		[Fact]
		public void Parse_When_called_with_single_word_starting_with_dash_Then_parses_word_with_dash_as_command_name()
		{
			var input = "-dash";

			var result = _commandParser.Parse(input);

			Assert.Equal(input, result.Name);
		}

		[Fact]
		public void Parse_When_called_for_invalid_command_Then_throws_parseException()
		{
			var input = "command with invalid input";

			Assert.Throws<ArgumentParseException>(()=>_commandParser.Parse(input));
		}
	}
}