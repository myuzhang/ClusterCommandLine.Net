using System;
using ClusterCommandLine.Parser;

namespace ClusterCommandLine
{
    public class ClusterCommand
    {
        public static void Exec(string commandName, string[] args)
        {
            try
            {
                ShowCommand.Instance.ApplicationName = commandName;
                if (ShowCommand.Instance.IsHelpCommand(args))
                {
                    ShowCommand.Instance.ShowCommandHelper(args);
                    return;
                }
                ParseCommand.Instance.Run(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
