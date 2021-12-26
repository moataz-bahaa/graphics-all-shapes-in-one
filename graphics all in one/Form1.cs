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
        private string ELLIPSE_MID_POINT = "Ellipse (MId Point)";
        private Circle c;
        private ellipse elip;
        private Line line;
        public Form1()
        {
            InitializeComponent();
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            c = new Circle(pictureBox1.Width, pictureBox1.Height);
            elip = new ellipse(pictureBox1.Width, pictureBox1.Height);
            line = new Line(pictureBox1.Width, pictureBox1.Height);
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            cur = comboBox1.Text;
            if (cur == LINE_DDA)
            {
                try
                {
                    line.initialize("Drawing Line Using DDA Algorithm");
                    pictureBox1.Image = line.drawUsingDDA();

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
                    line.initialize("Drawing Line Using Bresenham Algorithm");
                    pictureBox1.Image = line.drawUsingBresenham();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
            else if (cur == CIRCLE_MID_POINT)
            {
                try
                {
                    if (c.initialize())
                        pictureBox1.Image = c.draw();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
            else if (cur == ELLIPSE_MID_POINT)
            {
                try
                {
                    if (elip.initialize())
                        pictureBox1.Image = elip.draw();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }

		private void Form1_Load(object sender, EventArgs e)
		{

		}

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void test_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int x = int.Parse(translateX.Text), y = int.Parse(translateY.Text);
            if (cur == CIRCLE_MID_POINT)
            {
                pictureBox1.Image = c.translate(x, y);
            }
            else if (cur == ELLIPSE_MID_POINT)
            {
                pictureBox1.Image = elip.translate(x, y);
            }
            else if (cur == LINE_DDA || cur == LINE_BRESNHAM)
            {
                pictureBox1.Image = line.translate(x, y);
            }
        }

        private void scaleBtn_Click(object sender, EventArgs e)
        {
            int x = int.Parse(scaleX.Text), y = int.Parse(scaleY.Text);
            if (cur == CIRCLE_MID_POINT)
            {
                pictureBox1.Image = c.scale(x, y);
            }
            else if (cur == ELLIPSE_MID_POINT)
            {
                pictureBox1.Image = elip.scale(x, y);
            }
        }

        private void rotateBtn_Click(object sender, EventArgs e)
        {
            int angle = int.Parse(rotateAngle.Text);
            if (cur == CIRCLE_MID_POINT)
                MessageBox.Show("Circle Has No Rotate Diffrence");
            else if (cur == ELLIPSE_MID_POINT)
                pictureBox1.Image = elip.rotate(angle);
            else if (cur == LINE_DDA || cur == LINE_BRESNHAM)
                pictureBox1.Image = line.rotate(angle);
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            cur = "";
        }
    }
}
