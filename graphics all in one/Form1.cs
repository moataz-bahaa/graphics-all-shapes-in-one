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
        private int x_axios_center, y_axios_center;
        public Form1()
        {
            InitializeComponent();
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            c = new Circle();
            elip = new ellipse();
            line = new Line();
            x_axios_center = pictureBox1.Width / 2;
            y_axios_center = pictureBox1.Height / 2;

            plotPoints(new Point[0]);
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        void plotPoints(Point[] points)
        {
            int width = pictureBox1.Width, height = pictureBox1.Height;
            Bitmap bitmap = new Bitmap(width, height);

            Line l1 = new Line();
            Line l2 = new Line();
            l1.setP1Andp2(new Point(0, y_axios_center), new Point(width, y_axios_center));
            l1.drawUsingDDA();
            Point[] arr1 = l1.getPoints();

            l2.setP1Andp2(new Point(x_axios_center, 0), new Point(x_axios_center, height));
            l2.drawUsingDDA();
            Point[] arr2 = l2.getPoints();

            foreach (Point it in arr1)
                if (it.X >= 0 && it.Y >= 0 && it.X < width && it.Y < height)
                    bitmap.SetPixel(it.X, it.Y, Color.Black);

            foreach (Point it in arr2)
                if (it.X >= 0 && it.Y >= 0 && it.X < width && it.Y < height)
                    bitmap.SetPixel(it.X, it.Y, Color.Black);


            foreach (var p in points)
            {
                int x = Math.Abs(x_axios_center + p.X);
                int y = Math.Abs(y_axios_center - p.Y);
                bitmap.SetPixel(x, y, Color.Red);
            }
            pictureBox1.Image = bitmap;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cur = comboBox1.Text;
            if (cur == LINE_DDA)
            {
                try
                {
                    line.initialize("Drawing Line Using DDA Algorithm");
                    line.drawUsingDDA();
                    Point[] points = line.getPoints();
                    dataGridView1.DataSource = points;
                    plotPoints(points);

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
                    line.drawUsingBresenham();
                    Point[] points = line.getPoints();
                    dataGridView1.DataSource = points;
                    plotPoints(points);
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
                    {
                        c.draw();
                        Point[] points = c.getPoints();
                        dataGridView1.DataSource = points;
                        plotPoints(points);
                    }
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
                    {
                        elip.draw();
                        Point[] points = elip.getPoints();
                        dataGridView1.DataSource = points;
                        plotPoints(points);
                    }
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
                c.translate(x, y);
                Point[] points = c.getPoints();
                dataGridView1.DataSource = points;
                plotPoints(points);
            }
            else if (cur == ELLIPSE_MID_POINT)
            {
                elip.translate(x, y);
                Point[] points = elip.getPoints();
                dataGridView1.DataSource = points;
                plotPoints(points);
            }
            else if (cur == LINE_DDA || cur == LINE_BRESNHAM)
            {
                line.translate(x, y);
                Point[] points = line.getPoints();
                dataGridView1.DataSource = points;
                plotPoints(points);
            }
        }

        private void scaleBtn_Click(object sender, EventArgs e)
        {
            double x = double.Parse(scaleX.Text), y = double.Parse(scaleY.Text);
            if (cur == CIRCLE_MID_POINT)
            {
                c.scale(x, y);
                Point[] points = c.getPoints();
                dataGridView1.DataSource = points;
                plotPoints(points);
            }
            else if (cur == ELLIPSE_MID_POINT)
            {
                elip.scale(x, y);
                Point[] points = elip.getPoints();
                dataGridView1.DataSource = points;
                plotPoints(points);
            }
        }

        private void rotateBtn_Click(object sender, EventArgs e)
        {
            int angle = int.Parse(rotateAngle.Text);
            if (cur == CIRCLE_MID_POINT)
                MessageBox.Show("Circle Has No Rotate Diffrence");
            else if (cur == ELLIPSE_MID_POINT)
            {
                elip.rotate(angle);
                Point[] points = elip.getPoints();
                dataGridView1.DataSource = points;
                plotPoints(points);
            }
            else if (cur == LINE_DDA || cur == LINE_BRESNHAM)
            {
                line.rotate(angle);
                Point[] points = line.getPoints();
                dataGridView1.DataSource = points;
                plotPoints(points);
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            cur = "";
            plotPoints(new Point[0]);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
