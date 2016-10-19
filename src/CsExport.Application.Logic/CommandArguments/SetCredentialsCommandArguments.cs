namespace CsExport.Application.Logic.CommandArguments
{
	public class SetCredentialsCommandArguments
	{
		public string Secret { get; set; }

		public override bool Equals(object obj)
		{
			var source = this;
			var target = obj as SetCredentialsCommandArguments;

			if (target == null)
				return false;

			return source.Secret == target.Secret;
		}
	}
}
