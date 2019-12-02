using MissingLinq.Linq2Management.Model.CIMv2;
using System;
using System.Collections.Generic;


namespace HardwareMonitor
{
    public class HardwareTests
    {
        public void ComputerInitTest() //Verifies that Computer can be instantiated correctly. Note that this does not import data, merely create the Computer object.
        {
            var computer = new Computer("Opal");
            Console.WriteLine("ComputerInitTest: " + computer.Name);
        }
        public void CPUInitTest() //Verifies that the CPU (referred to as Processor) can be instantiated correctly.
        {
            var processor = new Processor();
            Console.WriteLine("CPUInitTest: " + processor.Name);
            Console.WriteLine("CPUValues: " + processor.Cores + processor.Manufacturer + processor.ClockSpeed);
        }
        public void GPUInitTest() //This one's for the Graphics Card/GPU. You know the drill. No data passed other than name.
        {
            var graphics = new Graphics("1060");
            Console.WriteLine("GPUInitTest: " + graphics.Name);
        }
        public void MissingLinqTest()
        {
            var pro = new Win32Processor();
            Console.WriteLine(pro.Name);
        }

        public HardwareTests() { }
    }
}
