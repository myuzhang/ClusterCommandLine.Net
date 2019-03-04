# ClusterCommandLine.Net
The project is providing a method to implement a command set.
If you have needs to build a command set like this format:
"commandName.exe commandCluster commandAction commandOption"

Where:

commandName.exe - is your command name, generally your project name/exe file name

commandCluster - is the command set

commandAction - is the command action for each of the command set

commandOption - is the options for each of action, it can have required and optional options

# How to use:
The design is the same as WebAPI using attribute driven to define commandCluster, commandAction and commandOption. Here is an example (the project: UnitTest ):

#### Step 1: Install NuGet package @ https://www.nuget.org/packages/ClusterCommandLine/ .

#### Step 2: Add attribute "CommandRoutePrefix" to the class which you are using to implement your command business. (refer to TryCommand.cs)
   Here are two parameters for the attribute: the first is for commandCluster name and the second is for commandOption which is the common option for all command actions in the class.
        You may also specify the help text for the cluster command.
        
#### Step 3, Inherit from class Command. (refer to TryCommand.cs)

#### Step 4, Add attribute "CommandRoute" to the command action method. Note that the action method must have one but only one input parameter. See next step to see the input parameter. (refer to TryCommand.cs)
  Here are two parameters for the attribute: the first is for the command action name and the second is for command option.
        
#### Step 5, Create a class for the command option. (refer to Option.cs)
  Note that if the command option defined in CommandRoutePrefix or CommandRoute has square braket, it means the command is optional. Otherwise it is required.
        
#### Step 6, In your Main method, Run this line: (refer to Program.cs)
```
ClusterCommand.Exec<Option>(args);
```
  Note the type Option is the class you defined for your command option.
  
#### Step 7, You can add the command header, command action help and command action example in your logic class as you like,
```
public override void HelpHeader() - command header
public override void ActionHelpHeader() - command action help
public override void ActionHelpExample() - command action example
```
