using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace projekt3_bresenham {
    public class Primitives {
        
        public static void FillBitmap(Brush col, Graphics g, int width, int height) {
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++)
                    // bmp.SetPixel(x, y, col);
                    g.FillRectangle(col, new Rectangle(new Point(x, y), new Size(1, 1)));
            }
        }
        
        public static void MidpointLine(int x1, int y1, int x2, int y2, Brush col,int thickness, Graphics g) {
            if (x2 > x1) {
                MidpointLine2(x1, y1, x2, y2, col,thickness, g);
            } else {
                MidpointLine2(x2, y2, x1, y1, col,thickness, g);
            }
        }

        private static void MidpointLine1(int x1, int y1, int x2, int y2, Brush col, int thickness, Graphics g) {
            int dx, dy, incrE, incrNE, d, x, y;
           
            dx = x2 - x1;
            dy = y2 - y1;
           
            d = dy * 2 - dx;
            incrE = dy * 2;
            incrNE = (dy - dx) * 2;
            x = x1;
            y = y1;
            
                

               // pb.SetPixel(x, y, col);
            g.FillRectangle(col, new Rectangle(new Point(x, y), new Size(thickness, thickness)));
            
            while (x < x2) {
                if (d <= 0) {
                    d += incrE;
                    x++;
                } else {
                    d += incrNE;
                    x++;
                    y++;
                }
               
                    //pb.SetPixel(x, y, col);
                    g.FillRectangle(col, new Rectangle(new Point(x, y), new Size(thickness, thickness)));
                
            }

        }

        private static void MidpointLine2(int x1, int y1, int x2, int y2, Brush col, int thickness, Graphics g) {
            int dx, dy, incrE,incrN, incrNE,incrSE,incrS, d, x, y;
            dx = x2 - x1;
            dy = y2 - y1;
            float div = (float)dy / (float)dx;
            if (div > 1) {
                d = dx * 2 - dy;
                incrNE = (dx - dy) * 2;
                incrN = dx*2 ;

                x = x1;
                y = y1;
                try { g.FillRectangle(col, new Rectangle(new Point(x, y), new Size(thickness, thickness))); } catch (Exception) { };

                while (y < y2) {
                    if (d <= 0) {
                        d += incrN;
                        y++;
                    } else {
                        d += incrNE;
                        x++;
                        y++;
                    }
                    try { g.FillRectangle(col, new Rectangle(new Point(x, y), new Size(thickness, thickness))); } catch (Exception) { };
                }
                
            } else if (div<= 1 && div >= 0) {
                d = dy * 2 - dx;
                incrE = dy * 2;
                incrNE = (dy - dx) * 2;
                x = x1;
                y = y1;
                try { g.FillRectangle(col, new Rectangle(new Point(x, y), new Size(thickness, thickness))); } catch (Exception) { };

                while (x < x2) {
                    if (d <= 0) {
                        d += incrE;
                        x++;
                    } else {
                        d += incrNE;
                        x++;
                        y++;
                    }
                    try { g.FillRectangle(col, new Rectangle(new Point(x, y), new Size(thickness, thickness))); } catch (Exception) { };
                }
            } else if (div < 0 && div > -1) {
                d = -dy * 2 - dx;
                incrE = -dy * 2;
                incrSE = (-dy - dx) * 2;
                x = x1;
                y = y1;
                try { g.FillRectangle(col, new Rectangle(new Point(x, y), new Size(thickness, thickness))); } catch (Exception) { };

                while (x < x2) {
                    if (d <= 0) {
                        d += incrE;
                        x++;
                    } else {
                        d += incrSE;
                        x++;
                        y--;
                    }
                    try { g.FillRectangle(col, new Rectangle(new Point(x, y), new Size(thickness, thickness))); } catch (Exception) { };
                }
            } else if (div <= -1) {
                d = dx * 2 + dy;
                incrSE = (dx + dy) * 2;
                incrS = dx * 2;
                x = x1;
                y = y1;
                try { g.FillRectangle(col, new Rectangle(new Point(x, y), new Size(thickness, thickness))); } catch (Exception) { };

                while (y > y2) {
                    if (d <= 0) {
                        d += incrS;
                        y--;
                    } else {
                        d += incrSE;
                        x++;
                        y--;
                    }
                    try { g.FillRectangle(col, new Rectangle(new Point(x, y), new Size(thickness, thickness))); } catch (Exception) { };
                }
            }

        }

        public static void MidpointCircle(int x1, int y1, int R, Brush col, int thickness, Graphics g) {
            int x, y, d, deltaE, deltaSE;
            x = 0;
            y = R;
            d = 1 - R;
            deltaE = 3;
            deltaSE = 5 - R * 2;

            #region drawing pixels
            try { 
                //bmp.SetPixel(x + x1, y + y1, col); 
                g.FillRectangle(col, new Rectangle(new Point(x+x1, y+y1), new Size(thickness, thickness)));
            } catch (Exception e) { }
            try { 
                //bmp.SetPixel(y + x1, x + y1, col); 
                g.FillRectangle(col, new Rectangle(new Point(y+x1, x+y1), new Size(thickness, thickness)));
            } catch (Exception e) { }
            try { 
                //bmp.SetPixel(x + x1, y1 - y, col); 
                g.FillRectangle(col, new Rectangle(new Point(x+x1, y1-y), new Size(thickness, thickness)));
            } catch (Exception e) { }
            try { 
                //bmp.SetPixel(y + x1, y1 - x, col);
                g.FillRectangle(col, new Rectangle(new Point(y+x1, y1-x), new Size(thickness, thickness)));
            } catch (Exception e) { }

            try { 
                //bmp.SetPixel(x1 - x, y + y1, col);
                g.FillRectangle(col, new Rectangle(new Point(x1-x, y+y1), new Size(thickness, thickness)));
            } catch (Exception e) { }
            try { 
                //bmp.SetPixel(x1 - y, x + y1, col); 
                g.FillRectangle(col, new Rectangle(new Point(x1-y, x+y1), new Size(thickness, thickness)));
            } catch (Exception e) { }
            try { 
                //bmp.SetPixel(x1 - x, y1 - y, col);
                g.FillRectangle(col, new Rectangle(new Point(x1-x, y1-y), new Size(thickness, thickness)));
            } catch (Exception e) { }
            try { 
                //bmp.SetPixel(x1 - y, y1 - x, col); 
                g.FillRectangle(col, new Rectangle(new Point(x1-y, y1-x), new Size(thickness, thickness)));
            } catch (Exception e) { }
            #endregion

            while (y>x){
                if (d < 0) {
                    d += deltaE;
                    deltaE += 2;
                    deltaSE += 2;
                    x++;
                } else {
                    d += deltaSE;
                    deltaE += 2;
                    deltaSE += 4;
                    x++;
                    y--;
                }


              
                #region drawing pixels
                try {
                    //bmp.SetPixel(x + x1, y + y1, col); 
                    g.FillRectangle(col, new Rectangle(new Point(x + x1, y + y1), new Size(thickness, thickness)));
                } catch (Exception e) { }
                try {
                    //bmp.SetPixel(y + x1, x + y1, col); 
                    g.FillRectangle(col, new Rectangle(new Point(y + x1, x + y1), new Size(thickness, thickness)));
                } catch (Exception e) { }
                try {
                    //bmp.SetPixel(x + x1, y1 - y, col); 
                    g.FillRectangle(col, new Rectangle(new Point(x + x1, y1 - y), new Size(thickness, thickness)));
                } catch (Exception e) { }
                try {
                    //bmp.SetPixel(y + x1, y1 - x, col);
                    g.FillRectangle(col, new Rectangle(new Point(y + x1, y1 - x), new Size(thickness, thickness)));
                } catch (Exception e) { }

                try {
                    //bmp.SetPixel(x1 - x, y + y1, col);
                    g.FillRectangle(col, new Rectangle(new Point(x1 - x, y + y1), new Size(thickness, thickness)));
                } catch (Exception e) { }
                try {
                    //bmp.SetPixel(x1 - y, x + y1, col); 
                    g.FillRectangle(col, new Rectangle(new Point(x1 - y, x + y1), new Size(thickness, thickness)));
                } catch (Exception e) { }
                try {
                    //bmp.SetPixel(x1 - x, y1 - y, col);
                    g.FillRectangle(col, new Rectangle(new Point(x1 - x, y1 - y), new Size(thickness, thickness)));
                } catch (Exception e) { }
                try {
                    //bmp.SetPixel(x1 - y, y1 - x, col); 
                    g.FillRectangle(col, new Rectangle(new Point(x1 - y, y1 - x), new Size(thickness, thickness)));
                } catch (Exception e) { }
                #endregion
            }
        }
        public static void MidpointEllipse(int x1, int y1, int a, int b, Brush col, int thickness, Graphics g) {
            
            ellipse(x1,y1, a, b, col, thickness, g, true);
            ellipse(x1, y1, b, a, col, thickness, g, false);
        }
        private static void ellipse(int x1, int y1, int a, int b, Brush col,int thickness, Graphics g,bool xy) {
            int x, y;
            int a2 = a * a;
            int b2 = b * b;

            int d = 4 * b2 - 4 * b * a2 + a2;
            int deltaE = 4 * 3 * b2;
            int deltaSE = 4 * (3 * b2 - 2 * b * a2 + 2 * a2);

            int limit = (int)(((double)(a2) * a2) / ((double)(a2) + b2));
            x = 0;
            y = b;
            #region draw pixels
            if (xy) {
                try { g.FillRectangle(col, new Rectangle(new Point(x + x1, y + y1), new Size(thickness, thickness))); } catch (Exception e) { }
                try { g.FillRectangle(col, new Rectangle(new Point(x + x1, y1 - y), new Size(thickness, thickness))); } catch (Exception e) { }
                try { g.FillRectangle(col, new Rectangle(new Point(x1 - x, y + y1), new Size(thickness, thickness))); } catch (Exception e) { }
                try { g.FillRectangle(col, new Rectangle(new Point(x1 - x, y1 - y), new Size(thickness, thickness))); } catch (Exception e) { }
            } else {
                try { g.FillRectangle(col, new Rectangle(new Point(y + x1, x + y1), new Size(thickness, thickness))); } catch (Exception e) { }
                try { g.FillRectangle(col, new Rectangle(new Point(y + x1, y1 - x), new Size(thickness, thickness))); } catch (Exception e) { }
                try { g.FillRectangle(col, new Rectangle(new Point(x1 - y, x + y1), new Size(thickness, thickness))); } catch (Exception e) { }
                try { g.FillRectangle(col, new Rectangle(new Point(x1 - y, y1 - x), new Size(thickness, thickness))); } catch (Exception e) { }
            }
            
            
            #endregion
            while (true) {
                if (x * x >= limit) {
                    break;
                }
                if (d <= 0) {
                    d += deltaE;
                    deltaE += 4 * 2 * b*b; 
                    deltaSE += 4 * 2 * b*b;
                    x++;
                } else {
                    d += deltaSE;
                    deltaE += 4 * 2 * b * b;
                    deltaSE += 4 * (2 * b*b + 2 * a*a);
                    x++;
                    y--;
                }



                #region draw pixels
                if (xy) {
                    try { g.FillRectangle(col, new Rectangle(new Point(x + x1, y + y1), new Size(thickness, thickness))); } catch (Exception e) { }
                    try { g.FillRectangle(col, new Rectangle(new Point(x + x1, y1 - y), new Size(thickness, thickness))); } catch (Exception e) { }
                    try { g.FillRectangle(col, new Rectangle(new Point(x1 - x, y + y1), new Size(thickness, thickness))); } catch (Exception e) { }
                    try { g.FillRectangle(col, new Rectangle(new Point(x1 - x, y1 - y), new Size(thickness, thickness))); } catch (Exception e) { }
                } else {
                    try { g.FillRectangle(col, new Rectangle(new Point(y + x1, x + y1), new Size(thickness, thickness))); } catch (Exception e) { }
                    try { g.FillRectangle(col, new Rectangle(new Point(y + x1, y1 - x), new Size(thickness, thickness))); } catch (Exception e) { }
                    try { g.FillRectangle(col, new Rectangle(new Point(x1 - y, x + y1), new Size(thickness, thickness))); } catch (Exception e) { }
                    try { g.FillRectangle(col, new Rectangle(new Point(x1 - y, y1 - x), new Size(thickness, thickness))); } catch (Exception e) { }
                }


                #endregion



            }
        }




        public static void MidpointEllipse2(int x1, int y1, int a, int b, Brush col,int thickness, Graphics g) {
            int x, y;
            double d, db, da, deltaE, deltaSE;
            x = 0;
            y = b;
            db = Convert.ToDouble(b);
            da = Convert.ToDouble(a);
            d = (double)(((double)db * db - (double)da * da * db + (double)da * da) / (double)(da * da * db * db));
            deltaE = (double)((double)3 / (double)(da * da));
            deltaSE = (double)(((double)2 - (double)(db * 2)) / (double)(db * db));

            #region drawing pixels
            try { 
                //bmp.SetPixel(x + x1, y + y1, col);
                g.FillRectangle(col, new Rectangle(new Point(x + x1, y + y1), new Size(thickness, thickness)));
            } catch (Exception e) { }
            try { 
                //bmp.SetPixel(y + x1, x + y1, col); 
                g.FillRectangle(col, new Rectangle(new Point(y + x1, x + y1), new Size(thickness, thickness)));
            } catch (Exception e) { }
            try { 
                //bmp.SetPixel(x + x1, y1 - y, col); 
                g.FillRectangle(col, new Rectangle(new Point(x + x1, y1 - y), new Size(thickness, thickness)));
            } catch (Exception e) { }
            try { 
                //bmp.SetPixel(y + x1, y1 - x, col);
                g.FillRectangle(col, new Rectangle(new Point(y + x1, y1 - x), new Size(thickness, thickness)));
            } catch (Exception e) { }

            try { 
                //bmp.SetPixel(x1 - x, y + y1, col);
                g.FillRectangle(col, new Rectangle(new Point(x1 - x, y + y1), new Size(thickness, thickness)));
            } catch (Exception e) { }
            try { 
                //bmp.SetPixel(x1 - y, x + y1, col); 
                g.FillRectangle(col, new Rectangle(new Point(x1 - y, x + y1), new Size(thickness, thickness)));
            } catch (Exception e) { }
            try { 
                //bmp.SetPixel(x1 - x, y1 - y, col); 
                g.FillRectangle(col, new Rectangle(new Point(x1 -x, y1 - y), new Size(thickness, thickness)));
            } catch (Exception e) { }
            try { 
                //bmp.SetPixel(x1 - y, y1 - x, col); 
                g.FillRectangle(col, new Rectangle(new Point(x1 - y, y1 - x), new Size(thickness, thickness)));
            } catch (Exception e) { }
            #endregion

            while (y > x) {
                if (d < 0) {
                    d += deltaE;
                    deltaE += (double)((double)2 / (double)da * da);
                    deltaSE += (double)((double)2 / (double)da * da);
                    x++;
                } else {
                    d += deltaSE;//(double)((double)(2 * x + 3) * (double)(db * db)) + (double)((double)(-2 * y + 2) * (double)(da * da));
                    deltaE += (double)((double)2 / (double)da * da);
                    deltaSE += ((double)((double)2 / (double)(da * da)) + (double)((double)2 / (double)(db * db)));
                    x++;
                    y--;
                }


                #region drawing pixels
                try {
                    //bmp.SetPixel(x + x1, y + y1, col);
                    g.FillRectangle(col, new Rectangle(new Point(x + x1, y + y1), new Size(thickness, thickness)));
                } catch (Exception e) { }
                try {
                    //bmp.SetPixel(y + x1, x + y1, col); 
                    g.FillRectangle(col, new Rectangle(new Point(y + x1, x + y1), new Size(thickness, thickness)));
                } catch (Exception e) { }
                try {
                    //bmp.SetPixel(x + x1, y1 - y, col); 
                    g.FillRectangle(col, new Rectangle(new Point(x + x1, y1 - y), new Size(thickness, thickness)));
                } catch (Exception e) { }
                try {
                    //bmp.SetPixel(y + x1, y1 - x, col);
                    g.FillRectangle(col, new Rectangle(new Point(y + x1, y1 - x), new Size(thickness, thickness)));
                } catch (Exception e) { }
                try {
                    //bmp.SetPixel(x1 - x, y + y1, col);
                    g.FillRectangle(col, new Rectangle(new Point(x1 - x, y + y1), new Size(thickness, thickness)));
                } catch (Exception e) { }
                try {
                    //bmp.SetPixel(x1 - y, x + y1, col); 
                    g.FillRectangle(col, new Rectangle(new Point(x1 - y, x + y1), new Size(thickness, thickness)));
                } catch (Exception e) { }
                try {
                    //bmp.SetPixel(x1 - x, y1 - y, col); 
                    g.FillRectangle(col, new Rectangle(new Point(x1 - x, y1 - y), new Size(thickness, thickness)));
                } catch (Exception e) { }
                try {
                    //bmp.SetPixel(x1 - y, y1 - x, col); 
                    g.FillRectangle(col, new Rectangle(new Point(x1 - y, y1 - x), new Size(thickness, thickness)));
                } catch (Exception e) { }
            #endregion

            }
        }

        public static void MidpointEllipse1(int xc, int yc, int rx, int ry, Brush col, int thickness, Graphics g) {
        long  rx_2 = rx*rx,   ry_2 = ry*ry;
        long  p = ry_2 - rx_2*ry + (ry_2>>2);
        int x = 0,       y = ry;
        long two_ry_2_x = 0, two_rx_2_y = (rx_2<<1)*y;
       
        try {
            //bmp.SetPixel(x+xc, y+yc, col);
            g.FillRectangle(col, new Rectangle(new Point(x+xc,y+yc), new Size(thickness, thickness)));
        } catch (Exception e) { };
        try { 
            //bmp.SetPixel(x + xc, yc - y, col); 
            g.FillRectangle(col, new Rectangle(new Point(x+xc,yc-y), new Size(thickness, thickness)));
        } catch (Exception e) { }
        try { 
            //bmp.SetPixel(xc - x, y + yc, col); 
            g.FillRectangle(col, new Rectangle(new Point(xc-x,y+yc), new Size(thickness, thickness)));
        } catch (Exception e) { }
        try { 
            //bmp.SetPixel(xc - x, yc - y, col);
            g.FillRectangle(col, new Rectangle(new Point(xc-x,yc-y), new Size(thickness, thickness)));
        } catch (Exception e) { }

        while(two_rx_2_y >= two_ry_2_x){
              ++x;
              two_ry_2_x += (ry_2<<1);
             
              p +=  two_ry_2_x + ry_2;
                
              if(p >= 0){
                   --y;
                   two_rx_2_y -= (rx_2<<1);
                  
                   p -= two_rx_2_y ;
              }
              
            /*try { bmp.SetPixel(x + xc, y + yc, col); } catch (Exception e) { };
              try { bmp.SetPixel(x + xc, yc - y, col); } catch (Exception e) { }
              try { bmp.SetPixel(xc - x, y + yc, col); } catch (Exception e) { }
              try { bmp.SetPixel(xc - x, yc - y, col); } catch (Exception e) { }
            */
              try {
                  //bmp.SetPixel(x+xc, y+yc, col);
                  g.FillRectangle(col, new Rectangle(new Point(x + xc, y + yc), new Size(thickness, thickness)));
              } catch (Exception e) { };
              try {
                  //bmp.SetPixel(x + xc, yc - y, col); 
                  g.FillRectangle(col, new Rectangle(new Point(x + xc, yc - y), new Size(thickness, thickness)));
              } catch (Exception e) { }
              try {
                  //bmp.SetPixel(xc - x, y + yc, col); 
                  g.FillRectangle(col, new Rectangle(new Point(xc - x, y + yc), new Size(thickness, thickness)));
              } catch (Exception e) { }
              try {
                  //bmp.SetPixel(xc - x, yc - y, col);
                  g.FillRectangle(col, new Rectangle(new Point(xc - x, yc - y), new Size(thickness, thickness)));
              } catch (Exception e) { }
        }
       
        p = (long)(ry_2*(x+1/2.0)*(x+1/2.0) + rx_2*(y-1)*(y-1) - rx_2*ry_2);
        while (y>=0) {
              p += rx_2;
              --y;
              two_rx_2_y -= (rx_2<<1);
              p -= two_rx_2_y;
             
              if(p <= 0) {
                   ++x;
                   two_ry_2_x += (ry_2<<1);
                   p += two_ry_2_x;
              }
              /*try { bmp.SetPixel(x + xc, y + yc, col); } catch (Exception e) { };
              try { bmp.SetPixel(x + xc, yc - y, col); } catch (Exception e) { }
              try { bmp.SetPixel(xc - x, y + yc, col); } catch (Exception e) { }
              try { bmp.SetPixel(xc - x, yc - y, col); } catch (Exception e) { }
            */
              try {
                  //bmp.SetPixel(x+xc, y+yc, col);
                  g.FillRectangle(col, new Rectangle(new Point(x + xc, y + yc), new Size(thickness, thickness)));
              } catch (Exception e) { };
              try {
                  //bmp.SetPixel(x + xc, yc - y, col); 
                  g.FillRectangle(col, new Rectangle(new Point(x + xc, yc - y), new Size(thickness, thickness)));
              } catch (Exception e) { }
              try {
                  //bmp.SetPixel(xc - x, y + yc, col); 
                  g.FillRectangle(col, new Rectangle(new Point(xc - x, y + yc), new Size(thickness, thickness)));
              } catch (Exception e) { }
              try {
                  //bmp.SetPixel(xc - x, yc - y, col);
                  g.FillRectangle(col, new Rectangle(new Point(xc - x, yc - y), new Size(thickness, thickness)));
              } catch (Exception e) { }
        }
}
    }
}
