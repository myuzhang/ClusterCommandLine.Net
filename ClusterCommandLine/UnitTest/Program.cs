using System;
using ClusterCommandLine;

namespace UnitTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ClusterCommand.Exec<Option>(args);
        }
    }
}
