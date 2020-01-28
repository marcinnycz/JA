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
        /*[DllImport("AStarASM.dll")]
        public static extern int calculateDistanceAsm(int x1, int x2, int y1, int y2);

        [DllImport("AStarCPP.dll")]
        public static extern int calculateDistanceCpp(int x1, int x2, int y1, int y2);*/

        [DllImport("AStarCPP.dll")]
        public static extern void calculateHDistanceCpp(int endX, int endY, int[] hArray);

        [DllImport("AStarASM.dll")]
        public static extern int calculateHDistanceAsm(int endX, int endY, int[] hArray);
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
