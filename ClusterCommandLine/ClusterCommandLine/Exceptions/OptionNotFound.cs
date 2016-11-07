using System;

namespace ClusterCommandLine.Exceptions
{
    internal class OptionNotFound : Exception
	{
		public OptionNotFound(string option)
			: base(string.Format("{0} option was required", option))
		{
		}
	}
}
