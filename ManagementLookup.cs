using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace HardwareMonitor
{
    class ManagementLookup
    {
        public ManagementLookup() { }

        public ManagementObjectCollection Find(string part)
        {
            using (ManagementClass mc = new ManagementClass(part)) { return mc.GetInstances(); }
        }
    }
}
