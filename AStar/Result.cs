using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AStar
{
    public partial class Result : Form
    {
        private const string dll_name = "cpp.dll";

        [DllImport(dll_name, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getDistance(int device_id);

        List<Point> list = new List<Point>();
        public Result(List<Point> list)
        {
            this.list = list;
            InitializeComponent();
        }

        public void Draw()
        {
            System.Drawing.SolidBrush redBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            System.Drawing.SolidBrush grayBrush = new System.Drawing.SolidBrush(System.Drawing.Color.LightGray);
            System.Drawing.SolidBrush blueBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
            System.Drawing.SolidBrush greenBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Green);
            System.Drawing.SolidBrush blackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);


            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            for(int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    formGraphics.FillRectangle(grayBrush, new Rectangle(2 + x*10, 2 + y*10, 8, 8));
                }
            }
            formGraphics.FillRectangle(blueBrush, new Rectangle(2 + list[0].X * 10, 2 + list[0].Y * 10, 8, 8));
            formGraphics.FillRectangle(greenBrush, new Rectangle(2 + list[1].X * 10, 2 + list[1].Y * 10, 8, 8));
            for (int i = 2; i < list.Count; i++)
            {
                formGraphics.FillRectangle(blackBrush, new Rectangle(2 + list[i].X * 10, 2 + list[i].Y * 10, 8, 8));
            }
            redBrush.Dispose();
            formGraphics.Dispose();
        }
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
