using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace projekt3_bresenham {
    public partial class DrawingPane : Control {
        internal class Points {
            public int x0 { get; set; }
            public int x1 { get; set; }

            public int y0 { get; set; }
            public int y1 { get; set; }

            public int x2 { get; set; }
            public int y2 { get; set; }

            public bool line { get; set; }
            public bool circle { get; set; }
            public bool ellipse { get; set; }
            public bool polyline { get; set; }

            public int R { get; set; }
            public int a { get; set; }
            public int b { get; set; }
            public ArrayList points { get; set; }
            

            public Points(string type) {
                this.x0 = 50;
                this.y0 = 50;

                this.x1 = 100;
                this.y1 = 100;

                this.x2 = 100;
                this.y2 = 110;

                this.R = 40;
                this.a = 20;
                this.b = 40;
                if (type.CompareTo("line") == 0) {
                    line = true;
                } else if (type.CompareTo("circle") == 0) {
                    circle = true;
                } else if (type.CompareTo("ellipse") == 0) {
                    ellipse = true;
                } else if (type.CompareTo("polyline") == 0) {
                    polyline = true;
                }
            }
        }
        Points actualPoly;
        bool isMouseOverPoint0 = false;
        bool isMouseOverPoint1 = false;
        bool isMouseOverPoint2 = false;
        bool isMouseDown = false;
        bool refresh0 = false;
        bool refresh1 = false;
        bool refresh2 = false;
        bool valueChanged = false;
        string mode = "normal";
        int smallCircleSize { get; set; }
        ArrayList arr = new ArrayList();
        public DrawingPane() {
            InitializeComponent();

            this.smallCircleSize = 3;
            //setInitial();
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;

            //this.MouseHover += new EventHandler(Line_MouseHover);
            NormalMode();

            //this.Refresh();
            //testing-------------
            //circle = true;
            //line = false;
            //ellipse = false;
            //if (ellipse) {
            //    this.MouseMove+= new MouseEventHandler(Ellipse_MouseMove);
            //}
            //circle = false;
        }
        /*private void setInitial() {
            this.x0 = 0;
            this.y0 = 0;
            
            this.x1 = 100;
            this.y1 = 100;

            this.x2 = 100;
            this.y2 = 110;

            this.R = 40;
            this.a = 20;
            this.b = 40;
        }*/
        void Line_MouseUp(object sender, MouseEventArgs e) {
            isMouseDown = false;
            valueChanged = false;
        }

        void Line_MouseDown(object sender, MouseEventArgs e) {
            isMouseDown = true;
        }
        public void NormalMode() {
            if (mode == "polyline")
                this.MouseDown -= new MouseEventHandler(Poly_MouseDown);
            this.MouseMove += new MouseEventHandler(Line_MouseMove);
            this.MouseDown += new MouseEventHandler(Line_MouseDown);
            this.MouseUp += new MouseEventHandler(Line_MouseUp);
            if(mode=="remove")
                this.MouseDown -= new MouseEventHandler(Remove_MouseDown);
            
            mode = "normal";

        }
        public void PolyMode() {
            if (mode == "normal") {
                this.MouseMove -= new MouseEventHandler(Line_MouseMove);
                this.MouseDown -= new MouseEventHandler(Line_MouseDown);
                this.MouseUp -= new MouseEventHandler(Line_MouseUp);
            }
            if (mode == "remove") {
                this.MouseDown -= new MouseEventHandler(Remove_MouseDown);
            }
            this.MouseDown += new MouseEventHandler(Poly_MouseDown);
            mode = "polyline";
        }

        void Poly_MouseDown(object sender, MouseEventArgs e) {
            actualPoly.points.Add(new Point(e.X,e.Y));
            this.Refresh();
        }
        public void RemoveMode() {
            if(mode== "polyline")
            this.MouseDown -= new MouseEventHandler(Poly_MouseDown);

            mode = "remove";
            this.MouseMove -= new MouseEventHandler(Line_MouseMove);
            this.MouseDown -= new MouseEventHandler(Line_MouseDown);
            this.MouseUp -= new MouseEventHandler(Line_MouseUp);
            this.MouseDown += new MouseEventHandler(Remove_MouseDown);
        }
        public void AddLine() {

            //setInitial();

            //line = true;
            //ellipse = false;
            //circle = false;
            arr.Add(new Points("line"));
            valueChanged = true;

            this.Refresh();

        }
        public void AddPoly() {
            actualPoly = new Points("polyline");
            actualPoly.points = new ArrayList();
            arr.Add(actualPoly);
            this.Refresh();
        }
        public void AddEllipse() {
            arr.Add(new Points("ellipse"));
            valueChanged = true;

            this.Refresh();

        }
        public void AddCircle() {
            arr.Add(new Points("circle"));
            valueChanged = true;

            this.Refresh();
        }


        //void Ellipse_MouseMove(object sender, MouseEventArgs e) {
        //    if (!isMouseDown) {
        //        if (e.X <= x2 + smallCircleSize
        //            && e.X >= x2 - smallCircleSize
        //            && e.Y <= y2 + smallCircleSize
        //            && e.Y >= y2 - smallCircleSize) {

        //            isMouseOverPoint2 = true;
        //            refresh2 = true;
        //            this.Refresh();
        //        } else {

        //            isMouseOverPoint2 = false;
        //            if (refresh2) {
        //                this.Refresh();
        //                refresh2 = false;
        //            }
        //        }
        //    } else {
        //         if (isMouseOverPoint2) {
        //            x2 = e.X;
        //            y2 = e.Y;
        //            this.Refresh();
        //        }
        //    }
        //}
        Points findNearest(MouseEventArgs e) {
            foreach (Points p in arr) {
                if (!p.polyline) {
                    if (e.X <= p.x0 + smallCircleSize + 10
                            && e.X >= p.x0 - smallCircleSize - 10
                            && e.Y <= p.y0 + smallCircleSize + 10
                            && e.Y >= p.y0 - smallCircleSize - 10) {
                        return p;
                    } else if (e.X <= p.x1 + smallCircleSize + 10
                            && e.X >= p.x1 - smallCircleSize - 10
                            && e.Y <= p.y1 + smallCircleSize + 10
                            && e.Y >= p.y1 - smallCircleSize - 10) {
                        return p;
                    } else if (e.X <= p.x2 + smallCircleSize + 10
                            && e.X >= p.x2 - smallCircleSize - 10
                            && e.Y <= p.y2 + smallCircleSize + 10
                            && e.Y >= p.y2 - smallCircleSize - 10) {
                        return p;
                    }
                }else{
                    foreach (Point point in p.points) {
                        if (e.X <= point.X + smallCircleSize + 10
                        && e.X >= point.X - smallCircleSize - 10
                        && e.Y <= point.Y + smallCircleSize + 10
                        && e.Y >= point.Y - smallCircleSize - 10)
                            return p;
                    }
                }
            }
            return null;
        }
        void Remove_MouseDown(object sender, MouseEventArgs e) {
            Points p = findNearest(e);
            arr.Remove(p);
            valueChanged = true;
            this.Refresh();
        }
        void Line_MouseMove(object sender, MouseEventArgs e) {
            //if (ellipse) {
            Points p = findNearest(e);

            if (p == null) return;
            if (p.polyline) {
                if (isMouseDown) {
                    foreach (Point point in p.points) {
                        if (e.X <= point.X + smallCircleSize + 10
                    && e.X >= point.X - smallCircleSize - 10
                    && e.Y <= point.Y + smallCircleSize + 10
                    && e.Y >= point.Y - smallCircleSize - 10) {
                            p.points[p.points.IndexOf(point)] = new Point(e.X, e.Y);
                            //point.Y=e.Y;
                            this.Refresh();
                            return;
                        }
                    }
                }
            }
            if (p.line||p.circle) {
                if (isMouseDown) {
                    if (e.X <= p.x0 + smallCircleSize + 10
                        && e.X >= p.x0 - smallCircleSize - 10
                        && e.Y <= p.y0 + smallCircleSize + 10
                        && e.Y >= p.y0 - smallCircleSize - 10) {
                        p.x0 = e.X;
                        p.y0 = e.Y;
                        this.Refresh();
                        return;
                    }
                    if (e.X <= p.x1 + smallCircleSize + 10
                        && e.X >= p.x1 - smallCircleSize - 10
                        && e.Y <= p.y1 + smallCircleSize + 10
                        && e.Y >= p.y1 - smallCircleSize - 10) {
                        p.x1 = e.X;
                        p.y1 = e.Y;
                        if (p.circle) {
                            p.R = Convert.ToInt32(Math.Sqrt(Math.Pow(p.x1 - p.x0, 2) + Math.Pow(p.y1 - p.y0, 2)));
                        }
                        this.Refresh();
                        return;
                    }
                }
            }
            if (p.ellipse) {
                if (isMouseDown) {
                    if (e.X <= p.x0 + smallCircleSize + 10
                        && e.X >= p.x0 - smallCircleSize - 10
                        && e.Y <= p.y0 + smallCircleSize + 10
                        && e.Y >= p.y0 - smallCircleSize - 10) {
                        p.x0 = e.X;
                        p.y0 = e.Y;
                        this.Refresh();
                        return;
                    }
                    if (e.X <= p.x1 + smallCircleSize + 10
                        && e.X >= p.x1 - smallCircleSize - 10
                        && e.Y <= p.y1 + smallCircleSize + 10
                        && e.Y >= p.y1 - smallCircleSize - 10) {
                        p.x1 = e.X;
                        p.y1 = e.Y;
                        p.a = Convert.ToInt32(Math.Sqrt(Math.Pow(p.x1 - p.x0, 2) + Math.Pow(p.y1 - p.y0, 2)));
                        this.Refresh();
                        return;
                    }
                    if (e.X <= p.x2 + smallCircleSize + 10
                        && e.X >= p.x2 - smallCircleSize - 10
                        && e.Y <= p.y2 + smallCircleSize + 10
                        && e.Y >= p.y2 - smallCircleSize - 10) {
                        p.x2 = e.X;
                        p.y2 = e.Y;
                        p.b = Convert.ToInt32(Math.Sqrt(Math.Pow(p.x2 - p.x0, 2) + Math.Pow(p.y2 - p.y0, 2)));
                        this.Refresh();
                        return;
                    }
                }
            }
             /*   if (!isMouseDown) {
                    if (e.X <= p.x2 + smallCircleSize + 10
                        && e.X >= p.x2 - smallCircleSize - 10
                        && e.Y <= p.y2 + smallCircleSize + 10
                        && e.Y >= p.y2 - smallCircleSize - 10) {

                        isMouseOverPoint2 = true;
                        refresh2 = true;
                        this.Refresh();
                    } else {

                        isMouseOverPoint2 = false;
                        if (refresh2) {
                            this.Refresh();
                            refresh2 = false;
                        }
                    }
                } else {
                    if (isMouseOverPoint2) {
                        p.x2 = e.X;
                        p.y2 = e.Y;
                        valueChanged = true;
                        this.Refresh();
                    }
                }

                //}

                if (!isMouseDown) {
                    if (e.X <= p.x0 + smallCircleSize + 10
                        && e.X >= p.x0 - smallCircleSize - 10
                        && e.Y <= p.y0 + smallCircleSize + 10
                        && e.Y >= p.y0 - smallCircleSize - 10) {

                        isMouseOverPoint0 = true;
                        refresh0 = true;
                        this.Refresh();
                    } else {

                        isMouseOverPoint0 = false;
                        if (refresh0) {
                            this.Refresh();
                            refresh0 = false;
                        }


                    }

                    if (e.X <= p.x1 + smallCircleSize + 10
                        && e.X >= p.x1 - smallCircleSize - 10
                        && e.Y <= p.y1 + smallCircleSize + 10
                        && e.Y >= p.y1 - smallCircleSize - 10) {

                        isMouseOverPoint1 = true;
                        refresh1 = true;
                        this.Refresh();
                    } else {
                        isMouseOverPoint1 = false;
                        if (refresh1) {
                            this.Refresh();
                            refresh1 = false;
                        }
                    }
                } else {
                    if (isMouseOverPoint0) {
                        p.x0 = e.X;
                        p.y0 = e.Y;
                        valueChanged = true;

                        this.Refresh();
                    } else if (isMouseOverPoint1) {
                        p.x1 = e.X;
                        p.y1 = e.Y;
                        valueChanged = true;

                        this.Refresh();
                    }
                }
            */
            }
        


        private void paintLine(Graphics g, Points p) {
            //if (!isMouseOverPoint0 && !isMouseOverPoint1) {
                Primitives.MidpointLine(p.x0, p.y0, p.x1, p.y1, Brushes.Black, 1, g);
                Primitives.MidpointCircle(p.x0, p.y0, smallCircleSize, Brushes.Red, 2, g);
                Primitives.MidpointCircle(p.x1, p.y1, smallCircleSize, Brushes.Red, 2, g);
            /*} else if (isMouseOverPoint0) {
                Primitives.MidpointLine(p.x0, p.y0, p.x1, p.y1, Brushes.Black, 1, g);
                Primitives.MidpointCircle(p.x0, p.y0, smallCircleSize, Brushes.Green, 2, g);
                Primitives.MidpointCircle(p.x1, p.y1, smallCircleSize, Brushes.Red, 2, g);
            } else if (isMouseOverPoint1) {
                Primitives.MidpointLine(p.x0, p.y0, p.x1, p.y1, Brushes.Black, 1, g);
                Primitives.MidpointCircle(p.x1, p.y1, smallCircleSize, Brushes.Green, 2, g);
                Primitives.MidpointCircle(p.x0, p.y0, smallCircleSize, Brushes.Red, 2, g);
            }*/
        }

        private void paintCircle(Graphics g, Points p) {

            //if (!isMouseOverPoint0 && !isMouseOverPoint1) {
            //    //p.R = Convert.ToInt32(Math.Sqrt(Math.Pow(p.x1 - p.x0, 2) + Math.Pow(p.y1 - p.y0, 2)));
            //    Primitives.MidpointCircle(p.x0, p.y0, p.R, Brushes.Black, 1, g);
            //    Primitives.MidpointCircle(p.x0, p.y0, smallCircleSize, Brushes.Red, 2, g);
            //    p.x1 = p.x0 + p.R;
            //    p.y1 = p.y0;
            //    Primitives.MidpointCircle(p.x1, p.y1, smallCircleSize, Brushes.Red, 2, g);
            //} else if (isMouseOverPoint0) {
                Primitives.MidpointCircle(p.x0, p.y0, p.R, Brushes.Black, 1, g);
                Primitives.MidpointCircle(p.x0, p.y0, smallCircleSize, Brushes.Red, 2, g);
                p.x1 = p.x0 + p.R;
                p.y1 = p.y0;
                Primitives.MidpointCircle(p.x1, p.y1, smallCircleSize, Brushes.Red, 2, g);
            //} else if (isMouseOverPoint1) {
            //    //p.R = Convert.ToInt32(Math.Sqrt(Math.Pow(p.x1 - p.x0, 2) + Math.Pow(p.y1 - p.y0, 2)));
            //    Primitives.MidpointCircle(p.x0, p.y0, p.R, Brushes.Black, 1, g);
            //    Primitives.MidpointCircle(p.x0, p.y0, smallCircleSize, Brushes.Red, 2, g);
            //    p.x1 = p.x0 + p.R;
            //    p.y1 = p.y0;
            //    Primitives.MidpointCircle(p.x1, p.y1, smallCircleSize, Brushes.Red, 2, g);
            //}
        }
        private void paintPoly(Graphics g, Points p) {
            Point prev = new Point(-100,-100);
            foreach (Point point in p.points) {
                Primitives.MidpointCircle(point.X, point.Y, smallCircleSize, Brushes.Red, 2, g);
                if (prev.Equals(new Point(-100, -100)) == true) {
                    prev = point;
                } else {
                    Primitives.MidpointLine(prev.X, prev.Y, point.X, point.Y, Brushes.Black, 1, g);
                    prev = point;
                }
            }
        }
        private void paintEllipse(Graphics g,Points p) {
            //if (!isMouseOverPoint0 && !isMouseOverPoint1 && !isMouseOverPoint2) {
              //  p.a = Convert.ToInt32(Math.Sqrt(Math.Pow(p.x1 - p.x0, 2) + Math.Pow(p.y1 - p.y0, 2)));
               // p.b = Convert.ToInt32(Math.Sqrt(Math.Pow(p.x2 - p.x0, 2) + Math.Pow(p.y2 - p.y0, 2)));
                Primitives.MidpointEllipse(p.x0, p.y0, p.a, p.b,Brushes.Black,1, g);
                Primitives.MidpointCircle(p.x0, p.y0, smallCircleSize, Brushes.Red, 2, g);

                p.x1 = p.x0 + p.a;
                p.y1 = p.y0;
                p.x2 = p.x0;
                p.y2 = p.y0 - p.b;
                Primitives.MidpointCircle(p.x1, p.y1, smallCircleSize, Brushes.Red, 2, g);
                Primitives.MidpointCircle(p.x2, p.y2, smallCircleSize, Brushes.Red, 2, g);
                
            //} else if (isMouseOverPoint0) {
            //    Primitives.MidpointEllipse(p.x0, p.y0, p.a, p.b, Brushes.Black, 1, g);
            //    Primitives.MidpointCircle(p.x0, p.y0, smallCircleSize, Brushes.Green, 2, g);
            //    p.x1 = p.x0 + p.a;
            //    p.y1 = p.y0;
            //    p.x2 = p.x0;
            //    p.y2 = p.y0 - p.b;
            //    Primitives.MidpointCircle(p.x1, p.y1, smallCircleSize, Brushes.Red, 2, g);
            //    Primitives.MidpointCircle(p.x2, p.y2, smallCircleSize, Brushes.Red, 2, g);

            //} else if (isMouseOverPoint1) {
            //    p.a = Convert.ToInt32(Math.Sqrt(Math.Pow(p.x1 - p.x0, 2) + Math.Pow(p.y1 - p.y0, 2)));
            //    Primitives.MidpointEllipse(p.x0, p.y0, p.a, p.b, Brushes.Black, 1, g);
            //    Primitives.MidpointCircle(p.x0, p.y0, smallCircleSize, Brushes.Red, 2, g);
            //    p.x1 = p.x0 + p.a;
            //    p.y1 = p.y0;
            //    p.x2 = p.x0;
            //    p.y2 = p.y0 - p.b;
            //    Primitives.MidpointCircle(p.x1, p.y1, smallCircleSize, Brushes.Green, 2, g);
            //    Primitives.MidpointCircle(p.x2, p.y2, smallCircleSize, Brushes.Red, 2, g);
            //} else if (isMouseOverPoint2) {
            //    p.b = Convert.ToInt32(Math.Sqrt(Math.Pow(p.x2 - p.x0, 2) + Math.Pow(p.y2 - p.y0, 2)));
            //    Primitives.MidpointEllipse(p.x0, p.y0, p.a, p.b, Brushes.Black, 1, g);
            //    Primitives.MidpointCircle(p.x0, p.y0, smallCircleSize, Brushes.Red, 2, g);
            //    p.x1 = p.x0 + p.a;
            //    p.y1 = p.y0;
            //    p.x2 = p.x0;
            //    p.y2 = p.y0 - p.b;
            //    Primitives.MidpointCircle(p.x1, p.y1, smallCircleSize, Brushes.Red, 2, g);
            //    Primitives.MidpointCircle(p.x2, p.y2, smallCircleSize, Brushes.Green, 2, g);
            //}
        }
         
        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);




            //cl.BackColor = Color.White;
            Graphics g = pe.Graphics;
            //Primitives.MidpointEllipse(50, 50, 20, 40, Brushes.Black, 2, g);
            // Primitives.ellipse(g,20,40, true);
            //if (valueChanged == false) return;
            foreach (Points p in arr) {
                if (p.line) {
                    paintLine(g, p);
                } else if (p.circle) {
                    paintCircle(g, p);
                } else if (p.ellipse) {
                    paintEllipse(g,p);
                } else if (p.polyline) {
                    paintPoly(g, p);
                }
            }
           // valueChanged = false;
                //.Show();

                //panel1.Controls.Add(cl);

            }


        }
    }

