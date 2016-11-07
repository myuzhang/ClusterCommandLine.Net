using System;
using System.ComponentModel;

namespace ClusterCommandLine
{
    public abstract class Command
    {
        public void HelpHeader()
        {
            Console.WriteLine(@"TCMX - Test Case Management eXtention Tool, Version 1.2");
            Console.WriteLine(@"Copyright (c) DimensionData Corporation.  All rights reserved.");
            Console.WriteLine();

            Console.WriteLine(@"Notes of Command Option:");
            Console.WriteLine(@"The command needs option 'collection' and 'teamproject',
you can set it in the command line or in the local profile
by using command 'tcmx config setlocal'");
            Console.WriteLine();
        }

        public virtual void ActionHelpHeader() { }

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
