namespace SqlPac.Library
{
    public class PushDacpacFactory
    {
        public static PushDacpac Build(
            string projectDir, 
            string projectName, 
            string packageServerEndpoint, 
            bool isDacpacService)
        {
            return new PushDacpac(
                projectDir, 
                projectName, 
                packageServerEndpoint, 
                isDacpacService);
        }

        public static PushDacpac Build(Command command)
        {
            return new PushDacpac(
                command.ProjectDir,
                command.ProjectName,
                command.PackageServerEndpoint,
                command.IsDacpacService);
        }
    }
}
