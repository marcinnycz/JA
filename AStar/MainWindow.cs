using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AStar
{
    public partial class MainWindow : Form
    {
        private Result resultForm;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            List<Point> list = new List<Point>();
            using (StreamReader sr = new StreamReader(openFileDialog.FileName))
            {
                // Read the stream to a string, and write the string to the console.
                String line = sr.ReadToEnd();
                String number1;
                String number2;

                while (line.Length > 0)
                {
                    number1 = line.Substring(0, 2);
                    number2 = line.Substring(3, 2);
                    Point p = new Point(Int32.Parse(number1), Int32.Parse(number2));
                    
                    if(line.Length > 6)
                    {
                        line = line.Substring(7, line.Length - 7);
                    }
                    else
                    {
                        line = "";
                    }
                    list.Add(p);
                }
            }
            resultForm = new Result(list);
            resultForm.Show();
            resultForm.Draw(); 
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            testLabel.Text = Program.calculateDistance(5, 2, 3, 7).ToString();
        }
    }
}
