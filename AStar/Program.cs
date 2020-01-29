using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AStar
{
    static class Program
    {
        [DllImport("AStarCPP.dll")]
        public static extern void calculateHDistanceCpp(int endX, int endY, int[] hArray);

        [DllImport("AStarASM.dll")]
        public static extern int calculateHDistanceAsm(int endX, int endY, int[] hArray);

        [DllImport("AStarASM.dll")]
        public static extern int SIMDExample(float[] src1, float[] src2, float[] result, int size);

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
