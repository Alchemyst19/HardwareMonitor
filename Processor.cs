using System;
using System.Management;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace HardwareMonitor
{
    public class Processor : ComputerComponent
    {
        [XmlAttribute("CPUFullName")]
        private string[] fullName = new string[6];
        public string[] FullName { get => fullName; set => fullName = value; }
        [XmlAttribute("CPUName")]
        public string Name { get => name; set => name = value; }
        [XmlAttribute("CPUManu")]
        public string Manufacturer { get => manufacturer; set => manufacturer = value; }
        private int cores;
        public int Cores { get => cores; set => cores = value; }
        private string clockSpeed;
        public string ClockSpeed { get => clockSpeed; set => clockSpeed = value; }
        [XmlAttribute("CPUThermal")]
        public int ThermalPower { get => thermalPower; set => thermalPower = value; }

        public Processor()
        {
            ManualInput();
        }
        public Processor(ManagementObject mo)
        {
            mo.Properties["Name"].Value.ToString().Split(' ').CopyTo(fullName, 0);
            Name = FullName[2];
            Manufacturer = FullName[0].Split('(')[0];
            Cores = Convert.ToInt32(mo.Properties["NumberOfCores"].Value);
            ClockSpeed = FullName[5];
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
            Console.WriteLine("Current Level: CPU");
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
            Console.WriteLine("CPU: " + Name);
            Console.WriteLine("Full Name: " + FullName.ToString());
            Console.WriteLine("Manufacturer: " + Manufacturer);
            Console.WriteLine("Cores: " + Cores);
            Console.WriteLine("Clock Speed: " + ClockSpeed);
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
            else if (key.Equals("l")) { Console.WriteLine("CPU Property Keywords: name, manufacturer, cores, clock, thermal"); }
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
            else if (key.Equals("cores"))
            {
                Console.WriteLine("Enter the new integer value for cores: ");
                Cores = Convert.ToInt32(Console.ReadLine());
            }
            else if (key.Equals("clock"))
            {
                Console.WriteLine("Enter the new string value for clock speed: ");
                ClockSpeed = Console.ReadLine();
            }
            else if (key.Equals("thermal"))
            {
                Console.WriteLine("Enter the new integer value for thermal power: ");
                ThermalPower = Convert.ToInt32(Console.ReadLine());
            }
            else
            {
                Console.Write("Input not recognized.");
                Edit();
            }
        }
        public override void PowerEstimate() //Definitely a bit of brute-force going on here, but I couldn't find an accessable database, and it's too close to the deadline to create one.
        {
            string[] Model = Name.Split('-');
            if (Model[0].Equals("i5")) //All values are rough estimates, for lack of a more detailed data set to pull from.
            {
                if (Model[1].Contains("K"))
                {
                    ThermalPower = 88;
                }
                else
                {
                    ThermalPower = 65;
                }
            }
            else if (Model[0].Equals("i7"))
            {
                if (Model[1].Contains("T"))
                {
                    ThermalPower = 35;
                }
                else if (Model[1].Contains("K"))
                {
                    ThermalPower = 91;
                }
                else if (Model[1].Contains("X"))
                {
                    ThermalPower = 140;
                }
                else
                {
                    ThermalPower = 65;
                }
            }
            else if (Model[0].Equals("i9"))
            {
                ThermalPower = 140;
            }
            else
            {
                Console.WriteLine("CPU Name returned in unrecognized format. HardwareMonitor currently only supports Intel processors automatically. Please input data manually.");
                ManualInput();
            }
        }

        public override void ManualInput()
        {
            Console.WriteLine("Please input CPU name");
            Name = Console.ReadLine();
            FullName[0] = Name;
            Console.WriteLine("Please input CPU Manufacturer");
            Manufacturer = Console.ReadLine();
            Console.WriteLine("Please input CPU Clock Speed");
            ClockSpeed = Console.ReadLine();
            Console.WriteLine("Please input CPU TDP");
            ThermalPower = Convert.ToInt32(Console.ReadLine());
        }

        public override bool Equals(object o)
        {
            if(o == null)
            {
                return false;
            }
            Processor p = o as Processor;
            if (p == null)
            {
                return false;
            }
            else return Equals(p);
        }
        public bool Equals(Processor p)
        {
            if(Name == p.Name)
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