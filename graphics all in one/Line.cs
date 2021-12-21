﻿using System;
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
        private Point p1;
        private Point p2;
        private int width, height;

        public Line(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        public Bitmap drawUsingDDA()
        {
            Bitmap bitmap = new Bitmap(width, height);
            int dx = Math.Abs(p2.X - p1.X), dy = Math.Abs(p2.Y - p1.Y), steps;
            steps = Math.Max(dx, dy);
            double xIncrement = dx / steps, yIncremnet = dy / steps,
                x = p1.X, y = p1.Y;
            for (int i = 0; i < steps; i++)
            {
                bitmap.SetPixel((int)Math.Round(x), (int)Math.Round(y), Color.Red);
                x += xIncrement;
                y += yIncremnet;
            }
            return bitmap;
        }

        public Bitmap drawUsingBresenham()
        {
            Bitmap bitmap = new Bitmap(width, height);
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
            while (x < end)
            {
                bitmap.SetPixel(x, y, Color.Red);
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
            return bitmap;
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
            form.Controls.AddRange(new Control[] { label1, label2, label3, label4, x1, x2, y1, y2,
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
    }
}