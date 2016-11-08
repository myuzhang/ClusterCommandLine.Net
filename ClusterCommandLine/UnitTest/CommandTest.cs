using System;
using ClusterCommandLine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class CommandTest
    {
        [TestMethod]
        public void TestActions()
        {
            var tryCommand = new TryCommand();
            Console.WriteLine("controller-action1:");
            ClusterCommand.Exec<Option>(new []{ "controller", "action1", "/con:controller", "/act1:one" });
            Console.WriteLine("controller-action2:");
            ClusterCommand.Exec<Option>(new[] { "controller", "action2", "/con:controller", "/act2:two", "/act2a:twoa" });
            Console.WriteLine("controller-action2 with optional parameter:");
            ClusterCommand.Exec<Option>(new[] { "controller", "action2", "/con:controller", "/act2:two", "/act2a:twoa", "/act2Optional:twoOptional" });
        }

        [TestMethod]
        public void TestHelp()
        {
            var tryCommand = new TryCommand();
            Console.WriteLine("command helper:");
            ClusterCommand.Exec<Option>(new[] { "/?" }, "TestHelp");
            Console.WriteLine("action helper:");
            ClusterCommand.Exec<Option>(new[] { "controller", "/h" }, "TestHelp");
        }
    }
}
