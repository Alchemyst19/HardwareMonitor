using MissingLinq.Linq2Management.Model.CIMv2;
using System;
using System.Management;

namespace HardwareMonitor
{
    public class Processor
    {
        private string name;
        public string Name { get => name; set => name = value; }
        private string manufacturer;
        public string Manufacturer { get => manufacturer; set => manufacturer = value; }
        private int cores;
        public int Cores { get => cores; set => cores = value; }
        private double clockSpeed;
        public double ClockSpeed { get => clockSpeed; set => clockSpeed = value; }
        private double powerUsage;
        public double PowerUsage { get => powerUsage; set => powerUsage = value; }
        private int thermalPower;
        public int ThermalPower { get => thermalPower; set => thermalPower = value; }

        public Processor()
        {
            Win32Processor win = new Win32Processor();
            Name = from w in win select w.Name;
            

        }


    }
}