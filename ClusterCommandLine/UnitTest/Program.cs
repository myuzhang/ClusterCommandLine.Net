using System;
using ClusterCommandLine;

namespace UnitTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var tryCommand = new TryCommand();
            ClusterCommand.Exec<Option>(args);
        }
    }
}
