using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Autofac;
using ClusterCommandLine.CommandLineRoute;
using ClusterCommandLine.Helper;

namespace ClusterCommandLine.Parser
{
    internal class ShowCommand
	{
		private static ShowCommand _instance;

		private ShowCommand()
		{
		}

        public string ApplicationName { get; set; }

        public static ShowCommand Instance
		{
			get { return _instance ?? (_instance = new ShowCommand()); }
		}

		public bool IsHelpCommand(string[] args)
		{
			return args.Count() <= 2 || args.IsHelp();
		}

		public void ShowCommandHelper(string[] args)
		{
			if (args.Count().Equals(0))
			{
				ShowCommandHelper();
				return;
			}

			if (args.Count().Equals(1) || args.Count().Equals(2))
			{
				bool isShowed = ShowActionHelper(args);
				if (isShowed) return;
			}

			ShowCommandHelper();
		}

		private void ShowCommandHelper()
		{
			Assembly thisAssembly = this.GetType().Assembly;
			foreach (Type type in thisAssembly.GetTypes())
			{
				if (type.IsSubclassOf(typeof (Command)))
				{
					var command = CommandContainer.Instance.Container.Resolve(type) as Command;
					command.HelpHeader();
					break;
				}
			}

			Dictionary<string, string> commandsHelper =
				(from type in thisAssembly.GetTypes()
					where type.IsSubclassOf(typeof (Command))
					select type.GetCustomAttribute(typeof (CommandRoutePrefix))
					into commonOptionAttribute
					select commonOptionAttribute)
					.Cast<CommandRoutePrefix>()
					.ToDictionary(p => p.Prefix, p => p.HelpText);

			Console.WriteLine("Commands:\n");
			foreach (var helper in commandsHelper)
			{
				Console.WriteLine("{0} {1}\t{2}\n", ApplicationName, helper.Key, helper.Value);
			}
		}

		private bool ShowActionHelper(string[] args)
		{
			bool isShowed = false;
			Assembly thisAssembly = this.GetType().Assembly;
			foreach (Type type in thisAssembly.GetTypes())
			{
				if (type.IsSubclassOf(typeof (Command)))
				{
					var commonOptionAttribute = type.GetCustomAttribute(typeof (CommandRoutePrefix));
					if (commonOptionAttribute == null)
					{
						continue;
					}
					CommandRoutePrefix p = (CommandRoutePrefix) commonOptionAttribute;
					if (args[0].Equals(p.Prefix, StringComparison.InvariantCultureIgnoreCase))
					{
						var command = CommandContainer.Instance.Container.Resolve(type) as Command;
						command.HelpHeader();
						command.ActionHelpHeader();

						Console.WriteLine("Options available for {0} command\n", args[0]);

						var methods = type.GetMethods();
						foreach (MethodInfo methodInfo in methods)
						{
							Attribute a = methodInfo.GetCustomAttribute(typeof(CommandRoute));
							if (a != null)
							{
								CommandRoute r = (CommandRoute)a;
								string actionLine = string.Format("{0} {1} {2}", ApplicationName, args[0], r.Action);
								string commandHelpText = p.CommonOptions + r.Options;
								string optionLine = commandHelpText.Replace("/", " /");
								PrintWithSecondColumnAlignment(actionLine, optionLine, " ");
								Console.WriteLine();
							}
						}

						command.ActionHelpExample();
						isShowed = true;
					}
				}
			}
			return isShowed;
		}

		private void PrintWithSecondColumnAlignment(string column1, string column2, string pattern)
		{
			int i = 0;
			StringBuilder builder = new StringBuilder();
			var matches = Regex.Split(column2, pattern);
			// skip the first whitespace
			if (string.IsNullOrWhiteSpace(matches[i]))
				i++;

			Console.WriteLine("{0,-20} {1,-40}", column1, matches[i++]);
			for (; i < matches.Count(); i++)
			{
				if (string.IsNullOrWhiteSpace(matches[i]))
				{
					continue;
				}
				Console.WriteLine("{0,-20} {1,-40}", " ", matches[i]);
			}
		}
	}
}
