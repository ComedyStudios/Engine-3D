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
        private static int WindowHeight;
        private static int WindowWidth;
        private WriteableBitmap WriteableBitmap;
        public MainWindow()
        {
            InitializeComponent();
            DrawBackground();
        }

        private void DrawBackground()
        {
            WindowWidth = (int) MainGrid.Width;
            WindowHeight = (int) MainGrid.Height;
            WriteableBitmap = new WriteableBitmap(WindowWidth, WindowHeight, 96, 96, PixelFormats.Bgr32, null);
            byte[, ,] pixels = new byte[WindowHeight, WindowWidth, 4];
            for (int x = 0;x< WriteableBitmap.Width ;x++)
            {
                for (int y = 0;y< WriteableBitmap.Width;y++)
                {
                    for (int i = 0; i < 3; i++)
                        pixels[y, x, i] = 0;
                    pixels[y, x, 3] = 255;
                }
            }
            
            byte[] pixels1d = new byte[WindowHeight * WindowWidth * 4];
            int index = 0;
            for (int row = 0; row < WindowHeight; row++)
            {
                for (int col = 0; col < WindowWidth; col++)
                {
                    for (int i = 0; i < 4; i++)
                        pixels1d[index++]= pixels[row, col, i];
                }
            }
            // Update writeable bitmap with the colorArray to the image.
            Int32Rect rect = new Int32Rect(0, 0, WindowWidth, WindowHeight);
            int stride = 4 * WindowWidth;
            WriteableBitmap.WritePixels(rect, pixels1d, stride, 0);

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