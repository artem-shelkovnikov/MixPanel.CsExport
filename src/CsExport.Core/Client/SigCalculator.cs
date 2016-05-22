using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CsExport.Core.Client
{
	public class SigCalculator : ISigCalculator
	{
		public string Calculate(IDictionary<string, string> parameters, string secret)
		{
			if (parameters == null)
				throw new ArgumentNullException("parameters");

			if (secret == null)
				throw new ArgumentNullException("secret");

			var sortedParameters = parameters.OrderBy(x => x.Key);

			var stringToHash = string.Format("{0}{1}", string.Join(string.Empty, sortedParameters.Select(x=> string.Format("{0}={1}", x.Key, x.Value))), secret);

			var calculatedHash = CalculateMD5(stringToHash);

			return calculatedHash.ToLowerInvariant();
		}

		private string CalculateMD5(string stringToHash)
		{
			var md5 = MD5.Create();

			var bytesToHash = Encoding.ASCII.GetBytes(stringToHash);

			var computedHashBytes = md5.ComputeHash(bytesToHash);

			StringBuilder sb = new StringBuilder();
			foreach (byte hashByte in computedHashBytes)
			{
				sb.Append(hashByte.ToString("X2"));
			}
			return sb.ToString();
		}
	}
}