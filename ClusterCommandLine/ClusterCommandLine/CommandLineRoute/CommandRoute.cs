using System;

namespace ClusterCommandLine.CommandLineRoute
{
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class CommandRoute : Attribute
	{
		public CommandRoute(string action, string options)
		{
			Action = action;
			Options = options;
		}

		//public CommandRoute(string template)
		//{
		//	Template = template;
		//}

		public string Action { get; private set; }
		
		public string Options { get; private set; }
	}
}
