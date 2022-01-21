using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Engine_3D
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int WindowHeight = 720;
        private static int WindowWidth = 1280;
        private WriteableBitmap? WriteableBitmap;
        public MainWindow()
        {
            InitializeComponent();
            DrawPixels();
        }

        private void DrawPixels()
        {
            WriteableBitmap = new WriteableBitmap(WindowWidth, WindowHeight, 96, 96, PixelFormats.Bgr32, null);

            for (int x = 0; x < WriteableBitmap.Width; x++)
            {
                for (int y = 0; y < WriteableBitmap.Height; y++)
                {
                    int alpha = 255;
                    int red = 0;
                    int green = 0;
                    int blue = 255;

                    byte[] colorData = {(byte) blue, (byte) green, (byte) red, (byte) alpha};
                    
                    Int32Rect rect = new Int32Rect(x, y, 1, 1);
                    int stride = (WriteableBitmap.Format.BitsPerPixel * WindowWidth) / 8 ;
                    WriteableBitmap.WritePixels(rect, colorData, stride, 0);
                }
            }
            
            
            // Update writeable bitmap with the colorArray to the image.
            

            // Create an Image to display the bitmap.
            Image image = new Image();
            image.Stretch = Stretch.None;
            image.Margin = new Thickness(0);

            MainGrid.Children.Add(image);

            //Set the Image source.
            image.Source = WriteableBitmap;
        }
    }
}