using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace graphics_all_in_one
{
    class ellipse
    {
        private const int SIZE = 1000;
        private Point[] points = new Point[SIZE];
        private int idx = 0;
        private Point center;
        private int width, height;
        private int rx, ry;
        Bitmap bitmap;
        private int x_axios_center, y_axios_center;

        public ellipse(int width, int height)
        {
            this.width = width;
            this.height = height;
            bitmap = new Bitmap(width, height);
            x_axios_center = width / 2;
            y_axios_center = height / 2;
        }

        public Point[] getPoints()
        {
            return points.Take(idx).ToArray();
        }


        public bool initialize()
        {
            Form form = new Form();
            Label label1 = new Label();
            Label label2 = new Label();
            Label label3 = new Label();
            Label label4 = new Label();

            TextBox x = new TextBox();
            TextBox y = new TextBox();
            TextBox rx = new TextBox();
            TextBox ry = new TextBox();

            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = "Mide Point Ellipse";
            label1.Text = "Enter x Center: ";
            label2.Text = "Enter y Center: ";
            label3.Text = "Enter x Radius: ";
            label4.Text = "Enter y Radius: ";

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label1.SetBounds(10, 30, 50, 20);
            label2.SetBounds(10, 60, 50, 20);
            label3.SetBounds(10, 90, 50, 20);
            label4.SetBounds(10, 120, 50, 20);

            x.SetBounds(150, 30, 100, 20);
            y.SetBounds(150, 60, 100, 20);
            rx.SetBounds(150, 90, 100, 20);
            ry.SetBounds(150, 120, 100, 20);
            buttonOk.SetBounds(220, 180, 75, 23);
            buttonCancel.SetBounds(310, 180, 75, 23);

            label1.AutoSize = true;
            label2.AutoSize = true;
            label3.AutoSize = true;
            label4.AutoSize = true;


            form.Controls.AddRange(new Control[] { label1, label2, label3, label4, x, y, rx, ry,
                buttonOk, buttonCancel});
            form.ClientSize = new Size(400, 220);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;
            DialogResult res = form.ShowDialog();
            if (res == DialogResult.OK)
            {
                if (x.Text != "" && y.Text != "" && rx.Text != "" && ry.Text != "")
                {
                    this.center = new Point(int.Parse(x.Text), int.Parse(y.Text));
                    this.rx = int.Parse(rx.Text);
                    this.ry = int.Parse(ry.Text);
                    return true;
                }
                else
                {
                    MessageBox.Show("Please Enter All Values");
                    form.ShowDialog();
                    return false;
                }
            }
            return false;
        }

        void ellipsePlotPoints()
        {
            bitmap = new Bitmap(width, height);

            Line l1 = new Line(width, height);
            Line l2 = new Line(width, height);
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


            for (int i = 0; i < idx; i++)
            {
                int x = Math.Abs(x_axios_center + points[i].X);
                int y = Math.Abs(y_axios_center - points[i].Y);
                bitmap.SetPixel(x, y, Color.Red);
            }
        }

        void addPointsPartOne(int x, int y)
        {
            points[idx++] = new Point(x + center.X, y + center.Y);
            points[idx++] = new Point(-x + center.X, y + center.Y);
            points[idx++] = new Point(x + center.X, -y + center.Y);
            points[idx++] = new Point(-x + center.X, -y + center.Y);
        }

        void addPointsPartTwo(int x, int y)
        {
            points[idx++] = new Point(x + center.X, y + center.Y);
            points[idx++] = new Point(-x + center.X, y + center.Y);
            points[idx++] = new Point(x + center.X, -y + center.Y);
            points[idx++] = new Point(-x + center.X, -y + center.Y);
        }

        public Bitmap draw()
        {
            idx = 0;
            int dx, dy, p1, p2, x, y;
            x = 0;
            y = ry;

            // Initial decision parameter of region 1
            p1 = (ry * ry) - (rx * rx * ry) + (int)(0.25f * rx * rx);
            dx = 2 * ry * ry * x;
            dy = 2 * rx * rx * y;

            // For region 1
            while (dx < dy)
            {
                addPointsPartOne(x, y);
                if (p1 < 0)
                {
                    x++;
                    dx = dx + (2 * ry * ry);
                    p1 = p1 + dx + (ry * ry);
                }
                else
                {
                    x++;
                    y--;
                    dx = dx + (2 * ry * ry);
                    dy = dy - (2 * rx * rx);
                    p1 = p1 + dx - dy + (ry * ry);
                }
            }

            // Decision parameter of region 2
            p2 = ((ry * ry) * (int)((x + 0.5f) * (x + 0.5f)))
                + ((rx * rx) * ((y - 1) * (y - 1)))
                - (rx * rx * ry * ry);

            while (y >= 0)
            {
                addPointsPartTwo(x, y);
                if (p2 > 0)
                {
                    y--;
                    dy = dy - (2 * rx * rx);
                    p2 = p2 + (rx * rx) - dy;
                }
                else
                {
                    y--;
                    x++;
                    dx = dx + (2 * ry * ry);
                    dy = dy - (2 * rx * rx);
                    p2 = p2 + dx - dy + (rx * rx);
                }
            }
            ellipsePlotPoints();
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
            ellipsePlotPoints();
            return bitmap;
        }

        public Bitmap translate(int tx, int ty)
        {
            int[,] T = { { 1, 0, tx },
                         { 0, 1, ty },
                         { 0, 0, 1 } };


            for (int i = 0; i < idx; i++)
            {
                int[] p = multiply(T, new int[]{ points[i].X, points[i].Y, 1 });
                points[i].X = p[0];
                points[i].Y = p[1];
            }

            center.X += tx;
            center.Y += ty;

            ellipsePlotPoints();
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
                p[0] -= center.X;
                p[1] -= center.Y;

                double[] newPoint = multiply(R, p);
                newPoint[0] += center.X;
                newPoint[1] += center.Y;

                points[i] = new Point((int)Math.Round(newPoint[0]), (int)Math.Round(newPoint[1]));
            }
            ellipsePlotPoints();
            return bitmap;
        }
    }
}
