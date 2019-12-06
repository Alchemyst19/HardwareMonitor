using System;
using System.Text.Json;


namespace HardwareMonitor
{
    static class HardwareMonitorMain
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            HardwareRunner runner = new HardwareRunner();
            runner.Run();
        }
    }
}
