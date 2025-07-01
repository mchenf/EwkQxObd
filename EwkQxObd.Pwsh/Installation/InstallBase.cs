using System.Management.Automation;
using System.Net.NetworkInformation;

namespace EwkQxObd.Pwsh.Installation
{
    public abstract class InstallBase : Cmdlet
    {
        private const string rootFolder = AppSharedResources.RootFolder;
        private const string appDatabase = AppSharedResources.AppDatabase;

        internal static string localAppDir = string.Empty;
        internal static string appRootDir = string.Empty;
        internal static string appDbDir = string.Empty;


        protected InstallBase()
        {
            localAppDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            appRootDir = Path.Combine(localAppDir, rootFolder);
            appDbDir = Path.Combine(appRootDir, appDatabase);

        }
    }
}
