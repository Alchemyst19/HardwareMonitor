using System;
using System.Management;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace HardwareMonitor
{
    public class Graphics : ComputerComponent
    {
        private string[] fullName = new string[6];
        public string[] FullName { get => fullName; set => fullName = value; }
        public string Name { get => name; set => name = value; }
        public string Manufacturer { get => manufacturer; set => manufacturer = value; }
        private string ram;
        public string Ram { get => ram; set => ram = value; }
        public int ThermalPower { get => thermalPower; set => thermalPower = value; }

        public Graphics()
        {
            ReadGraphicsInfo();
            PowerEstimate();
        }

        public void ReadGraphicsInfo() //This method reads the physical gpu's properties, as recorded by WMI's win32_videocontroller class.
        {
            ManagementClass mc = new ManagementClass("win32_videocontroller");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                Name = mo.Properties["Name"].Value.ToString();
                mo.Properties["Name"].Value.ToString().Split(' ').CopyTo(FullName, 0);
                Manufacturer = FullName[0];
                Ram = Math.Round(Convert.ToDouble(mo.Properties["AdapterRAM"].Value) / 1000000000).ToString() + "GB";
                if (!(mo.Properties["Name"].Value.ToString().Contains("Intel")))
                {
                    break;
                }
            }
        }

        public void PowerEstimate()
        {
            Console.WriteLine("Please input GPU TDP (" + Name + "):");
            ThermalPower = Convert.ToInt32(Console.ReadLine());
        }
        public void ManualInput()
        {
            Console.WriteLine("Please input GPU name:");
            Name = Console.ReadLine();
            Console.WriteLine("Please input GPU Manufacturer:");
            Manufacturer = Console.ReadLine();
            Console.WriteLine("Please input GPU TDP:");
            ThermalPower = Convert.ToInt32(Console.ReadLine());
        }
        public void List()
        {
            Console.WriteLine("Graphics: " + Name);
            Console.WriteLine("Graphics TDP: " + ThermalPower);
        }
    }
}