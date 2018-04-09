namespace DacpacPack
{
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;
    using SqlPac.Library;

    public class PackDacpac : Task
    {
        [Required]
        public string ProjectDir { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public string PackageServerEndpoint { get; set; }

        [Required]
        public bool IsDacpacService { get; set; }

        [Required]
        public bool CreatePackage { get; set; }

        

        private PushDacpac pushDacpac;

        public override bool Execute()
        {
            if(!CreatePackage)
            {
                return true;
            }
            pushDacpac = PushDacpacFactory.Build(ProjectDir, ProjectName, PackageServerEndpoint, IsDacpacService);
            pushDacpac.Initialize();
            return pushDacpac.Save();
        }
    }
}
