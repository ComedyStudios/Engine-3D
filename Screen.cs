using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Engine_3D;

public class Screen
{
    public WriteableBitmap Display(int WindowWidth, int WindowHeight)
    {
      
        
        
        WriteableBitmap wb = new WriteableBitmap(WindowWidth, WindowHeight, 96, 96, PixelFormats.Bgr32, null);
        byte[] pixels = new byte[(int)wb.Width * (int)wb.Height * wb.Format.BitsPerPixel / 8];
        Int32Rect rect = new Int32Rect(0, 0, (int)wb.Width, (int)wb.Height);

        for (int x = 0; x < wb.Width; x++)
        {
            for (int y = 0; y < wb.Height; y++)
            {
                int alpha = 0;
                int red = 0;
                int green = 0;
                int blue = 255;

                int pixelOffset = (x + y * wb.PixelWidth) * wb.Format.BitsPerPixel / 8;
                pixels[pixelOffset] = (byte)blue;
                pixels[pixelOffset + 1] = (byte)green;
                pixels[pixelOffset + 2] = (byte)red;
                pixels[pixelOffset + 3] = (byte)alpha;
                
            }
        }
        int stride = (wb.Format.BitsPerPixel * WindowWidth) / 8 ;
        wb.WritePixels(rect, pixels, stride, 0);

        return wb;
        //MainGrid.Children.Add(image);
        //image.Source = wb;
    }
}