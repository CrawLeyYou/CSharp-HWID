using System;
using System.Windows.Forms;
using System.Text;
using System.Management;

namespace HWID
{
    class Class1
    {
        [STAThread]
        static void Main(string[] args)
        {
            var Processor = HWID("win32_processor", "processorID");
            var GPU = HWID("win32_videocontroller", "driverversion");
            var HDD = HWID("win32_diskdrive", "serialnumber");
            var MotherBoard = HWID("win32_baseboard", "version");
            var HardwareID = Processor + "!" + GPU + "!" + HDD + "!" + MotherBoard;
            byte[] ASCII = ASCIIEncoding.ASCII.GetBytes(HardwareID);
            string Base64 = Convert.ToBase64String(ASCII);
            Clipboard.SetText(Base64);
            MessageBox.Show("Your HWID is copied into your clipboard!");
        }

        private static string HWID(string Select, string ID)
        {
            ManagementClass Hardware = new ManagementClass(Select);
            ManagementObjectCollection HardwareInfo = Hardware.GetInstances();
            String HWIDa = null;
            foreach (ManagementObject HWID in HardwareInfo)
            {
                HWIDa = HWID.Properties[ID].Value.ToString();
                break;
            }
            return HWIDa;
        }
    }
}
