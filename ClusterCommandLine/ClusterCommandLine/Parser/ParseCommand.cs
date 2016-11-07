using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Autofac;
using ClusterCommandLine.CommandLineRoute;
using ClusterCommandLine.Exceptions;
using ClusterCommandLine.Helper;

namespace ClusterCommandLine.Parser
{
    internal class ParseCommand
	{
		private static ParseCommand _instance;

		private ParseCommand()
		{
		}

		public static ParseCommand Instance
		{
			get { return _instance ?? (_instance = new ParseCommand()); }
		}

		public void Run(string[] args)
		{
			bool validate = false;
			Assembly thisAssembly = GetType().Assembly;
			foreach (Type type in thisAssembly.GetTypes())
			{
				if (type.IsSubclassOf(typeof(Command)))
				{
					var commonOptionAttribute = type.GetCustomAttribute(typeof(CommandRoutePrefix));
					if (commonOptionAttribute == null)
					{
						continue;
					}
					CommandRoutePrefix p = (CommandRoutePrefix)commonOptionAttribute;
					// check for the command
					if (args[0].Equals(p.Prefix, StringComparison.InvariantCultureIgnoreCase))
					{
						var methods = type.GetMethods();
						foreach (MethodInfo methodInfo in methods)
						{
							Attribute a = methodInfo.GetCustomAttribute(typeof(CommandRoute));
							if (a != null)
							{
								CommandRoute r = (CommandRoute)a;
								// check the action
								if (args[1].Equals(r.Action, StringComparison.InvariantCultureIgnoreCase))
								{
									string commandArgs = args.ToCommandStringLine();
									string commandOpts = p.CommonOptions + r.Options;
									MatchCollection routeCollection = commandOpts.ToCommandOptionCollection();
									if (routeCollection.Count.Equals(0)) validate = true;
									for (var i = 0; i < routeCollection.Count; i++)
									{
										if (commandArgs.Contains(routeCollection[i].Value))
											validate = true;
										else
										{
											string missingOption = routeCollection[i].Value;
											throw new OptionNotFound(missingOption);
										}
									}
									if (validate)
									{
										var command = CommandContainer.Instance.Container.Resolve(type);
										try
										{
											methodInfo.Invoke(command, new object[] { args.ToOption() });
										}
										catch (Exception e)
										{
											throw e.InnerException;
										}
										return;
									}
								}
							}
						}
						throw new CommandNotFound(string.Format("{0} {1}", args[0], args[1]));
					}
				}
			}
		}
	}
}
