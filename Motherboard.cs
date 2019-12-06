using System;
using System.Management;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace HardwareMonitor
{
    public class Motherboard : ComputerComponent
    {
        public string Name { get => name; set => name = value; }
        public string Manufacturer { get => manufacturer; set => manufacturer = value; }
        public int ThermalPower { get => thermalPower; set => thermalPower = value; }

        public Motherboard()
        {
            ManualInput();
        }
        public Motherboard(ManagementObject mo)
        {
            ReadName(mo);
            Manufacturer = mo.Properties["Manufacturer"].Value.ToString();
            if (Manufacturer.Length > 1)
            {
                Manufacturer = "Unknown";
            }
            PowerEstimate();
        }

        public override ConsoleKey Run()
        {
            Console.Clear();
            while (input != ConsoleKey.Escape)
            {
                if (input == ConsoleKey.I) { Help(); }
                else if (input == ConsoleKey.L) { List(); }
                else if (input == ConsoleKey.E) { Edit(); }
                else if (input == ConsoleKey.B) { return ConsoleKey.L; }
                else { input = Console.ReadKey().Key; }
            }
            return input;
        }
        public override void Help()
        {
            Console.WriteLine("Current Level: Motherboard");
            Console.WriteLine("i - Information. Displays current level, along with currently valid keys");
            Console.WriteLine("l - List. Lists part name and properties.");
            Console.WriteLine("e - Edit. Allows editing of this part.");
            Console.WriteLine("b - Back. Transfer control up one level (Computer).");
            Console.WriteLine("Esc - Escape. Ends program.");
            input = Console.ReadKey().Key;
            Console.Clear();
        }
        public override void List()
        {
            Console.WriteLine("Motherboard: " + Name);
            Console.WriteLine("Manufacturer: " + Manufacturer);
            Console.WriteLine("Thermal Power: " + ThermalPower);
            input = Console.ReadKey().Key;
            Console.Clear();
        }
        public override void Edit()
        {
            Console.WriteLine("Enter the name of the property to be edited, l to list property names, or b to go back.");
            string key = Console.ReadLine();
            if (key.Equals("b"))
            {
                Console.Clear();
                input = Console.ReadKey().Key;
            }
            else if (key.Equals("l")) { Console.WriteLine("Motherboard Property Keywords: name, manufacturer, thermal"); }
            else if (key.Equals("name"))
            {
                Console.WriteLine("Enter the new string value for name: ");
                Name = Console.ReadLine();
            }
            else if (key.Equals("manufacturer"))
            {
                Console.WriteLine("Enter the new string value for manufacturer: ");
                Manufacturer = Console.ReadLine();
            }
            else if (key.Equals("thermal"))
            {
                PowerEstimate();
            }
            else
            {
                Console.Write("Input not recognized.");
                Edit();
            }
        }
        public override void PowerEstimate()
        {
            Console.WriteLine("Please input Motherboard TDP (" + Name + "):");
            Console.WriteLine("Note: Input 50 for laptops, 100 for mini-ATX, 150 for full ATX.");
            ThermalPower = Convert.ToInt32(Console.ReadLine());
        }
        public override void ManualInput()
        {
            Console.WriteLine("Please input Motherboard name:");
            Name = Console.ReadLine();
            Console.WriteLine("Please input Motherboard Manufacturer:");
            Manufacturer = Console.ReadLine();
            Console.WriteLine("Please input Motherboard TDP:");
            ThermalPower = Convert.ToInt32(Console.ReadLine());
        }

        public void ReadName(ManagementObject mo) //Most win32_baseboard values are optional on the manufacturer side, so try we must.
        {
            try
            {
                Name = mo.Properties["Model"].Value.ToString();
            }
            catch { }
            try
            {
                Name = mo.Properties["PartNumber"].Value.ToString();
            }
            catch { }
            try
            {
                Name = mo.Properties["Product"].Value.ToString();
            }
            catch { }
            try
            {
                Name = mo.Properties["SerialNumber"].Value.ToString();
            }
            catch { }
        }
        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }
            Motherboard p = o as Motherboard;
            if (p == null)
            {
                return false;
            }
            else return Equals(p);
        }
        public bool Equals(Motherboard p)
        {
            if (Name == p.Name)
            {
                return true;
            }
            else return false;
        }
        public override int GetHashCode()
        {
            return ThermalPower;
        }
    }
}
