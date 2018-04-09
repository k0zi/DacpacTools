
namespace DacpacPack
{
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;
    using SqlPac.Library;

    public class GetDacpac : Task
    {
        [Required]
        public string PackagesPath { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public string ProjectDir { get; set; }

        [Required]
        public string PackageServerEndpoint { get; set; }

        [Required]
        public bool IsDacpacService { get; set; }

        private PullDacpac pullDacpac;

        public override bool Execute()
        {
            pullDacpac = PullDacpacFactory.Build(
                PackagesPath, 
                ProjectName, 
                ProjectDir, 
                PackageServerEndpoint, 
                IsDacpacService);

            pullDacpac.Initialize();
            if (pullDacpac.HasPackages)
            {
                return pullDacpac.Process();
            }
            return true;
        }
    }
}
