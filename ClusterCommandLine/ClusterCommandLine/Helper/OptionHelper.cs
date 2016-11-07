using System.Text;
using System.Text.RegularExpressions;
using ClusterCommandLine.Exceptions;
using ClusterCommandLine.Models;
using Newtonsoft.Json;

namespace ClusterCommandLine.Helper
{
	internal static class OptionHelper
	{
		public static Option ToOption(this string[] args)
		{
			StringBuilder builder = new StringBuilder();
			foreach (var s in args)
			{
				string pattern = @"/(\w+):";
				if (s.StartsWith("/"))
				{
					try
					{
						string opt = Regex.Matches(s, pattern, RegexOptions.IgnoreCase)[0].Groups[1].Value;
						string value = s.Substring(opt.Length + 2);
						string pair = string.Format("\"{0}\":\"{1}\",", opt, value);
						builder.Append(pair);
					}
					catch (System.ArgumentOutOfRangeException)
					{
						throw new OptionNotValid(s);
					}
				}
			}
			var options = builder.ToString();

			if (options.EndsWith(","))
			{
				options = options.Remove(options.Length - 1);
			}

            options = options.Replace(@"\", @"\\");

			string optionJson = string.Format("{{ {0} }}", options);
			Option option = JsonConvert.DeserializeObject<Option>(optionJson);
			return option;
		}
	}
}
