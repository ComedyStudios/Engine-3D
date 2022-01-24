using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace Engine_3D;

public class Screen
{

    private Scene mainScene;
    private Cam mainCam;

    public Screen()
    {
        mainScene = new Scene();
        mainCam = mainScene.mainCam;
    }
    public WriteableBitmap Display(int WindowWidth, int WindowHeight)
    {
        WriteableBitmap wb = new WriteableBitmap(WindowWidth, WindowHeight, 96, 96, PixelFormats.Bgr32, null);
        byte[] pixels = new byte[(int)wb.Width * (int)wb.Height * wb.Format.BitsPerPixel / 8];
        Int32Rect rect = new Int32Rect(0, 0, (int)wb.Width, (int)wb.Height);

        for (int x = 0; x < wb.Width; x++)
        {
            for (int y = 0; y < wb.Height; y++)
            {
                //these are values for test porposes delete later
                Vector3 rayDirection = mainCam.direction;
                var ray = new Ray(mainCam.center, rayDirection);
                
                int red = 0;
                int green = 0;
                int blue = 0;
                
                if (ray.RayCastHit(mainScene))
                {
                    red = 0;
                    green = 0;
                    blue = 0;
                }
                else
                {
                    red = 0;
                    green = 0;
                    blue = 0;
                }
                

                int pixelOffset = (x + y * wb.PixelWidth) * wb.Format.BitsPerPixel / 8;
                pixels[pixelOffset] = (byte)blue;
                pixels[pixelOffset + 1] = (byte)green;
                pixels[pixelOffset + 2] = (byte)red;
                pixels[pixelOffset + 3] = 255;
            }
        }
        int stride = (wb.Format.BitsPerPixel * WindowWidth) / 8 ;
        wb.WritePixels(rect, pixels, stride, 0);

        return wb;
    }
}