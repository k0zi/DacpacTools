namespace SqlPac.Library
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using Microsoft.Build.Evaluation;
    using SqlPac.Library.Services;

    public class PullDacpac
    {
        private IDacpacService client;
        private string packagesPath;
        private string projectName;
        private string projectDir;
        private string packageServerEndpoint;
        private bool isDacpacService;
        private const string SQLPACKAGES = @"..\sqlpackages";

        public packages Packages { get; private set; }
        public bool HasPackages { get { return Packages.Items.Length > 0; } }

        public PullDacpac(string packagesPath, 
            string projectName, 
            string projectDir, 
            string packageServerEndpoint, 
            bool isDacpacService)
        {
            this.packagesPath = packagesPath;
            this.projectName = projectName;
            this.projectDir = projectDir;
            this.packageServerEndpoint = packageServerEndpoint;
            this.isDacpacService = isDacpacService;
        }

        public void Initialize()
        {
            Packages = GetPackages();
            
            if (isDacpacService)
            {
                client = GetClient();
            }
            else
            {
                client = GetDacpacFileProvider();
            }
        }

        private bool CheckProjectFile(packagesPackage[] items)
        {
            try
            {
                var project = new Project($"{projectDir}{projectName}");
                var referencies = project.Items.Where(i => i.ItemType == "ArtifactReference").ToList();
                foreach (var item in items)
                {
                    string path = $@"{SQLPACKAGES}\{item.id}.dacpac";
                    AddReferenceToProject(project, referencies, path);
                }
                project.Save();
            }
            catch (Exception e)
            {
                throw new Exception("Error processing sqlproj file.", e);
            }
            return true;
        }


        private void CreateCheckoutDir()
        {
            if (!Directory.Exists(SQLPACKAGES))
            {
                Directory.CreateDirectory(SQLPACKAGES);
            }
        }

        private void AddReferenceToProject(Project project, List<ProjectItem> referencies, string dacPath)
        {
            string referenceTypeName = "ArtifactReference";
            if (referencies.Any(p => p.EvaluatedInclude == dacPath))
            {
                return;
            }
            var metadata = new KeyValuePair<string, string>[] {
                new KeyValuePair<string, string>("HintPath", dacPath),
                new KeyValuePair<string, string>("SuppressMissingDependenciesErrors", "False")};
            project.AddItem(referenceTypeName, dacPath, metadata);
        }

        public bool Process()
        {
            CreateCheckoutDir();
            DownloadPackages(Packages.Items);
            return CheckProjectFile(Packages.Items);
        }

        private void DownloadPackages(packagesPackage[] items)
        {
            foreach (var item in items)
            {
                var dacpac = client.GetDacpac(item.id, item.version);
                var path = $@"{SQLPACKAGES}\{item.id}.dacpac";
                using (var file = new FileStream(path, FileMode.Create))
                {
                    file.Write(dacpac, 0, dacpac.Length);
                }
            }
        }
        private packages GetPackages()
        {
            if (!File.Exists(packagesPath))
            {
                return null;
            }
            using (var configReader = new StreamReader(packagesPath))
            {
                var serializer = new XmlSerializer(typeof(packages));
                return (serializer.Deserialize(configReader) as packages);
            }
        }


        private IDacpacService GetDacpacFileProvider()
        {
            return new DacpacFileProvider(packageServerEndpoint);
        }

        private IDacpacService GetClient()
        {
            return new DacpacServiceClient(packageServerEndpoint);
        }
    }
}
