using Microsoft.Win32;
using System.Windows.Forms;

namespace ENPEG
{
    public class RegeditGen
    {
        public void Create(string name, string path, string iconpath)
        {
            GUIDgen gen = new GUIDgen();
            var guid = gen.Generate();
            const string clsid = "{0E5AAE11-A475-4c5b-AB00-C66DE400274E}";
            const string shellpath = "%SYSTEMROOT%\\SysWow64\\shell32.dll";

            //Start - Main Path 1
            var key =
                RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64)
                    .OpenSubKey("SOFTWARE\\Classes\\CLSID", true)
                    ?.CreateSubKey(guid, RegistryKeyPermissionCheck.ReadWriteSubTree);
            key?.SetValue("", name);
            key?.SetValue("System.IsPinnedToNamespaceTree", "1", RegistryValueKind.DWord);
            key?.SetValue("SortOrderIndex", "66", RegistryValueKind.DWord);

            var keyInProcServer32 = key?.CreateSubKey("InProcServer32", RegistryKeyPermissionCheck.ReadWriteSubTree);
            keyInProcServer32?.SetValue("", shellpath, RegistryValueKind.ExpandString);

            var keyShellFolder = key?.CreateSubKey("ShellFolder", RegistryKeyPermissionCheck.ReadWriteSubTree);
            keyShellFolder?.SetValue("FolderValueFlags", "40", RegistryValueKind.DWord);
            keyShellFolder?.SetValue("Attributes", unchecked((int)4034920525), RegistryValueKind.DWord);

            var keyInstance = key?.CreateSubKey("Instance", RegistryKeyPermissionCheck.ReadWriteSubTree);
            keyInstance?.SetValue("CLSID", clsid);

            var keyIcon = key?.CreateSubKey("DefaultIcon", RegistryKeyPermissionCheck.ReadWriteSubTree);
            keyIcon?.SetValue("", iconpath);

            var keyInstanceProptertyBag = keyInstance?.CreateSubKey("InitPropertyBag", RegistryKeyPermissionCheck.ReadWriteSubTree);
            keyInstanceProptertyBag?.SetValue("Attributes", "17", RegistryValueKind.DWord);
            keyInstanceProptertyBag?.SetValue("TargetFolderPath", path);

            //Start - Main Path 2
            var key_2 =
                RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64)
                    .OpenSubKey("Software\\Classes\\Wow6432Node\\CLSID", true)
                    ?.CreateSubKey(guid, RegistryKeyPermissionCheck.ReadWriteSubTree);
            key_2?.SetValue("", name);
            key_2?.SetValue("System.IsPinnedToNamespaceTree", "1", RegistryValueKind.DWord);
            key_2?.SetValue("SortOrderIndex", "66", RegistryValueKind.DWord);

            var keyInProcServer32_2 = key_2?.CreateSubKey("InProcServer32", RegistryKeyPermissionCheck.ReadWriteSubTree);
            keyInProcServer32_2?.SetValue("", shellpath, RegistryValueKind.ExpandString);

            var keyShellFolder_2 = key_2?.CreateSubKey("ShellFolder", RegistryKeyPermissionCheck.ReadWriteSubTree);
            keyShellFolder_2?.SetValue("FolderValueFlags", "40", RegistryValueKind.DWord);
            keyShellFolder_2?.SetValue("Attributes", unchecked((int)4034920525), RegistryValueKind.DWord);

            var keyInstance_2 = key_2?.CreateSubKey("Instance", RegistryKeyPermissionCheck.ReadWriteSubTree);
            keyInstance_2?.SetValue("CLSID", clsid);

            var keyIcon_2 = key_2?.CreateSubKey("DefaultIcon", RegistryKeyPermissionCheck.ReadWriteSubTree);
            keyIcon_2?.SetValue("", iconpath);

            var keyInstanceProptertyBag_2 = keyInstance_2?.CreateSubKey("InitPropertyBag", RegistryKeyPermissionCheck.ReadWriteSubTree);
            keyInstanceProptertyBag_2?.SetValue("Attributes", "17", RegistryValueKind.DWord);
            keyInstanceProptertyBag_2?.SetValue("TargetFolderPath", path);

            // Start - Misc
            var keyNsp = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64)
                .OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel", true);
            keyNsp?.SetValue(guid, "1", RegistryValueKind.DWord);

            var keyNs =
                RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64)
                    .OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Desktop\\NameSpace", true)
                    ?.CreateSubKey(guid, RegistryKeyPermissionCheck.ReadWriteSubTree);
            keyNs?.SetValue("", name);

            MessageBox.Show("Done");
        }
    }
}