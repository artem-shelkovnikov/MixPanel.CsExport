using System;
using System.Linq;
using CsExport.Core;

namespace CsExport.Application.Logic.CommandArguments
{
	public class RawExportCommandArguments
	{
		public Date From { get; set; }
		public Date To { get; set; }

		public string[] Events { get; set; }

		public override bool Equals(object obj)
		{	  
			var source = this;
			var target = obj as RawExportCommandArguments;

			if (target == null)
				return false;

			return source.From.Equals(target.From)
					&& source.To.Equals(target.To)
					&& source.Events.Length.Equals(target.Events.Length)
					&& source.Events.All(x => target.Events.Contains(x));
		}
	}
}