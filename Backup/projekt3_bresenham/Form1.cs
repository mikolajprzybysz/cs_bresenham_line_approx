using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace projekt3_bresenham {
    public partial class Form1 : Form {
        Bitmap bm;
        int click = 0;
        int x1 = 0;
        int x2 = 0;
        int y1 = 0;
        int y2 = 0;
        DrawingPane pane = new DrawingPane();
        public Form1() {
            
            InitializeComponent();
            //bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            
            pane.Visible = true;
            pane.Size = new Size(panel1.Size.Width,panel1.Size.Height);
            pane.Location = new Point(0, 0);
            //pane.AddLine();
            this.panel1.Controls.Add(pane);
            this.panel1.Update();
            //Primitives.FillBitmap(Brushes.White, g,10,10);
           


           // Graphics g = CreateGraphics();
            
        }

        

        void pictureBox1_MouseClick(object sender, MouseEventArgs e) {
            //if (click == 0) {
            //    x1 = e.X;
            //    y1 = e.Y;
            //    click++;
            //    return;
            //}
            //if (click == 1) {
            //   // Primitives.MidpointLine(x1, y1, e.X, e.Y, Color.Black, bm);
            //    pictureBox1.Image = bm;
            //    click = 0;
            //    x1 = 0;
            //    y1 = 0;
            //    return;
            //}
        }

        private void buttonLine_Click(object sender, EventArgs e) {
            pane.NormalMode();
            pane.AddLine();
        }

        private void buttonCircle_Click(object sender, EventArgs e) {
            pane.NormalMode();

            pane.AddCircle();
        }
        private void buttonEllipse_Click(object sender, EventArgs e) {
            pane.NormalMode();

            pane.AddEllipse();
        }

        private void buttonRemove_Click(object sender, EventArgs e) {
            pane.RemoveMode();
        }

        private void buttonEdit_Click(object sender, EventArgs e) {
            pane.NormalMode();
        }

        private void button1_Click(object sender, EventArgs e) {
            pane.PolyMode();
            pane.AddPoly();
        }
    }
}
