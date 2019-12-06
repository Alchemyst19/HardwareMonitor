using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

namespace HardwareMonitor
{
    class HardwareRunner
    {
        [XmlArray("ComputerList"), XmlArrayItem(typeof(Computer))]
        public List<Computer> Master = new List<Computer>();
        private ConsoleKey input = ConsoleKey.I;
        public Computer computer;
        public void Run()
        {
            Load();
            while (input != ConsoleKey.Escape)
            {
                Console.WriteLine();
                if (input == ConsoleKey.I) { Help(); }
                else if (input == ConsoleKey.N) { Create(); }
                else if (input == ConsoleKey.E) { Edit(); }
                else { input = Console.ReadKey().Key; }
            }
            Close();
            return;
        }
        public void Test()
        {
            var tester = new HardwareTests();
            tester.ProgramTest();
        }
        public void Help()
        {
            Console.WriteLine("Current Level: HardwareMonitor");
            Console.WriteLine("i - Information. Displays current level, along with currently valid keys");
            Console.WriteLine("n - New System. Creates a new instance of Computer and transfers control down a level.");
            Console.WriteLine("e - Existing System. Allows editing or deletion of saved instances.");
            Console.WriteLine("Esc - Escape. Ends program.");
            Console.WriteLine("Press a key to continue... ");
            input = Console.ReadKey().Key;
        }
        public void Create()
        {
            Console.WriteLine("Enter the name of the new Computer");
            string key = Console.ReadLine();
            Computer computer = new Computer(key);
            input = computer.Run();
            Save(computer);
        }
        public void Edit()
        {
            XmlSerializer serial = new XmlSerializer(typeof(List<Computer>));
            Console.WriteLine("Enter the name of the saved Computer: ");
            string key = Console.ReadLine();
            Console.WriteLine("Press e to edit this system, or d to delete it.");
            input = Console.ReadKey().Key;
            if (input == ConsoleKey.E)
            {
                computer = Master.Find(x => x.Name.Equals(key));
                input = computer.Run();
                Save(computer);
            }
            else if (input == ConsoleKey.D)
            {
                Console.WriteLine("You are about to delete " + key + ". Pleae confirm by pressing D, or press any other key to cancel.");
                input = Console.ReadKey().Key;
                if (input == ConsoleKey.D)
                {
                    XmlReader reader = XmlReader.Create("data.xml");
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(reader);
                    XmlNode node = xmldoc.SelectSingleNode("//data");
                    node.RemoveChild(xmldoc.SelectSingleNode("//data/" + key));
                    Console.WriteLine(key + " has been removed.");
                }
                input = Console.ReadKey().Key;
            }
        }
        public void Load()
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "HardwareMonitor";
            xRoot.IsNullable = true;
            XmlSerializer serial = new XmlSerializer(typeof(List<Computer>), xRoot);
            using (XmlReader reader = XmlReader.Create("data.xml"))
            {
                var other = (List<Computer>)serial.Deserialize(reader);
                Master.Clear();
                Master.AddRange(other);
            }
        }
        public void Save(Computer c)
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "HardwareMonitor";
            xRoot.IsNullable = true;
            XmlSerializer serial = new XmlSerializer(typeof(List<Computer>), xRoot);
            if (!(Master.Contains(new Computer(c.Name, 0)))) { Master.Add(c); }
            input = ConsoleKey.I;
        }
        public void Close()
        {
            XmlSerializer serial = new XmlSerializer(typeof(List<Computer>));
            using (XmlWriter writer = XmlWriter.Create("data.xml")) { serial.Serialize(writer, Master); writer.Close(); }
        }
    }
}
