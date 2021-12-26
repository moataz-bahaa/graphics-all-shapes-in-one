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
        Point[] points = new Point[SIZE];
        private int idx = 0;
        private Point center;
        private int width, height;
        private int rx, ry;
        Bitmap bitmap;
        public ellipse(int width, int height)
        {
            this.width = width;
            this.height = height;
            bitmap = new Bitmap(width, height);
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

        void circlePlotPoints()
        {
            bitmap = new Bitmap(width, height);
            for (int i = 0; i < idx; i++)
            {
                bitmap.SetPixel(points[i].X, points[i].Y, Color.Red);
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
            circlePlotPoints();
            return bitmap;
        }

        public Bitmap scale(int x, int y)
        {
            for (int i = 0; i < idx; i++)
            {
                int a = points[i].X, b = points[i].Y;
                a -= center.X;
                b -= center.Y;

                a *= x;
                b *= y;

                a += center.X;
                b += center.Y;

                points[i] = new Point(a, b);
            }
            circlePlotPoints();
            return bitmap;
        }

        public Bitmap translate(int x, int y)
        {
            for (int i = 0; i < idx; i++)
            {
                points[i].X += x;
                points[i].Y += y;
            }
            center.X += x;
            center.Y += y;
            circlePlotPoints();
            return bitmap;
        }

        public Bitmap rotate(int angle)
        {
            for (int i = 0; i < idx; i++)
            {
                int x = points[i].X, y = points[i].Y;

                x -= center.X;
                y -= center.Y;

                x = (int)((x * Math.Cos(angle * 3.141592653589 / 180)) - (y * Math.Sin(angle * 3.141592653589 / 180)));
                y = (int)((x * Math.Sin(angle * 3.141592653589 / 180)) + (y * Math.Cos(angle * 3.141592653589 / 180)));

                x += center.X;
                y += center.Y;

                points[i] = new Point(x, y);
            }
            circlePlotPoints();
            return bitmap;
        }
    }
}
