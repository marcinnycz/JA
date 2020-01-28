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

        List<List<Point>> list = new List<List<Point>>();
        String time;
        public Result(List<List<Point>> list, String time)
        {
            this.list = list;
            this.time = time;
            InitializeComponent();
            numericUpDown.Maximum = list.Count;
        }

        public void Draw()
        {
            timeLabel.Text = time;
            List<Point> li = list[Convert.ToInt32(Math.Round(numericUpDown.Value, 0))-1];
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
            formGraphics.FillRectangle(blueBrush, new Rectangle(2 + li[0].Y * 10, 2 + li[0].X * 10, 8, 8));
            formGraphics.FillRectangle(redBrush, new Rectangle(2 + li[1].Y * 10, 2 + li[1].X * 10, 8, 8));
            
            int i = 2;
            while(!li[i].Equals(new Point(-1,-1)))
            {
                formGraphics.FillRectangle(blackBrush, new Rectangle(2 + li[i].Y * 10, 2 + li[i].X * 10, 8, 8));
                i++;
            }
            i++;
            while (i != li.Count)
            {
                formGraphics.FillRectangle(greenBrush, new Rectangle(2 + li[i].Y * 10, 2 + li[i].X * 10, 8, 8));
                i++;
            }
            greenBrush.Dispose();
            blackBrush.Dispose();
            grayBrush.Dispose();
            blueBrush.Dispose();
            redBrush.Dispose();
            formGraphics.Dispose();
        }
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.Draw();
        }
    }
}
