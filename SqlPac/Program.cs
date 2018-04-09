namespace SqlPac
{
    using System;
    using SqlPac.Library;

    class Program
    {
        static void Main(string[] args)
        {
            string arg = string.Join(" ", args);
            Console.WriteLine(arg);
            string[] commands = arg.Split('-');
            for (int i = 0; i < commands.Length; i++)
            {
                commands[i] = $"-{commands[i]}";
            }
            var command = new CommandFactory().Create(commands);
            try
            {
                if (command.IsGet)
                {
                    var pull = PullDacpacFactory.Build(command);
                    pull.Initialize();
                    if (pull.HasPackages)
                    {
                        pull.Process();
                    }
                }
                else
                {
                    var push = PushDacpacFactory.Build(command);
                    push.Initialize();
                    push.Save();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error in SqlPac: {ex.Message}. \n\rDetails: {ex.ToString()}");
            }
        }
    }
}
