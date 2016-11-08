using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClusterCommandLine;
using ClusterCommandLine.CommandLineRoute;

namespace UnitTest
{
    [CommandRoutePrefix("controller", "/con:controller", HelpText = "Try command controller")]
    public class TryCommand : Command
    {
        [CommandRoute("action1", "/act1:one")]
        public void Method1(Option option)
        {
            // ShowOption is for debug only
            ShowOption(option);
        }

        [CommandRoute("action2", "/act2:two/act2a:twoa/[act2Optional:twoOptional]")]
        public void Method2(Option option)
        {
            // ShowOption is for debug only
            ShowOption(option);
        }
    }
}
