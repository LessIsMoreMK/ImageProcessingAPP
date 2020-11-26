using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageConverterLibrary
{
    public class ConvertRGB
    {
        public BitmapImage ConvertRGBValue(string ImagePath)
        {
            unsafe
            {
                Bitmap processedBitmap = (Bitmap)Bitmap.FromFile(ImagePath);
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

                return BitmapToImageSource(processedBitmap);
            }
        }

        private BitmapImage BitmapToImageSource(Bitmap bitmap)
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

        public Task<BitmapImage> ConvertRGBValueAsync(string ImagePath)
        {
            return Task.Run(() => ConvertRGBValue(ImagePath));
        }
    }
}
