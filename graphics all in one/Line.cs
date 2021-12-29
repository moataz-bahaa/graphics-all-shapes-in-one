using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace graphics_all_in_one
{
    class Line
    {
        private const int SIZE = 1000;
        Point[] points = new Point[SIZE];
        private int idx = 0;
        private Point p1;
        private Point p2;
        private int width, height;
        private Bitmap bitmap;
        Point start;
        public Line(int width, int height)
        {
            this.width = width;
            this.height = height;
            bitmap = new Bitmap(width, height);
        }
        public Point[] getPoints()
        {
            return points.Take(idx).ToArray();
        }
        public void initialize(string title = "")
        {
            Form form = new Form();
            Label label1 = new Label();
            Label label2 = new Label();
            Label label3 = new Label();
            Label label4 = new Label();
            TextBox x1 = new TextBox();
            TextBox x2 = new TextBox();
            TextBox y1 = new TextBox();
            TextBox y2 = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label1.Text = "Enter x1: ";
            label2.Text = "Enter y1: ";
            label3.Text = "Enter x2: ";
            label4.Text = "Enter y2: ";

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label1.SetBounds(10, 30, 50, 20);
            label2.SetBounds(10, 60, 50, 20);
            label3.SetBounds(10, 90, 50, 20);
            label4.SetBounds(10, 120, 50, 20);
            x1.SetBounds(80, 30, 100, 20);
            y1.SetBounds(80, 60, 100, 20);
            x2.SetBounds(80, 90, 100, 20);
            y2.SetBounds(80, 120, 100, 20);
            buttonOk.SetBounds(220, 150, 75, 23);
            buttonCancel.SetBounds(310, 150, 75, 23);

            label1.AutoSize = true;
            label2.AutoSize = true;
            label3.AutoSize = true;
            label4.AutoSize = true;


            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label1, label2, label3, label4, x1, y1, x2, y2,
                buttonOk, buttonCancel});
            form.ClientSize = new Size(400, 200);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;
            DialogResult res = form.ShowDialog();
            if (res == DialogResult.OK)
            {
                if (x1.Text != "" && x2.Text != "" && y1.Text != "" && y2.Text != "")
                {
                    this.p1 = new Point(int.Parse(x1.Text), int.Parse(y1.Text));
                    this.p2 = new Point(int.Parse(x2.Text), int.Parse(y2.Text));
                }
                else
                {
                    MessageBox.Show("Please Enter All Values");
                    form.ShowDialog();
                }
            }
        }
        void linePlotPoints()
        {
            bitmap = new Bitmap(width, height);
            for (int i = 0; i < idx; i++)
            {
                if (points[i].X >= 0 && points[i].Y >= 0 && points[i].X < width && points[i].Y < height)
                    bitmap.SetPixel(points[i].X, points[i].Y, Color.Red);
            }
        }
        public Bitmap drawUsingDDA()
        {
            idx = 0;
            int dx = Math.Abs(p2.X - p1.X), dy = Math.Abs(p2.Y - p1.Y), steps;
            steps = Math.Max(dx, dy);
            double xIncrement = dx / steps, yIncremnet = dy / steps,
                x = p1.X, y = p1.Y;
            start = new Point((int)Math.Round(x), (int)Math.Round(y));
            for (int i = 0; i < steps; i++)
            {
                points[idx++] = new Point((int)Math.Round(x), (int)Math.Round(y));
                x += xIncrement;
                y += yIncremnet;
            }
            linePlotPoints();
            return bitmap;
        }

        public Bitmap drawUsingBresenham()
        {
            idx = 0;
            int dx = Math.Abs(p2.X - p1.X), dy = Math.Abs(p2.Y - p1.Y);
            int p = 2 * dy - dx;
            int x, y, end;
            if (p1.X > p2.X)
            {
                x = p2.X;
                y = p2.Y;
                end = p1.X;
            }
            else
            {
                x = p1.X;
                y = p1.Y;
                end = p2.X;
            }
            start = new Point(x, x);
            while (x < end)
            {
                points[idx++] = new Point(x, y);
                x++;
                if (p < 0)
                {
                    p += 2 * dy;
                }
                else
                {
                    p += 2 * dy - 2 * dx;
                    y++;
                }
            }
            linePlotPoints();
            return bitmap;
        }

        int[] multiply(int[,] matrix, int[] p)
        {
            int[] result = new int[3];
            for (int i = 0; i < 3; i++)
            {
                result[i] += matrix[i, 0] * p[0];
                result[i] += matrix[i, 1] * p[1];
                result[i] += matrix[i, 2] * p[2];
            }
            return result;
        }
        double[] multiply(double[,] matrix, double[] p)

        {
            double[] result = new double[3];
            for (int i = 0; i < 3; i++)
            {
                result[i] += matrix[i, 0] * p[0];
                result[i] += matrix[i, 1] * p[1];
                result[i] += matrix[i, 2] * p[2];
            }
            return result;
        }
        public Bitmap translate(int tx, int ty)
        {
            int[,] T = { { 1, 0, tx },
                         { 0, 1, ty },
                         { 0, 0, 1 } };

            for (int i = 0; i < idx; i++)
            {
                int[] p = multiply(T, new int[] { points[i].X, points[i].Y, 1 });
                points[i].X = p[0];
                points[i].Y = p[1];
            }
            start = new Point(points[0].X, points[0].Y);
            linePlotPoints();
            return bitmap;
        }
        public Bitmap rotate(int angle)
        {
            double theta = angle * 3.141592653589 / 180;
            double[,] R = {{Math.Cos(theta), -Math.Sin(theta), 0 },
                        {Math.Sin(theta), Math.Cos(theta), 0 },
                        {0, 0, 0 } };

            for (int i = 0; i < idx; i++)
            {
                double[] p = { points[i].X, points[i].Y, 1 };
                p[0] -= start.X;
                p[1] -= start.Y;

                double[] newPoint = multiply(R, p);
                newPoint[0] += start.X;
                newPoint[1] += start.Y;

                points[i] = new Point((int)Math.Round(newPoint[0]), (int)Math.Round(newPoint[1]));
            }
            linePlotPoints();
            return bitmap;
        }
    }
}
