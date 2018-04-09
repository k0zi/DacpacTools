namespace SqlPac.Library
{
    public class Command
    {
        public bool IsGet { get; set; }
        public string PackagesPath { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDir { get; set; }
        public string PackageServerEndpoint { get; set; }
        public bool IsDacpacService { get; set; }
    }
}
