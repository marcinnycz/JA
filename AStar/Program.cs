using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AStar
{
    static class Program
    {
        private const string dll_name = "AStarASM.dll";

        /*[DllImport(dll_name, CallingConvention = CallingConvention.Cdecl)]
        public static extern int calculateDistance(int x1, int x2, int y1, int y2);
        */

        [DllImport(dll_name, CallingConvention = CallingConvention.Cdecl)]
        public static extern int calculateDistance(int x1, int x2, int y1, int y2);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
