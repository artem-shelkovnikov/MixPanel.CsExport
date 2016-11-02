using System;
using System.Text.RegularExpressions;

namespace CsExport.Application.Logic.Parser
{
	public static class SignatureValidation
	{
		private const int MaxSignatureLength = 20;
		private static readonly Regex SignatureValidationRegex = new Regex("^[a-z0-9-]+$");

		public static void Validate(string signature)
		{
			if (signature == null)
				throw new ArgumentNullException(nameof(signature));

			if (SignatureValidationRegex.Match(signature).Value != signature)
				throw new ArgumentException("Parameter signature must contain only lower case letter, numbers and dashes",
				                            nameof(signature));

			if (signature.Length > MaxSignatureLength)
				throw new ArgumentException($"Signature maximum length is {MaxSignatureLength}", nameof(signature));
		}
	}
}