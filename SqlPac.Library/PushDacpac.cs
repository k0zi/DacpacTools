namespace SqlPac.Library
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.Build.Evaluation;
    using SqlPac.Library.Services;

    public class PushDacpac
    {
        private string projectDir;
        private string projectName;
        private string packageServerEndpoint;
        private bool isDacpacService;
        private IDacpacService client;

        private Project project;
        private ProjectProperty name;
        private ProjectProperty version;

        public PushDacpac(string projectDir, string projectName, string packageServerEndpoint, bool isDacpacService)
        {
            this.projectDir = projectDir;
            this.projectName = projectName;
            this.packageServerEndpoint = packageServerEndpoint;
            this.isDacpacService = isDacpacService;
        }

        public void Initialize()
        {
            var globals = new Dictionary<string, string>()
            {
                ["Configuration"] = "Release"
            };
            project = new Project($"{projectDir}{projectName}", globals, "4.0");
            name = project.Properties.FirstOrDefault(p => p.Name == "DacApplicationName")
                ?? project.Properties.FirstOrDefault(p => p.Name == "Name");
            version = project.Properties.FirstOrDefault(p => p.Name == "DacVersion");
            if (isDacpacService)
            {
                client = GetClient();
            }
            else
            {
                client = GetDacpacFileProvider();
            }
        }

        public bool Save()
        {
            string path = GetReleasePath(project);
            var binary = File.ReadAllBytes(path);
            client.SetDacpac(name.EvaluatedValue, version.EvaluatedValue, binary);
            return true;
        }

        private IDacpacService GetDacpacFileProvider()
        {
            return new DacpacFileProvider(packageServerEndpoint);
        }

        private IDacpacService GetClient()
        {
            return new DacpacServiceClient(packageServerEndpoint);
        }

        private string GetReleasePath(Project project)
        {
            var dacPath = project.Properties.FirstOrDefault(p => p.Name == "OutputPath");
            var dacName = project.Properties.FirstOrDefault(p => p.Name == "SqlTargetFile");
            return Path.Combine(projectDir, dacPath.EvaluatedValue, dacName.EvaluatedValue);
        }
    }
}
