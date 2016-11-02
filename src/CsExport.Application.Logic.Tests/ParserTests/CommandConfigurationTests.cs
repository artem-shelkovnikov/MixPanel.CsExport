using System;
using CsExport.Application.Logic.Parser;
using Xunit;

namespace CsExport.Application.Logic.Tests.ParserTests
{
	public class CommandConfigurationTests
	{
		[Fact]
		public void Ctor_when_command_has_valid_signature_Then_does_not_throw()
		{
			var command = new ValidCommandConfiguration();
		}

		[Fact]
		public void Ctor_when_command_has_signature_containing_invalid_character_Then_throws_commandConfigurationException()
		{
			Assert.Throws<ArgumentException>(() => new InvalidSignatureCommandConfiguration());
		}

		[Fact]
		public void Ctor_when_command_has_signature_that_is_too_long_Then_throws_commandConfigurationException()
		{
			Assert.Throws<ArgumentException>(() => new TooLongSignatureCommandConfiguration());
		}

		private class ValidCommandConfiguration : CommandConfiguration<StubArguments>
		{
			public ValidCommandConfiguration()
			{
				HasSignature("valid-signature");
			}
		}

		private class InvalidSignatureCommandConfiguration : CommandConfiguration<StubArguments>
		{
			public InvalidSignatureCommandConfiguration()
			{
				HasSignature("invalid signature");
			}
		}

		private class TooLongSignatureCommandConfiguration : CommandConfiguration<StubArguments>
		{
			public TooLongSignatureCommandConfiguration()
			{
				HasSignature("too-long-signature-that-throws-exception");
			}
		}

		private class StubArguments : IArguments
		{
		}
	}
}