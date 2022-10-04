using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows;

namespace TestBook.BL
{
    public class ConvertImage
    {
        public static string BitmapToBase64(Bitmap img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap bit = new Bitmap(img))
                {
                    bit.Save(ms, ImageFormat.Jpeg);
                }
                var convertToBase64 = Convert.ToBase64String(ms.GetBuffer());

                return convertToBase64;
            }
        }

        public static Bitmap Byte64ToImg(string im)
        {
            byte[] imageBytes = Convert.FromBase64String(im);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            ms.Write(imageBytes, 0, imageBytes.Length);
            var img = System.Drawing.Image.FromStream(ms);
            var bitImage = new Bitmap(img);
            return bitImage;
        }

        public static ImageSource BitpamToImageSource(Bitmap image)
        {
            var handle = image.GetHbitmap();
            return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        public static Bitmap ImageSourceToBitMap(ImageSource image)
        {
            BitmapSource m = (BitmapSource)image;

            Bitmap bmp = new System.Drawing.Bitmap(m.PixelWidth, m.PixelHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb); // Pit: Select Format32bppRgb will not have transparency

            BitmapData data = bmp.LockBits(
            new Rectangle(System.Drawing.Point.Empty, bmp.Size), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            m.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride);
            bmp.UnlockBits(data);

            return bmp;
        }
    }
}
