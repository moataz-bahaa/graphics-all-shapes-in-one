using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphics_all_in_one
{
    class Circle
    {
        private const int SIZE = 1000;
        Point[] points = new Point[SIZE];
        private int idx = 0;
        private Point center;
        private int radiux, width, height;
        Bitmap bitmap;
        public Circle (int width, int height)
        {
            this.width = width;
            this.height = height;
            bitmap = new Bitmap(width, height);
        }
        void circlePlotPoints()
        {
            bitmap = new Bitmap(width, height);
            for (int i = 0; i < idx; i++)
            {
                bitmap.SetPixel(points[i].X, points[i].Y, Color.Red);
            }
        }


        void addPoints(int x, int y)
        {
            points[idx++] = new Point(center.X + x, center.Y + y);
            points[idx++] = new Point(center.X + x, center.Y - y);
            points[idx++] = new Point(center.X - x, center.Y + y);
            points[idx++] = new Point(center.X - x, center.Y - y);
            points[idx++] = new Point(center.X + y, center.Y + x);
            points[idx++] = new Point(center.X - y, center.Y + x);
            points[idx++] = new Point(center.X + y, center.Y - x);
            points[idx++] = new Point(center.X - y, center.Y - x);
        }

        public Bitmap draw()
        {
            idx = 0;
            int x = 0, y = radiux, p = 1 - radiux;
            addPoints(x, y);
            while (x < y)
            {
                x++;
                if (p < 0)
                    p += 2 * x + 1;
                else
                {
                    y--;
                    p += 2 * (x - y) + 1;
                }
                addPoints(x, y);
            }
            circlePlotPoints();
            return bitmap;
        }

        public bool initialize()
        {
            Form form = new Form();
            Label label1 = new Label();
            Label label2 = new Label();
            Label label3 = new Label();
            TextBox x = new TextBox();
            TextBox y = new TextBox();
            TextBox r = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = "Mide Point Circle";
            label1.Text = "Enter x: ";
            label2.Text = "Enter y: ";
            label3.Text = "Enter radiux: ";

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label1.SetBounds(10, 30, 50, 20);
            label2.SetBounds(10, 60, 50, 20);
            label3.SetBounds(10, 90, 50, 20);
            x.SetBounds(120, 30, 100, 20);
            y.SetBounds(120, 60, 100, 20);
            r.SetBounds(120, 90, 100, 20);
            buttonOk.SetBounds(220, 150, 75, 23);
            buttonCancel.SetBounds(310, 150, 75, 23);

            label1.AutoSize = true;
            label2.AutoSize = true;
            label3.AutoSize = true;


            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label1, label2, label3, x, y, r,
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
                if (x.Text != "" && y.Text != "" && r.Text != "")
                {
                    this.center = new Point(int.Parse(x.Text), int.Parse(y.Text));
                    this.radiux = int.Parse(r.Text);
                    return true;
                }
                else
                {
                    MessageBox.Show("Please Enter All Values");
                    form.ShowDialog();
                }
            }
            return false;
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
        public Bitmap scale(double sx, double sy)
        {
            double[,] S = { { sx, 0, 0 },
                         { 0, sy, 0 },
                         { 0, 0, 1 } };

            for (int i = 0; i < idx; i++)
            {
                double[] p = { points[i].X, points[i].Y, 1 };

                p[0] -= center.X;
                p[1] -= center.Y;

                double[] newPoint = multiply(S, p);

                newPoint[0] += center.X;
                newPoint[1] += center.Y;

                points[i] = new Point((int)Math.Round(newPoint[0]), (int)Math.Round(newPoint[1]));
            }
            circlePlotPoints();
            return bitmap;
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

            center.X += tx;
            center.Y += ty;

            circlePlotPoints();
            return bitmap;
        }

    }
}
