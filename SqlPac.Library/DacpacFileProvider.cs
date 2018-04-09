namespace SqlPac.Library
{
    using System.IO;
    using System.Threading.Tasks;
    using SqlPac.Library.Services;

    internal class DacpacFileProvider : IDacpacService
    {
        private DirectoryInfo storePath;

        public DacpacFileProvider(string packageServerEndpoint)
        {
            storePath = new DirectoryInfo(packageServerEndpoint);
        }

        public byte[] GetDacpac(string id, string version)
        {
            string path = GetPath(id, version);
            CheckFile(path);
            return File.ReadAllBytes(path);
        }

        private void CheckFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(path);
            }
        }

        private string GetPath(string id, string version)
        {
            return $@"{storePath.FullName}\{version}\{id}.dacpac";
        }

        public Task<byte[]> GetDacpacAsync(string id, string version)
        {
            return new TaskFactory<byte[]>().StartNew(
                () =>
                {
                    return GetDacpac(id, version);
                });
        }

        public void SetDacpac(string id, string version, byte[] dacpac)
        {
            string path = GetPath(id, version);
            if (!storePath.Exists)
            {
                throw new DirectoryNotFoundException(storePath.FullName);
            }
            string versionPath = $@"{storePath.FullName}\{version}\";
            if (!Directory.Exists(versionPath))
            {
                Directory.CreateDirectory(versionPath);
            }
            using (var filewriter = File.Open(path, FileMode.Create))
            {
                filewriter.Write(dacpac, 0, dacpac.Length);
            }
        }

        public Task SetDacpacAsync(string id, string version, byte[] dacpac)
        {
            return new TaskFactory().StartNew(
                () =>
                {
                    SetDacpac(id, version, dacpac);
                });
        }
    }
}