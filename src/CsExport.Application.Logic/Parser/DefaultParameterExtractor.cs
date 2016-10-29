using System;
using System.Linq;

namespace CsExport.Application.Logic.Parser
{
	public class DefaultParameterExtractor : IParameterExtractor
	{
		private const char ParameterPrefix = '-';
		private const char ParameterValueSeparator = '=';
		private const char ParameterValueEscapeSymbol = '"';
		private const char ParameterTerminator = ' ';

		//TODO: refactor, too complex and unclear
		public string ExtractValue(string input, string parameterName)
		{
			var trimmedInput = input.Trim();

			var parameterSignature = ParameterPrefix + parameterName;

			var firstIndexOfParameterSignature = trimmedInput.IndexOf(parameterSignature,
			                                                          StringComparison.InvariantCultureIgnoreCase);

			if (firstIndexOfParameterSignature == -1)
				return null;

			var escapeCharactersCountBeforeParameter =
				trimmedInput.Substring(0, firstIndexOfParameterSignature).Count(y => y == ParameterValueEscapeSymbol);

			if (escapeCharactersCountBeforeParameter%2 == 0) //this is really a parameter
			{
				var indexOfFirstSymbolAfterParameterSignature = firstIndexOfParameterSignature + parameterSignature.Length;
				if (indexOfFirstSymbolAfterParameterSignature >= trimmedInput.Length)
					return string.Empty;

				var nextSymbolAfterParameterSignature = trimmedInput.ElementAt(indexOfFirstSymbolAfterParameterSignature);
				if (nextSymbolAfterParameterSignature != ParameterValueSeparator)
					return string.Empty;

				var partToGetValue = trimmedInput.Substring(indexOfFirstSymbolAfterParameterSignature + 1);

				if (partToGetValue.StartsWith(ParameterValueEscapeSymbol.ToString()))
				{
					var indexOfNextEscapeSymbol = partToGetValue.IndexOf(ParameterValueEscapeSymbol, 1);
					if (indexOfNextEscapeSymbol == -1)
						return null;

					return partToGetValue.Substring(1, indexOfNextEscapeSymbol - 1);
				}

				var indexOfSpace = partToGetValue.IndexOf(ParameterTerminator);
				if (indexOfSpace == -1)
					return partToGetValue;

				return partToGetValue.Substring(0, indexOfSpace);
			}

			var indexOfClosingEscapeCharacter = trimmedInput.IndexOf(ParameterValueEscapeSymbol, firstIndexOfParameterSignature);

			if (indexOfClosingEscapeCharacter != -1)
				return ExtractValue(trimmedInput.Substring(indexOfClosingEscapeCharacter + 1), parameterName);

			return null;
		}
	}
}