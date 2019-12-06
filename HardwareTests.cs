using System;


namespace HardwareMonitor
{
    public class HardwareTests
    {
        public void ProgramTest()
        {
            ComputerInitTest();
            CPUInitTest();
            GPUInitTest();
            MotherboardTest();
            FanTest();
        }
        public void ComputerInitTest() //Verifies that Computer can be instantiated correctly. Note that this does not import data, merely create the Computer object.
        {
            var computer = new Computer("Test1");
            Console.WriteLine("ComputerInitTest: " + computer.Name);
        }
        public void CPUInitTest() //Verifies that the CPU (referred to as Processor) can be instantiated correctly.
        {
            var processor = new Processor();
            Console.WriteLine("CPUInitTest: " + processor.Name);
            Console.WriteLine("CPUValues: " + processor.Cores + " " + processor.Manufacturer + " " + processor.ClockSpeed + " " + processor.ThermalPower);
                 
        }
        public void GPUInitTest() //This one's for the Graphics Card/GPU. You know the drill. No data passed other than name.
        {
            var graphics = new Graphic();
            Console.WriteLine("GPUInitTest: " + graphics.Name);
            Console.WriteLine("GPUValues: " + graphics.Ram + " " + graphics.Manufacturer + " " + graphics.ThermalPower);
        }
        public void MotherboardTest() //Motherboard
        {
            var motherboard = new Motherboard();
            Console.WriteLine("MBInitTest: " + motherboard.Name);
            Console.WriteLine("MBValues: " + motherboard.Manufacturer + " " + motherboard.ThermalPower);
        }
        public void FanTest()
        {
            var fan = new Fan();
            Console.WriteLine("FanInitTest: " + fan.Name);
            Console.WriteLine("FanValue: " + fan.ThermalPower);
        }
    }
}
