using System;


namespace HardwareMonitor
{
    public class Computer
    {
        private string systemName;
        public string Name { get => systemName; set => systemName = value; }
        private Processor processor;
        public Processor GetProcessor { get => processor; set => processor = value; }
        private Graphics graphics;
        public Graphics GetGraphics { get => graphics; set => graphics = value; }
        private string fanSetup;
        public string FanSetup { get => fanSetup; set => fanSetup = value; }
        private int thermalPower;
        public int ThermalPower { get => thermalPower; set => thermalPower = value; }

        public Computer(string SystemName) => Name = SystemName;
    }
}

