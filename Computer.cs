using System;
using System.Collections.Generic;
using System.Management;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


namespace HardwareMonitor
{
    public class Computer : ComputerComponent
    {        
        private readonly ManagementLookup ManagementLookup = new ManagementLookup();
        [XmlAttribute("ComputerName")]
        public string Name { get => name; set => name = value; }
        public List<Processor> Processors = new List<Processor>();
        public List<Graphic> Graphics = new List<Graphic>();
        public List<Motherboard> Motherboards = new List<Motherboard>();
        public List<Fan> Fans { get; set; }
        [XmlAttribute("TotalThermalPower")]
        public int ThermalPower { get => thermalPower; set => thermalPower = value; }


        public Computer(string n)
        {
            Name = n;
            Console.WriteLine(Name + " created. Detecting parts...");
            AutoFindParts();
            Console.ReadLine();
            Console.Clear();
            Run();
        }
        public Computer()
        {
            Name = "Null";
            AutoFindParts();
            Console.ReadLine();
            Console.Clear();
            Run();
        }
        public Computer(string n, int i) { }

        public override ConsoleKey Run()
        {
            while(input != ConsoleKey.Escape)
            {
                if (input == ConsoleKey.I) { Help(); }
                else if (input == ConsoleKey.L) { List(); }
                else if (input == ConsoleKey.E) { Edit(); }
                else if (input == ConsoleKey.A) { Add(); }
                else if (input == ConsoleKey.D) { Delete(); }
                else if (input == ConsoleKey.B) { break; }
                else { input = Console.ReadKey().Key; }
            }
            return ConsoleKey.I;
        }
        public override void Help()
        {
            Console.WriteLine("Current Level: Computer");
            Console.WriteLine("i - Information. Displays current level, along with currently valid keys");
            Console.WriteLine("l - List. Lists computer name, parts, and thermal power.");
            Console.WriteLine("e - Edit. Allows editing of the Computer or component parts.");
            Console.WriteLine("a - Add. Allows addition of parts.");
            Console.WriteLine("d - Delete. Allows deletion of parts.");
            Console.WriteLine("b - Back. Saves and transfers control up one level (Hardware).");
            Console.WriteLine("Esc - Escape. Ends program.");
            input = Console.ReadKey().Key;
            Console.Clear();
        }
        public override void List()
        {
            Console.WriteLine(Name);
            Console.WriteLine("CPUs:");
            Processors.ForEach(delegate (Processor p) { Console.WriteLine(p.Name); });
            Console.WriteLine("GPUs:");
            Graphics.ForEach(delegate (Graphic g) { Console.WriteLine(g.Name); });
            Console.WriteLine("Motherboards:");
            Motherboards.ForEach(delegate (Motherboard m) { Console.WriteLine(m.Name); });
            Console.WriteLine("Fans:");
            Fans.ForEach(delegate (Fan f) { Console.WriteLine(f.Name); });
            Console.WriteLine("Total Thermal Power: " + ThermalPower);
            input = Console.ReadKey().Key;
            Console.Clear();
        }

        public override void Edit()
        {
            ComputerComponent c;
            Console.WriteLine("Enter the name of the part to be edited, a part keyword to list all parts of that type, or b to go back: ");
            string key = Console.ReadLine();
            if (key.Equals("b"))
            {
                Console.Clear();
                input = Console.ReadKey().Key;
            }
            else if (key.Equals("cpu") || key.Equals("gpu") || key.Equals("motherboard") || key.Equals("fan")) { ListType(key); Edit(); }
            else
            {
                c = Target(key);
                if (c == this)
                {
                    Console.WriteLine("Enter the new string value for name: ");
                    Name = Console.ReadLine();
                }
                else { input = c.Run(); }
            }
        }
        public void Add()
        {
            Console.WriteLine("Enter the part keyword for the part to be added");
            string key = Console.ReadLine();
            if (key.Equals("cpu")) { Processors.Add(new Processor()); }
            else if(key.Equals("gpu")) { Graphics.Add(new Graphic()); }
            else if (key.Equals("motherboard")) { Motherboards.Add(new Motherboard()); }
            else if (key.Equals("fan")) { Fans.Add(new Fan()); }
        }
        public void Delete()
        {
            Console.WriteLine("Enter the name of the part to be deleted, or a keyword to list parts.");
            string key = Console.ReadLine();
            Console.WriteLine("You are about to delete " + key + ". Pleae confirm by pressing D, or press any other key to cancel.");
            input = Console.ReadKey().Key;
            if (input == ConsoleKey.D)
            {
                using (XmlReader reader = XmlReader.Create("data.xml"))
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(reader);
                    XmlNode node = xmldoc.SelectSingleNode("//data/" + Name);
                    node.RemoveChild(xmldoc.SelectSingleNode("//data/" + Name + "/" + key));
                    Console.WriteLine(key + "has been removed.");
                    input = Console.ReadKey().Key;
                    Console.Clear();
                }
            }
        }
        public override void PowerEstimate()
        {
            ThermalPower = 0;
            foreach(Processor p in Processors) { ThermalPower += p.ThermalPower; }
            foreach(Graphic g in Graphics) { ThermalPower += g.ThermalPower; }
            foreach(Motherboard m in Motherboards) { ThermalPower += m.ThermalPower; }
            foreach(Fan f in Fans) { ThermalPower -= f.ThermalPower; }
        }
        public void ThermalRecommendations()
        {
            if (ThermalPower <= 0) { Console.WriteLine("Thermal power is adequate for identified systems."); }
            else if(ThermalPower <= 100) { Console.WriteLine("Thermal power is moderate. Recommend limiting high-stress activities or installing additional cooling."); }
            else if(ThermalPower > 100) { Console.WriteLine("Thermal power at dangerous levels. High-stress activities could cause heat damage to components."); }
            else { Console.WriteLine("Run."); }
        }
        public void AutoFindParts()
        {
            int[] count = new int[] { 0, 0, 0, 0 };
            foreach(ManagementObject mo in ManagementLookup.Find("win32_processor"))
            {
                try { Processors.Add(new Processor(mo)); } catch { }
                count[0]++;
            }
            foreach(ManagementObject mo in ManagementLookup.Find("win32_videocontroller"))
            {
                try { Graphics.Add(new Graphic(mo)); } catch { }
                count[1]++;
            }
            foreach(ManagementObject mo in ManagementLookup.Find("win32_baseboard"))
            {
                try { Motherboards.Add(new Motherboard(mo)); } catch { }
                count[2]++;
            }
            foreach(ManagementObject mo in ManagementLookup.Find("win32_fan"))
            {
                try { Fans.Add(new Fan(mo)); } catch { }
                count[3]++;
            }
            Console.WriteLine("Found " + count[0] + " CPUs, " + count[1] + " GPUs, " + count[2] + " motherboards, and " + count[3] + " fans.");
        }
        public ComputerComponent Target(string name) //Attempts to find a given part based on its name. Probably broken. Don't use.
        {
            if(Name.Equals(name)) { return this; }
            else if (Processors.Contains(new Processor { Name = name })) { return Processors.Find(p => p.Name.Equals(name));  }
            else if (Graphics.Contains(new Graphic { Name = name })) { return Graphics.Find(g => g.Name.Equals(name)); }
            else if (Motherboards.Contains(new Motherboard { Name = name })) { return Motherboards.Find(m => m.Name.Equals(name)); }
            else if (Fans.Contains(new Fan { Name = name })) { return Fans.Find(f => f.Name.Equals(name)); }
            else return null;
        }
        public void ListType(string key)
        {
            if (key.Equals("cpu"))
            {
                Console.WriteLine("CPUs: ");
                foreach (Processor p in Processors)
                {
                    Console.WriteLine(p.Name);
                }
            }
            else if (key.Equals("gpu"))
            {
                Console.WriteLine("GPUs: ");
                foreach (Graphic g in Graphics)
                {
                    Console.WriteLine(g.Name);
                }
            }
            else if (key.Equals("motherboard"))
            {
                Console.WriteLine("Motherboards: ");
                foreach (Motherboard m in Motherboards)
                {
                    Console.WriteLine(m.Name);
                }
            }
            else if (key.Equals("fan"))
            {
                Console.WriteLine("Fans: ");
                foreach (Fan f in Fans)
                {
                    Console.WriteLine(f.Name);
                }
            }
            else Console.WriteLine("Input not recognized");
        }
        public override void ManualInput()
        {
            throw new NotImplementedException();
        }

        public bool Equals(Computer c)
        {
            if (Name.Equals(c.Name)) return true; else return false;
        }
    }
}

