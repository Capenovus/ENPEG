using System;
using Microsoft.Win32;

namespace ENPE_RemovalTool
{
    internal class Program
    {
        private static void Main()
        {
            Init();
        }

        private static void Init()
        {
            Console.WriteLine("Please Wait");
            var keyNs =
                RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64)
                    .OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Desktop\\NameSpace", true);
            var guids = keyNs?.GetSubKeyNames();
            Console.Clear();
            for (var i = 0; i < guids?.Length; i++)
            {
                var key =
                    RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64)
                        .OpenSubKey($"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Desktop\\NameSpace\\{guids[i]}", true);
                Console.WriteLine($"[{i}] {key?.GetValue("")}");
            }
            Console.WriteLine("\nWhich Entry do you want to delete?");
            while (true)
            {
                var option = Console.ReadLine();
                if (int.TryParse(option, out var id))
                {
                    Remove(guids?[id]);
                    break;
                }
                Console.WriteLine("Please select a Number");
            }
            Main();

        }

        private static void Remove(string guid)
        {
            //Start - Main Path 1
            var key =
                RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64)
                    .OpenSubKey($"SOFTWARE\\Classes\\CLSID", true);
            key?.DeleteSubKeyTree(guid);

            //Start - Main Path 2
            var key2 =
                RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64)
                    .OpenSubKey("Software\\Classes\\Wow6432Node\\CLSID", true);
            key2?.DeleteSubKeyTree(guid);

            // Start - Misc
            var keyNsp = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64)
                .OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel", true);
            keyNsp?.DeleteValue(guid);

            var keyNs =
                RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64)
                    .OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Desktop\\NameSpace", true);
            keyNs?.DeleteSubKeyTree(guid);
        }
    }
}
