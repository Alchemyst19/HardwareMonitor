using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace HardwareMonitor
{
    class Part
    {
        public Part() { }

        public ManagementObjectCollection Find(string part)
        {
            ManagementClass mc = new ManagementClass(part);
            return mc.GetInstances();
        }
    }
}
