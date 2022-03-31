namespace Common
{
    using System.Diagnostics;
    using System.Reflection;

    public class ApplicationInfo
    {
        public ApplicationInfo(Type type)
        {
            ExecutingAssembly = Assembly.GetAssembly(type);
        }
        public Assembly ExecutingAssembly { get; private set; }

        public FileVersionInfo ExecutingFileVersionInfo => FileVersionInfo.GetVersionInfo(ExecutingAssembly.Location);
        public string AssemblyVersion => ExecutingAssembly.GetName().Version.ToString();
        public string FileVersion => ExecutingFileVersionInfo.FileVersion;
        public string SemanticVersion => ExecutingAssembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
        public string Description => ExecutingAssembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description;
    }
}