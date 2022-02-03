using System.Numerics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;

namespace EngineLib;

public class Screen
{

    private Scene mainScene;
    private Camera _mainCamera;

    public Screen()
    {
        mainScene = new Scene();
        _mainCamera = mainScene.MainCamera;
    }
    public WriteableBitmap Display()
    {
        var windowHeight = Camera.Height;
        var windowWidth = Camera.Width;
        WriteableBitmap wb = new WriteableBitmap((int)windowWidth, (int)windowHeight, 96, 96, PixelFormats.Bgr32, null);
        byte[] pixels = new byte[(int)wb.Width * (int)wb.Height * wb.Format.BitsPerPixel / 8];
        Int32Rect rect = new Int32Rect(0, 0, (int)wb.Width, (int)wb.Height);

        for (int x = 0; x < wb.Width; x++)
        {
            for (int y = 0; y < wb.Height; y++)
            {
                //these are values for test porposes delete later
                Vector3 rayDirection = mainScene.CameraToWorldCoordinate(x, y, _mainCamera);
                var ray = new Ray(_mainCamera.Position, rayDirection);
                
                int red = 0;
                int green = 0;
                int blue = 0;

                var hit = ray.RayCastHitAnyObject(mainScene);
                
                if (hit != null)
                {
                    //TODO: fix this
                    /*if (hit.SpotInShow(mainScene))
                    {
                        red = 0;
                        green = 0;
                        blue = 0;
                    }*/
                    /*else
                    {*/
                        red = hit.PixelColor.R;
                        green = hit.PixelColor.G;
                        blue = hit.PixelColor.B;
                    //}
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
        var stride = (int)(wb.Format.BitsPerPixel * windowWidth / 8) ;
        wb.WritePixels(rect, pixels, stride, 0);

        return wb;
    }
}