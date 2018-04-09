namespace SqlPac
{
    public static class Commands
    {
        public const string GET = "-get";
        public const string SET = "-set";
        public const string PACKAGES_PATH = "-c";
        public const string PROJECT_NAME = "-p";
        public const string PROJECT_DIR = "-d";
        public const string ENDPOINT = "-e";
        public const string IS_SERVICE = "-s";

        public static string[] All = new string[] { GET, SET, PACKAGES_PATH, PROJECT_NAME, PROJECT_DIR, ENDPOINT, IS_SERVICE };
    }
}
