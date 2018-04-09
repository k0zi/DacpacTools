namespace SqlPac
{
    using System.Linq;
    using SqlPac.Library;

    public class CommandFactory
    {
        public Command Create(string[] commands)
        {
            var command = new Command()
            {
                IsGet = GetCommandType(commands),
                ProjectName = GetProjectName(commands),
                ProjectDir = GetProjectDir(commands),
                IsDacpacService = GetServiceType(commands),
                PackageServerEndpoint = GetEndpoint(commands)
            };
            command.PackagesPath = GetPath(command.IsGet, commands);
            return command;
        }

        private string GetEndpoint(string[] commands)
        {
            var endPoint = commands.First(c => c.StartsWith(Commands.ENDPOINT));
            return endPoint.TrimStart(Commands.ENDPOINT.ToArray()).Trim();
        }

        private bool GetServiceType(string[] commands)
        {
            var typ = commands.FirstOrDefault(c => c.StartsWith(Commands.IS_SERVICE));
            if(typ == null)
            {
                return false;
            }
            return true;
        }

        private string GetProjectDir(string[] commands)
        {
            var dir = commands.First(c => c.StartsWith(Commands.PROJECT_DIR));
            return dir.TrimStart(Commands.PROJECT_DIR.ToArray()).Trim();
        }

        private string GetProjectName(string[] commands)
        {
            var project = commands.First(c => c.StartsWith(Commands.PROJECT_NAME));
            return project.TrimStart(Commands.PROJECT_NAME.ToArray()).Trim();
        }

        private string GetPath(bool isGet, string[] commands)
        {
            if(!isGet)
            {
                return string.Empty;
            }
            var path = commands.First(c => c.StartsWith(Commands.PACKAGES_PATH));
            return path.TrimStart(Commands.PACKAGES_PATH.ToArray()).Trim();
        }

        private bool GetCommandType(string[] commands)
        {
            return commands.FirstOrDefault(c => c.StartsWith(Commands.GET)) != null;
        }
    }
}
