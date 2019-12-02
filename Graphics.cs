using System;

namespace HardwareMonitor
{
    public class Graphics
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

        public Graphics(string graphicsName) => Name = graphicsName;
    }
}