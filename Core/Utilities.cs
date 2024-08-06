using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Win11Toolbar
{
    internal class Utilities
    {
        internal abstract class Win11Theme
        {
            public abstract Color Accent { get; }
            public abstract Color Background { get; }
            public abstract Color Highlight { get; }
            public abstract Color Text { get; }
        }
        public class Dark : Win11Theme
        {
            public override Color Accent { get; } = Color.FromArgb(76, 194, 255);
            public override Color Background { get; } = Color.FromArgb(26, 34, 31);
            public override Color Highlight { get; } = Color.FromArgb(38, 45, 52);
            public override Color Text { get; } = Color.FromArgb(203, 205, 206);
        }
        public class Light : Win11Theme
        {
            public override Color Accent { get; } = Color.FromArgb(76, 194, 255);
            public override Color Background { get; } = Color.FromArgb(26, 34, 31);
            public override Color Highlight { get; } = Color.FromArgb(38, 45, 52);
            public override Color Text { get; } = Color.FromArgb(203, 205, 206);
        }

        public static Win11Theme GetTheme()
        {
            return new Dark();
        }

        /// <summary>
        /// Struct representing a point.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        /// <summary>
        /// Retrieves the cursor's position, in screen coordinates.
        /// </summary>
        /// <see>See MSDN documentation for further information.</see>
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            // NOTE: If you need error handling
            // bool success = GetCursorPos(out lpPoint);
            // if (!success)

            return lpPoint;
        }



        //
        // ##############################
        // Do a bunch of Icon conversions
        // ##############################
        //             STATIC
        // ##############################

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Size"></param>
        /// <returns></returns>
        public static Bitmap Exe2Icon2Bitmap(Size Size)
        {
            throw new NotImplementedException();
            Icon ico = Icon.ExtractAssociatedIcon(@"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe");
            Console.WriteLine($"Exe Icon Size: {Size}");
            Icon ico48 = new Icon(ico, Size);
            return ico48.ToBitmap();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Filename"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public static Image Exe2Icon2Image(string Filename, Size Size)
        {
            try
            {
                Icon ico = Icon.ExtractAssociatedIcon(Filename);
                //logger.LogTrace($"Exe Icon Size: {Size}");
                Icon ico48 = new Icon(ico, Size);
                return Utilities.Resize(ico48.ToBitmap(), Size);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Argument error: {Filename}\n{e}");
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="icon"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public static Image Icon2Image(Icon icon, Size Size)
        {
            try
            {
                //logger.LogTrace($"Exe Icon Size: {Size}");
                Icon ico48 = new Icon(icon, Size);
                return Utilities.Resize(ico48.ToBitmap(),Size);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e}");
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcImage"></param>
        /// <returns></returns>
        public static Icon Bitmap2Icon(Image srcImage)
        {
            Bitmap bitmap = (Bitmap)srcImage;
            return Icon.FromHandle(bitmap.GetHicon());
        }
        /// <summary>
        /// Resize an Image to a specific Size.
        /// </summary>
        /// <param name="srcImage"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public static Image Resize(Image srcImage, Size Size)
        {
            try
            {
                Bitmap newImage = new Bitmap(Size.Width, Size.Height);
                using (Graphics graphics = Graphics.FromImage((Image)newImage))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.DrawImage((Image)srcImage, new Rectangle(0, 0, Size.Width, Size.Height));
                }
                return (Image)newImage;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to convert image to Size: {Size}\n{e}");
            }
            return null;
        }
        /// <summary>
        /// Load an image from the filesystem and resize to a specific Size.
        /// </summary>
        /// <param name="Filepath"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public static Image Resize(string Filepath, Size Size)
        {
            try
            {
                using (Image srcImage = Image.FromFile(Filepath))
                {
                    return Utilities.Resize(srcImage, Size);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to convert image at {Filepath} to Size: {Size}\n{e}");
            }
            return null;
        }
        /// <summary>
        /// Resize an Image to a specific Height and Width.
        /// </summary>
        /// <param name="srcImage"></param>
        /// <param name="Height"></param>
        /// <param name="Width"></param>
        /// <returns></returns>
        public static Image Resize(Bitmap srcImage, int Height, int Width)
        {
            try
            {
                return Utilities.Resize(srcImage, new Size(Height, Width));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to convert image to Size: H:{Height} W:{Width}\n{e}");
            }
            return null;

        }
        /// <summary>
        /// Load an image from the filesystem and resize to a specific Height and Width.
        /// </summary>
        /// <param name="Filepath"></param>
        /// <param name="Height"></param>
        /// <param name="Width"></param>
        /// <returns></returns>
        public static Image Resize(string Filepath, int Height, int Width)
        {
            try
            {
                using (Image srcImage = Image.FromFile(Filepath))
                {
                    return Utilities.Resize(srcImage, new Size(Height, Width));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to convert image at {Filepath} to Size: H: {Height}  W: {Width}\n{e}");
            }
            return null;
        }

        public static Image Blur(Image image, Int32 blurSize)
        {
            Blur(ref image, new Rectangle(0, 0, image.Width, image.Height), blurSize);
            return image;
        }
        private static void Blur(ref Image image, Rectangle rectangle, Int32 blurSize)
        {
            Bitmap blurred = new Bitmap(image.Width, image.Height);

            // make an exact copy of the bitmap provided
            using (Graphics graphics = Graphics.FromImage(blurred))
                graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                    new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

            // look at every pixel in the blur rectangle
            for (int xx = rectangle.X; xx < rectangle.X + rectangle.Width; xx++)
            {
                for (int yy = rectangle.Y; yy < rectangle.Y + rectangle.Height; yy++)
                {
                    int avgR = 0, avgG = 0, avgB = 0;
                    int blurPixelCount = 0;

                    // average the color of the red, green and blue for each pixel in the
                    // blur size while making sure you don't go outside the image bounds
                    for (int x = xx; (x < xx + blurSize && x < image.Width); x++)
                    {
                        for (int y = yy; (y < yy + blurSize && y < image.Height); y++)
                        {
                            Color pixel = blurred.GetPixel(x, y);

                            avgR += pixel.R;
                            avgG += pixel.G;
                            avgB += pixel.B;

                            blurPixelCount++;
                        }
                    }

                    avgR = avgR / blurPixelCount;
                    avgG = avgG / blurPixelCount;
                    avgB = avgB / blurPixelCount;

                    // now that we know the average for the blur size, set each pixel to that color
                    for (int x = xx; x < xx + blurSize && x < image.Width && x < rectangle.Width; x++)
                        for (int y = yy; y < yy + blurSize && y < image.Height && y < rectangle.Height; y++)
                            blurred.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                }
            }

            image = (Image) blurred;
        }
    }
    
}
