using System;

namespace ClusterCommandLine.Exceptions
{
    internal class OptionNotValid : Exception
	{
		public OptionNotValid(string option)
			: base(string.Format("{0} option was not valid", option))
		{
		}
	}
}
