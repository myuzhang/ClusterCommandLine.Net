using System;

namespace ClusterCommandLine.CommandLineRoute
{
	[AttributeUsage(AttributeTargets.Class)]
	public class CommandRoutePrefix : Attribute
	{
		public CommandRoutePrefix(string prefix)
		{
			Prefix = prefix;
		}

		public CommandRoutePrefix(string prefix, string commonOptions)
		{
			Prefix = prefix;
			CommonOptions = commonOptions;
		}

		public string Prefix { get; private set; }

		public string CommonOptions { get; private set; }

		public string HelpText { get; set; }
	}
}
