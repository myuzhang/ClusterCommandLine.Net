using System;

namespace ClusterCommandLine.Exceptions
{
    internal class CommandNotFound : Exception
	{
		public CommandNotFound(string command)
			: base(string.Format("The combination of command and action <{0}> was not found", command))
		{
		}
	}
}
