using System;
using System.ComponentModel;

namespace ClusterCommandLine
{
    public abstract class Command
    {
        public virtual void HelpHeader()
        {
            Console.WriteLine(@"Cluster Command Set, Version 1.0");
            Console.Write(@"GNU GENERAL PUBLIC LICENSE.");
            Console.WriteLine(@"Copyright (C) 2007 Free Software Foundation");
            Console.WriteLine();
        }

        public virtual void ActionHelpHeader()
        {
            Console.WriteLine(@"Notes of Command Option:");
            Console.WriteLine(@"The first parameter is the command name");
            Console.WriteLine(@"The second parameter is the command controller");
            Console.WriteLine(@"The third parameter is the action name");
            Console.WriteLine(@"The fourth parameter is the command option");
        }

        public virtual void ActionHelpExample() { }

        protected virtual void ShowOption(Object obj)
        {
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(obj);
                Console.WriteLine("{0}={1}", name, value);
            }
        }
    }
}
