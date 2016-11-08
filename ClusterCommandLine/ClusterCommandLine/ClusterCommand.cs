using System;
using System.IO;
using ClusterCommandLine.Parser;

namespace ClusterCommandLine
{
    public class ClusterCommand
    {
        public static void Exec<TOption>(string[] args, string commandName = null)
        {
            try
            {
                ShowCommand.Instance.ApplicationName = commandName ?? Path.GetFileName(System.Reflection.Assembly.GetEntryAssembly().Location);
                if (ShowCommand.Instance.IsHelpCommand(args))
                {
                    ShowCommand.Instance.ShowCommandHelper(args);
                    return;
                }
                ParseCommand.Instance.Run<TOption>(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
