using System;
using System.Collections.Generic;
using System.Linq;
using CsExport.Core.Client;
using Xunit;

namespace CsExport.Core.Tests
{
	public class SigCalculatorTests
	{
		private ISigCalculator _sigCalculator = new SigCalculator();

		private const string Secret = "662809431a8b473862ad7814d232e0b8";

		[Fact]
		public void Calculate_When_called_with_null_as_parameters_Then_throws_argument_null_exception()
		{
			Assert.Throws<ArgumentNullException>(() => _sigCalculator.Calculate(null, Secret));
		}

		[Fact]
		public void Calculate_When_called_with_null_secret_Then_throws_argument_null_exception()
		{
			Assert.Throws<ArgumentNullException>(() => _sigCalculator.Calculate(new Dictionary<string, string>(), null));
		}

		[Fact]
		public void Calculate_When_called_with_test_parameters_Then_returns_expected_signature()
		{
			//real signature manually calculated and tested in api
			var expectedSig = "64ccf31e71adbf89c1df33af4d28c4fa";

			var parameters = GetParameters();
			
			var sig = _sigCalculator.Calculate(parameters, Secret);

			Assert.Equal(expectedSig, sig); 
		}

		[Fact]
		public void Calculate_When_called_with_same_parameters_in_different_order_Then_returns_same_signature()
		{
			var parameters = GetParameters();

			var parametersOrderedByKey = parameters.OrderBy(x => x.Key).ToDictionary(x=>x.Key, x=>x.Value);
			var parametersOrderedByKeyDescending = parameters.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

			var sigWithFirstOrder = _sigCalculator.Calculate(parametersOrderedByKey, Secret);
			var sigWithSecondOrder = _sigCalculator.Calculate(parametersOrderedByKeyDescending, Secret);

			Assert.Equal(sigWithFirstOrder, sigWithSecondOrder);
			
		}

		private static Dictionary<string, string> GetParameters()
		{
			return new Dictionary<string, string>
			{
				{"expire", "1483228800"},
				{"from_date", "2016-01-01"},
				{"to_date", "2016-03-01"},
				{"api_key", "ff28ac99bfb2bbc181a764684c993cd4"}
			};
		}
	}
}