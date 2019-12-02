﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HardwareMonitor;


namespace HardwareMonitor
{
    static class HardwareMonitorMain
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /**Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());**/
            var tester = new HardwareTests();
            tester.ComputerInitTest();
            tester.CPUInitTest();
            tester.GPUInitTest();
            tester.MissingLinqTest();
        }
    }
}
