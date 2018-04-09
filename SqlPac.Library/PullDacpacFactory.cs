namespace SqlPac.Library
{
    public class PullDacpacFactory
    {
        public static PullDacpac Build(
            string packagesPath, 
            string projectName, 
            string projectDir, 
            string packageServerEndpoint, 
            bool isDacpacService)
        {
            return new PullDacpac(packagesPath, projectName, projectDir, packageServerEndpoint, isDacpacService);
        }

        public static PullDacpac Build(Command command)
        {
            return new PullDacpac(
                command.PackagesPath, 
                command.ProjectName, 
                command.ProjectDir, 
                command.PackageServerEndpoint, 
                command.IsDacpacService);
        }
    }
}
