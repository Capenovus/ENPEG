using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ENPEG
{
    public class RegeditGen
    {
        public void Create(string name, string path, string iconpath)
        {
            GUIDgen gen = new GUIDgen();
            var guid = gen.Generate();
            var mpath2 = $"HKEY_CURRENT_USER\\Software\\Classes\\Wow6432Node\\CLSID\\{guid}\\";
            const string nsp = "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel";
            var ns = $"HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Desktop\\NameSpace\\{guid}";
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
            keyInstanceProptertyBag?.SetValue("Attrivutes", "17", RegistryValueKind.DWord);
            keyInstanceProptertyBag?.SetValue("TargetFolderPath", path);


            //Start - Main Path 2
            Registry.SetValue(mpath2, "", name);
            Registry.SetValue(mpath2, "System.IsPinnedToNamespaceTree", "1", RegistryValueKind.DWord);
            Registry.SetValue(mpath2, "SortOrderIndex", "66", RegistryValueKind.DWord);

            Registry.SetValue($"{mpath2}InProcServer32", "", shellpath, RegistryValueKind.ExpandString);

            Registry.SetValue($"{mpath2}ShellFolder", "FolderValueFlags", "40", RegistryValueKind.DWord);
            Registry.SetValue($"{mpath2}ShellFolder", "Attributes", unchecked((int)4034920525), RegistryValueKind.DWord);

            Registry.SetValue($"{mpath2}Instance", "CLSID", clsid);
            Registry.SetValue($"{mpath2}Instance\\InitPropertyBag", "Attributes", "17", RegistryValueKind.DWord);
            Registry.SetValue($"{mpath2}Instance\\InitPropertyBag", "TargetFolderPath", path);
            
            Registry.SetValue($"{mpath2}DefaultIcon", "", iconpath);


            // Start - Misc
            Registry.SetValue(nsp, guid, "1", RegistryValueKind.DWord);
            Registry.SetValue(ns, "", name);
            

            MessageBox.Show("Done");

        }
    }
}
