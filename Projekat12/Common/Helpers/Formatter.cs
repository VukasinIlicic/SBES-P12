namespace Common.Helpers
{
	public class Formatter
	{
		public static string ParseName(string winLogonName)
		{
			string[] parts = new string[] {};

			if (winLogonName.Contains("@"))
			{
				parts = winLogonName.Split('@');
				return parts[0];
			}
			else if (winLogonName.Contains("\\"))
			{
				parts = winLogonName.Split('\\');
				return parts[1];
			}
			else
			{
				return winLogonName;
			}
		}
	}
}