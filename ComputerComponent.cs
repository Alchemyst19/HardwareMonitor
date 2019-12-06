using System;
using System.Management;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace HardwareMonitor
{
    public abstract class ComputerComponent : IEquatable<object>
    {
        protected ConsoleKey input = ConsoleKey.I;
        protected int thermalPower;
        protected string manufacturer;
        protected string name;

        public abstract ConsoleKey Run();
        public abstract void Help();
        public abstract void List();
        public abstract void Edit();
        public abstract void PowerEstimate();
        public abstract void ManualInput();
    }
}
