using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageConverterLibrary 
{
    public class Converter
    {
        public Bitmap Grayscale(string imagePath)
        {
            unsafe
            {
                Bitmap processedBitmap = (Bitmap)Bitmap.FromFile(imagePath);
                for (int i = 0; i < processedBitmap.Width; i++)
                {
                    for (int x = 0; x < processedBitmap.Height; x++)
                    {
                        Color oldColor = processedBitmap.GetPixel(i, x);
                        int grayScale = (int)((oldColor.R * 0.3) + (oldColor.G * 0.59) + (oldColor.B * 0.11));
                        Color newColor = Color.FromArgb(oldColor.A, grayScale, grayScale, grayScale);
                        processedBitmap.SetPixel(i, x, newColor);
                    }
                }

                return processedBitmap;
            }
        }

        public Bitmap ConvertRGBValue(string imagePath)
        {
            unsafe
            {
                Bitmap processedBitmap = (Bitmap)Bitmap.FromFile(imagePath);
                BitmapData bitmapData = processedBitmap.LockBits(new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height), ImageLockMode.ReadWrite, processedBitmap.PixelFormat);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, heightInPixels, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int oldBlue = currentLine[x];
                        int oldGreen = currentLine[x + 1];
                        int oldRed = currentLine[x + 2];

                        if (oldBlue > oldGreen && oldBlue > oldRed)
                            oldBlue = 255;
                        else if (oldGreen > oldRed && oldGreen > oldBlue)
                            oldGreen = 255;
                        else
                            oldRed = 255;

                        currentLine[x] = (byte)oldBlue;
                        currentLine[x + 1] = (byte)oldGreen;
                        currentLine[x + 2] = (byte)oldRed;
                    }
                });
                processedBitmap.UnlockBits(bitmapData);

                return processedBitmap;
            }
        }

        public BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                return bitmapimage;
            }
        }

        [DllImport("../../../Debug/ImageConverterLibraryCpp.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GrayscaleCpp(IntPtr bmap);

        public Bitmap GrayscaleCppExecute(string imagePath)
        {
            Bitmap processedBitmap = (Bitmap)Bitmap.FromFile(imagePath);
            IntPtr hBitmap = processedBitmap.GetHbitmap();
            processedBitmap = Image.FromHbitmap(GrayscaleCpp(hBitmap));
            return processedBitmap;
        }
    }
}
