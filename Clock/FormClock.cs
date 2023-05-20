using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System;

namespace Clock
{
    public partial class ClockForm : Form
    {
        public ClockForm()
        {
            InitializeComponent();
        }

        private void ClockForm_Paint(object sender, PaintEventArgs e)
        {
            DateTime dt = DateTime.Now;
            Graphics g = e.Graphics;
            GraphicsState gs;

            g.TranslateTransform(this.Width / 2, this.Height / 2);
            g.ScaleTransform(this.Width / 250, this.Height / 250);


            Pen cir_pen = new Pen(Color.Black, 1.5f);

            int size = 120;
            g.DrawEllipse(cir_pen, -size, -size, size * 2, size * 2);
            float g2r = MathF.PI / 180;
            //Отрисовка циферблата
            for(int i = 6, number = 0; i <= 360; i+=6) 
            {
                float angle = g2r * (i - 90);
                float x = MathF.Cos(angle), y = MathF.Sin(angle);
                int length = 10;
                if (i % 15 == 0) 
                {
                    number++;
                    length = i % 45 == 0 ? 20 : 15;

                    
                    Brush numberBrush = new SolidBrush(Color.Black);
                    float center_x = x * (size - 30), center_y = y * (size - 30);
                    float rect_size_x = DefaultFont.Size * 2, rect_size_y = DefaultFont.Height + 1;
                    if (number < 10) rect_size_x = DefaultFont.Size;
                    RectangleF rect = new RectangleF(center_x - rect_size_x / 2, center_y - rect_size_y / 2,
                        rect_size_x, rect_size_y);
                    
                    g.DrawString(number.ToString(), DefaultFont, numberBrush, rect);
                    //g.DrawRectangles(cir_pen, new RectangleF[]{ rect });
                }

                g.DrawLine(cir_pen, new PointF(x * (size - length), y * (size - length)),
                    new PointF(x * size, y * size));
            }

            Pen second_pen = new Pen(Color.Red, 1);
            Pen minute_pen = new Pen(Color.Black, 3);
            Pen hour_pen = new Pen(Color.Black, 3);
            float second_length = 90;
            float minute_length = 80;
            float hour_length = 60;

            //Часовая стрелка
            gs = g.Save();
            g.RotateTransform(30 * (dt.Hour - 12) + (float)dt.Minute / 2 + 180);
            g.DrawLine(hour_pen, 0, 0, 0, hour_length);
            g.Restore(gs);
            //Минутная стрелка
            gs = g.Save();
            g.RotateTransform(6 * dt.Minute + (float)dt.Second / 10 + 180);
            g.DrawLine(minute_pen, 0, 0, 0, minute_length);
            g.Restore(gs);
            //Секундняая стрелка
            gs = g.Save();
            g.RotateTransform(6 * (dt.Second + 1) + 180);
            g.DrawLine(second_pen, 0, 0, 0, second_length);
            g.Restore(gs);

            float point_size = 3;
            g.FillEllipse(new SolidBrush(Color.Black), -point_size, -point_size,
                point_size * 2, point_size * 2);

        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}