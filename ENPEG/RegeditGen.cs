using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace ENPEG
{
    public class RegeditGen
    {
        public void Create(string name, string path, string iconpath)
        {
            GUIDgen gen = new GUIDgen();
            var guid = gen.Generate();

            var regcontent = "Windows Registry Editor Version 5.00\n\n" +
                             $"[HKEY_CURRENT_USER\\Software\\Classes\\Wow6432Node\\CLSID\\{guid}]\n" +
                             $"@=\"{name}\"\n" +
                             $"\"System.IsPinnedToNamespaceTree\"=dword:00000001\n" +
                             $"\"SortOrderIndex\"=dword:00000042\n\n" +
                             $"[HKEY_CURRENT_USER\\Software\\Classes\\Wow6432Node\\CLSID\\{guid}\\InProcServer32]\n" +
                             $"@=hex(2):25,00,53,00,59,00,53,00,54,00,45,00,4D,00,52,00,4F,00,4F,00,54,00,\\\n" +
                             $"25,00,5C,00,73,00,79,00,73,00,74,00,65,00,6D,00,33,00,32,00,5C,00,73,00,68,\\\n" +
                             $"00,65,00,6C,00,6C,00,33,00,32,00,2E,00,64,00,6C,00,6C,00,00,00\n\n" +
                             $"[HKEY_CURRENT_USER\\Software\\Classes\\Wow6432Node\\CLSID\\{guid}\\ShellFolder]\n" +
                             $"\"FolderValueFlags\"=dword:00000028\n" +
                             $"\"Attributes\"=dword:f080004d\n\n" +
                             $"[HKEY_CURRENT_USER\\Software\\Classes\\CLSID\\{guid}]\n" +
                             $"@=\"{name}\"\n" +
                             $"\"System.IsPinnedToNamespaceTree\"=dword:00000001\n" +
                             $"\"SortOrderIndex\"=dword:00000042\n\n" +
                             $"[HKEY_CURRENT_USER\\Software\\Classes\\CLSID\\{guid}\\InProcServer32]\n" +
                             $"@=hex(2):25,00,53,00,59,00,53,00,54,00,45,00,4D,00,52,00,4F,00,4F,00,54,00,\\\n" +
                             $"25,00,5C,00,73,00,79,00,73,00,74,00,65,00,6D,00,33,00,32,00,5C,00,73,00,68,\\\n" +
                             $"00,65,00,6C,00,6C,00,33,00,32,00,2E,00,64,00,6C,00,6C,00,00,00\n\n" +
                             $"[HKEY_CURRENT_USER\\Software\\Classes\\CLSID\\{guid}\\ShellFolder]\n" +
                             $"\"FolderValueFlags\"=dword:00000028\n" +
                             $"\"Attributes\"=dword:f080004d\r\n\n" +
                             $"[HKEY_CURRENT_USER\\Software\\Classes\\Wow6432Node\\CLSID\\{guid}\\DefaultIcon]\n" +
                             $"@=\"{iconpath}\"\n\n" +
                             $"[HKEY_CURRENT_USER\\Software\\Classes\\Wow6432Node\\CLSID\\{guid}\\Instance]\n" +
                             $"\"CLSID\"=\"{{0E5AAE11-A475-4c5b-AB00-C66DE400274E}}\"\n\n" +
                             $"[HKEY_CURRENT_USER\\Software\\Classes\\Wow6432Node\\CLSID\\{guid}\\Instance\\InitPropertyBag]\n" +
                             $"\"Attributes\"=dword:00000011\n" +
                             $"\"TargetFolderPath\"=\"{path}\"\n\n" +
                             $"[HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel]\n" +
                             $"\"{guid}\"=dword:00000001\n\n" +
                             $"[HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Desktop\\NameSpace\\{guid}]\n" +
                             $"@=\"{name}\"\n\n" +
                             $"[HKEY_CURRENT_USER\\Software\\Classes\\CLSID\\{guid}\\DefaultIcon]\n" +
                             $"@=\"{iconpath}\"\n\n" +
                             $"[HKEY_CURRENT_USER\\Software\\Classes\\CLSID\\{guid}\\Instance]\n" +
                             $"\"CLSID\"=\"{{0E5AAE11-A475-4c5b-AB00-C66DE400274E}}\"\n\n" +
                             $"[HKEY_CURRENT_USER\\Software\\Classes\\CLSID\\{guid}\\Instance\\InitPropertyBag]\n" +
                             $"\"Attributes\"=dword:00000011\n" +
                             $"\"TargetFolderPath\"=\"{path}\"";

            File.WriteAllBytes("key.reg", Encoding.ASCII.GetBytes(regcontent));

            DialogResult result = MessageBox.Show("key.reg file has been generated. Import now?", "Success!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Process regeditProcess = Process.Start("regedit.exe", "/s key.reg");
                regeditProcess.WaitForExit();
                MessageBox.Show("Import Successful! You might have to restart your PC", "Success!", MessageBoxButtons.OK);
            }

        }
    }
}
