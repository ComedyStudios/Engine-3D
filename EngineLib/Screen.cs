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
    private Color BackgroundColor = Color.Blue; 

    /// <summary>
    /// construcktor of the Class
    /// </summary>
    public Screen(WriteableBitmap wb)
    {
        mainScene = new Scene();
        _mainCamera = new Camera(0, 0, -20, 90, (int)wb.Width, (int)wb.Height);
    }
    public void Display(WriteableBitmap wb)
    {
        byte[] pixels = new byte[(int)wb.Width * (int)wb.Height * wb.Format.BitsPerPixel / 8];
        Int32Rect rect = new Int32Rect(0, 0, (int)wb.Width, (int)wb.Height);
    
        for (int x = 0; x < wb.Width; x++)
        {
            for (int y = 0; y < wb.Height; y++)
            {
                Vector3 rayDirection = _mainCamera.CameraToWorldCoordinate(x, y);
                var ray = new Ray(_mainCamera.Position, rayDirection);
            
                int red = 0;
                int green = 0;
                int blue = 0;
    
                var hit = ray.RayCastAndShade(mainScene,3);
            
                if (hit != null)
                {
                    red = hit.PixelColor.R;
                    green = hit.PixelColor.G;
                    blue = hit.PixelColor.B;
                }
                else
                {
                    red = BackgroundColor.R;
                    green = BackgroundColor.G;
                    blue = BackgroundColor.B;
                }
            
                int pixelOffset = (x + y * wb.PixelWidth) * wb.Format.BitsPerPixel / 8;
                pixels[pixelOffset] = (byte)blue;
                pixels[pixelOffset + 1] = (byte)green;
                pixels[pixelOffset + 2] = (byte)red;
                pixels[pixelOffset + 3] = 255;
            }
        }
        var stride = (int)(wb.Format.BitsPerPixel * wb.Width / 8) ;
        wb.WritePixels(rect, pixels, stride, 0);
    }
    
    public void DisplayUnsafe(IntPtr buffer, int stride, int bitsPerPixel, int width, int height, Input input)
    {
       _mainCamera.MoveCamera(input);
        unsafe
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector3 rayDirection = _mainCamera.CameraToWorldCoordinate(x, y);
                    var ray = new Ray(_mainCamera.Position, rayDirection);
                
                    int red = 0;
                    int green = 0;
                    int blue = 0;

                    var hit = ray.RayCastAndShade(mainScene,3);
                
                    if (hit != null)
                    {
                        red = hit.PixelColor.R;
                        green = hit.PixelColor.G;
                        blue = hit.PixelColor.B;
                    }
                    else
                    {
                        red = BackgroundColor.R;
                        green = BackgroundColor.G;
                        blue = BackgroundColor.B;
                    }
                
                    // Compute the pixel's color.
                    int colorData = 255 << 24; // alfa
                    colorData |= (byte)red << 16; // R
                    colorData |= (byte)green << 8;   // G
                    colorData |= (byte)blue << 0;   // B
                    
                    int pixelOffset = y * stride + x * bitsPerPixel / 8;
                    *(int*)(buffer + pixelOffset) = colorData;
                }
            }
        }
    }
}