using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Win11Toolbar
{
    internal static class CustomGraphics
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
        public static GraphicsPath RoundedRect(Rectangle bounds, int TopLeft, int TopRight, int BottomRight, int BottomLeft)
        {
            int diameter = 0;
            Size size = new Size();
            Rectangle arc = new Rectangle();
            GraphicsPath path = new GraphicsPath();

            if (TopLeft > 0)
            {
                diameter = TopLeft * 2;
                size = new Size(diameter, diameter);
                arc = new Rectangle(bounds.Location, size);
                // top left arc  
                path.AddArc(arc, 180, 90);
            }
            else if (TopLeft == -1) { }
            else
            {
                Point start = new Point(bounds.Left, bounds.Top);
                Point end = new Point(bounds.Right - TopRight - 5, bounds.Top);
                path.AddLine(start, end);
            }

            if (TopRight > 0)
            {
                diameter = TopRight * 2;
                size = new Size(diameter, diameter);
                arc = new Rectangle(bounds.Location, size);
                // top right arc  
                arc.X = bounds.Right - diameter;
                path.AddArc(arc, 270, 90);
            }
            else if (TopRight == -1) { }
            else
            {
                Point start = new Point(bounds.Right, bounds.Top);
                Point end = new Point(bounds.Right, bounds.Bottom - BottomRight - 5);
                path.AddLine(start, end);
            }

            if (BottomRight > 0)
            {
                diameter = BottomRight * 2;
                size = new Size(diameter, diameter);
                arc = new Rectangle(bounds.Location, size);
                // bottom right arc  
                arc.Y = bounds.Bottom - diameter;
                path.AddArc(arc, 0, 90);
            }
            else if (BottomRight == -1) { }
            else
            {
                Point start = new Point(bounds.Right, bounds.Bottom);
                Point end = new Point(bounds.Left - BottomLeft - 5, bounds.Bottom);
                path.AddLine(start, end);
            }

            if (BottomLeft > 0)
            {
                diameter = BottomLeft * 2;
                size = new Size(diameter, diameter);
                arc = new Rectangle(bounds.Location, size);
                // bottom left arc 
                arc.X = bounds.Left;
                path.AddArc(arc, 90, 90);
            }
            else if (BottomLeft == -1) { }
            else
            {
                Point start = new Point(bounds.Left, bounds.Bottom);
                Point end = new Point(bounds.Left, bounds.Top - BottomLeft - 5);
                path.AddLine(start, end);
            }

            path.CloseFigure();
            return path;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="pen"></param>
        /// <param name="bounds"></param>
        /// <param name="cornerRadius"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null)
                throw new ArgumentNullException(nameof(graphics));
            if (pen == null)
                throw new ArgumentNullException(nameof(pen));

            using (GraphicsPath path = RoundedRect(bounds, cornerRadius))
            {
                graphics.DrawPath(pen, path);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="pen"></param>
        /// <param name="bounds"></param>
        /// <param name="TopLeft"></param>
        /// <param name="TopRight"></param>
        /// <param name="BottomRight"></param>
        /// <param name="BottomLeft"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, Rectangle bounds, int TopLeft, int TopRight, int BottomRight, int BottomLeft)
        {
            if (graphics == null)
                throw new ArgumentNullException(nameof(graphics));
            if (pen == null)
                throw new ArgumentNullException(nameof(pen));

            using (GraphicsPath path = RoundedRect(bounds, TopLeft, TopRight, BottomRight, BottomLeft))
            {
                graphics.DrawPath(pen, path);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="pen"></param>
        /// <param name="bounds"></param>
        /// <param name="Sides">new Padding(Left,Top,Right,Bottom)</param>
        /// <param name="CornerRadii">new Padding(TopLeft,TopRight,BottomRight,BottomLeft)</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void DrawPartialRoundedBorder(this Graphics graphics, Pen pen, Rectangle bounds, Padding Sides, Padding CornerRadii)
        {
            if (graphics == null)
                throw new ArgumentNullException(nameof(graphics));
            if (pen == null)
                throw new ArgumentNullException(nameof(pen));


            int TopLeft = CornerRadii.Left;
            int TopRight = CornerRadii.Top;
            int BottomRight = CornerRadii.Right;
            int BottomLeft = CornerRadii.Bottom;

            int TL = TopLeft < 0 ? 0 : TopLeft;
            int BL = BottomLeft < 0 ? 0 : BottomLeft;
            int TR = TopRight < 0 ? 0 : TopRight;
            int BR = BottomRight < 0 ? 0 : BottomRight;

            Debug.WriteLine($"Sides: {Sides.ToString()}");
            Debug.WriteLine($"CornerRadii: {CornerRadii.ToString()}");

            int diameter = 0;
            Size size = new Size();
            Rectangle arc = new Rectangle();

            //
            // Top Left Corner and Top Side
            //
            Debug.WriteLine($"TopLeft:{TopLeft} - Top:{Sides.Top}");
            if (TopLeft > 0)
            {
                Debug.WriteLine("Drawing TopLeft Corner");
                diameter = TopLeft * 2;
                size = new Size(diameter, diameter);
                arc = new Rectangle(bounds.Location, size);
                // top left arc  
                graphics.DrawArc(pen, arc, 180, 90);
            }
            if (Sides.Top > -1)
            {
                Debug.WriteLine("Drawing Top Side");
                Point start = new Point(bounds.Left - TL, bounds.Top);
                Point end = new Point(bounds.Right - TR, bounds.Top);
                graphics.DrawLine(pen, start, end);
            }

            //
            // Top Right Corner and Right Side
            //
            Debug.WriteLine($"TopRight:{TopRight} - Right:{Sides.Right}");
            if (TopRight > 0)
            {
                Debug.WriteLine("Drawing TopRight Corner");
                diameter = TopRight * 2;
                size = new Size(diameter, diameter);
                arc = new Rectangle(bounds.Location, size);
                // top right arc  
                arc.X = bounds.Right - diameter;
                graphics.DrawArc(pen, arc, 270, 90);
            }
            if (Sides.Right > -1)
            {
                Debug.WriteLine("Drawing Right Side");
                Point start = new Point(bounds.Right, bounds.Top - TR);
                Point end = new Point(bounds.Right, bounds.Bottom - BR);
                graphics.DrawLine(pen, start, end);
            }

            //
            // Bottom Right Corner and Bottom Side
            //
            Debug.WriteLine($"BottomRight:{BottomRight} - Bottom:{Sides.Bottom}");
            if (BottomRight > 0)
            {
                Debug.WriteLine("Drawing BottomRight Corner");
                diameter = BottomRight * 2;
                size = new Size(diameter, diameter);
                arc = new Rectangle(bounds.Location, size);
                // bottom right arc  
                arc.Y = bounds.Bottom - diameter;
                graphics.DrawArc(pen, arc, 0, 90);
            }
            if (Sides.Bottom > -1)
            {
                Debug.WriteLine("Drawing Bottom Side");
                Point start = new Point(bounds.Right - BR, bounds.Bottom);
                Point end = new Point(bounds.Left - BL, bounds.Bottom);
                graphics.DrawLine(pen, start, end);
            }

            //
            // Bottom Left Corner and Left Side
            //
            Debug.WriteLine($"BottomLeft:{BottomLeft} - Left:{Sides.Left}");
            if (BottomLeft > 0)
            {
                Debug.WriteLine("Drawing BottomLeft Corner");
                diameter = BottomLeft * 2;
                size = new Size(diameter, diameter);
                arc = new Rectangle(bounds.Location, size);
                // bottom left arc 
                arc.X = bounds.Left;
                graphics.DrawArc(pen, arc, 90, 90);
            }
            if (Sides.Left > -1)
            {
                Debug.WriteLine("Drawing Left Side");
                Point start = new Point(bounds.Left, bounds.Bottom - BL);
                Point end = new Point(bounds.Left, bounds.Top - TL);
                graphics.DrawLine(pen, start, end);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="brush"></param>
        /// <param name="bounds"></param>
        /// <param name="cornerRadius"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null)
                throw new ArgumentNullException(nameof(graphics));
            if (brush == null)
                throw new ArgumentNullException(nameof(brush));

            using (GraphicsPath path = RoundedRect(bounds, cornerRadius))
            {
                graphics.FillPath(brush, path);
            }
        }
        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle bounds, int TopLeft, int TopRight, int BottomRight, int BottomLeft)
        {
            if (graphics == null)
                throw new ArgumentNullException(nameof(graphics));
            if (brush == null)
                throw new ArgumentNullException(nameof(brush));

            using (GraphicsPath path = RoundedRect(bounds, TopLeft, TopRight, BottomRight, BottomLeft))
            {
                graphics.FillPath(brush, path);
            }


        }
    }
}
