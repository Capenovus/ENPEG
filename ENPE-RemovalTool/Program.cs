using Microsoft.Win32;
using System;


namespace ENPE_RemovalTool
{
#nullable enable
#pragma warning disable CA1416 // Plattformkompatibilität überprüfen
    internal class Program
    {
        private static void Main()
        {
            Init();
        }
        
        private static void Init()
        {
            Console.WriteLine("Please Wait");
            RegistryKey? keyNs =
                RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64)?
                    .OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Desktop\\NameSpace", true);

            string[]? guids = keyNs?.GetSubKeyNames();
            Console.Clear();
            for (var i = 0; i < guids?.Length; i++)
            {
                RegistryKey? key =
                    RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64)?
                        .OpenSubKey($"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Desktop\\NameSpace\\{guids[i]}", true);
                Console.WriteLine($"[{i}] {key?.GetValue("")}");
            }
            Console.WriteLine("\nWhich Entry do you want to delete?");
            while (true)
            {
                var option = Console.ReadLine();
                if (int.TryParse(option, out var id))
                {
                    if (guids?[id] == null) break;
                    Remove(guids[id]);
                    break;
                }
                Console.WriteLine("Please select a Number");
            }
            Main();

        }

        private static void Remove(string guid)
        {
            //Start - Main Path 1
            RegistryKey? key =
                RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64)?
                    .OpenSubKey($"SOFTWARE\\Classes\\CLSID", true);
            try { key?.DeleteSubKeyTree(guid); } catch { }

            //Start - Main Path 2
            RegistryKey? key2 =
                RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64)?
                    .OpenSubKey("Software\\Classes\\Wow6432Node\\CLSID", true);
            try { key2?.DeleteSubKeyTree(guid); } catch { }

            // Start - Misc
            RegistryKey? keyNsp = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64)?
                .OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel", true);
            try { keyNsp?.DeleteValue(guid); } catch { }

            RegistryKey? keyNs =
                RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64)?
                    .OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Desktop\\NameSpace", true);
            try { keyNs?.DeleteSubKeyTree(guid); } catch { }
        }
    }
#pragma warning restore CA1416 // Plattformkompatibilität überprüfen
}
