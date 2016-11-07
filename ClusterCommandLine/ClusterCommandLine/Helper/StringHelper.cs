using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ClusterCommandLine.Helper
{
	internal static class StringHelper
	{
		public static MatchCollection ToCommandOptionCollection(this string routeOptions)
		{
			string pattern = @"/\w+:";
			return Regex.Matches(routeOptions, pattern, RegexOptions.IgnoreCase);
		}

		public static string ToCommandStringLine(this string[] args)
		{
			StringBuilder builder = new StringBuilder();
			foreach (string value in args)
			{
				builder.Append(value);
				builder.Append(' ');
			}
			return builder.ToString();
		}

		public static string ToCommandStringOption(this string[] args)
		{
			StringBuilder builder = new StringBuilder();
			for (int i = 2; i < args.Count(); i++)
			{
				builder.Append(args[i]);
				builder.Append(' ');
			}
			return builder.ToString();
		}

		public static string ToLeft40AlignedString(this string source, string pattern)
		{
			StringBuilder builder = new StringBuilder();
			var matches = Regex.Split(source, pattern);
			foreach (string match in matches)
			{
				if (string.IsNullOrWhiteSpace(match))
				{
					continue;
				}
				var oneLine = string.Format("{0,-40}\n", match);
				builder.Append(oneLine);
			}
			return builder.ToString();
		}

		public static string ToUserName(this string login)
		{
			var credential = login.Split(',');
			return credential[0];
		}

		public static string ToPassword(this string login)
		{
			var credential = login.Split(',');
			if (credential.Count() > 1)
			{
				return credential[1];
			}
			return null;
		}

		public static bool IsHelp(this string arg)
		{
			return arg.Equals("/help", StringComparison.InvariantCultureIgnoreCase) ||
				   arg.Equals("/h", StringComparison.InvariantCultureIgnoreCase) ||
				   arg.Equals("/?", StringComparison.InvariantCultureIgnoreCase);
		}

		public static bool IsHelp(this string[] args)
		{
			if (!args.Count().Equals(0))
			{
				return args.Any(a => a.IsHelp());
			}
			return false;
		}
	}
}
