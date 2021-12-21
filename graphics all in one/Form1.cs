using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphics_all_in_one
{
    public partial class Form1 : Form
    {
        private string cur = "";
        private string LINE_DDA = "Line Using DDA";
        private string LINE_BRESNHAM = "Line Using Presenhame";
        private string CIRCLE_MID_POINT = "Circle (Mid Point)";
        public Form1()
        {
            InitializeComponent();
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            cur = comboBox1.Text;
            if (cur == LINE_DDA)
            {
                Line dda = new Line(pictureBox1.Width, pictureBox1.Height);
                try
                {
                    dda.initialize("Drawing Line Using DDA Algorithm");
                    pictureBox1.Image = dda.drawUsingDDA();

                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
                
            }
            else if (cur == LINE_BRESNHAM)
            {
                try
                {
                    Line bres = new Line(pictureBox1.Width, pictureBox1.Height);
                    bres.initialize("Drawing Line Using Bresenham Algorithm");
                    pictureBox1.Image = bres.drawUsingBresenham();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
            else if (cur == CIRCLE_MID_POINT)
            {
                Circle c = new Circle(pictureBox1.Width, pictureBox1.Height);
                c.initialize();
                pictureBox1.Image = c.draw();
            }
        }

		private void Form1_Load(object sender, EventArgs e)
		{

		}
	}
}
