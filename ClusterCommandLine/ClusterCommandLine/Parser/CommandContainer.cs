using System;
using Autofac;

namespace ClusterCommandLine.Parser
{
	internal class CommandContainer
	{
		private static CommandContainer _instance = null;

		public IContainer Container { get; private set; }

		private CommandContainer()
		{
			var builder = new ContainerBuilder();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder
				.RegisterAssemblyTypes(assemblies)
				.Where(t => t.IsSubclassOf(typeof (Command)))
				.AsSelf();

			Container = builder.Build();
		}

		public static CommandContainer Instance
		{
			get { return _instance ?? (_instance = new CommandContainer()); }
		}
	}
}
