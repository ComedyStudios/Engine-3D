using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using EngineLib;

namespace Engine_3D
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WriteableBitmap _bmpLive;
        private Screen _screen = new Screen();
        private Image _image = new Image();
        private long LastRenderTick;
        private long MinDeltaTime  = (long)1000/60;

        public MainWindow()
        {
            //SetSettings
            _image.Stretch = Stretch.None;
            _image.Margin = new Thickness(0);
            
            //create window and Start app
            InitializeComponent();
            MainGrid.Children.Add(_image);
            RunApp(720, 1280);
        }

        private void RunApp(int windowHeight,int windowWidth)
        {
            LastRenderTick = DateTime.Now.Ticks;
            _bmpLive = new WriteableBitmap(windowWidth, windowWidth, 96, 96, PixelFormats.Bgr32, null);
            _screen.Display(_bmpLive);
            _image.Source = _bmpLive;
            
             var t = new Thread(() => RenderContinuously());
             t.Start();
        }
        

        private void RenderContinuously()
        {
            while (true)
            {
                var pBackBuffer = IntPtr.Zero;
                int backBufferStride = 0;
                int bitsPerPixel = 32;
                int width = 0;
                int height = 0;
                Application.Current.Dispatcher.Invoke(() =>
                {//lock bitmap in ui thread
                    _bmpLive.Lock();
                    pBackBuffer = _bmpLive.BackBuffer;//Make pointer available to background thread
                    backBufferStride = _bmpLive.BackBufferStride;
                    bitsPerPixel = _bmpLive.Format.BitsPerPixel;
                    width = (int)_bmpLive.Width;
                    height = (int)_bmpLive.Height;
                });
                
                //Back to the worker thread
                _screen.DisplayUnsafe(pBackBuffer, backBufferStride, bitsPerPixel, width, height);
                
                Application.Current.Dispatcher.Invoke(() =>
                {//UI thread does post update operations
                    _bmpLive.AddDirtyRect(new System.Windows.Int32Rect(0, 0, width, height));
                    _bmpLive.Unlock();
                });

                var currentTick = DateTime.Now.Ticks;
                if (currentTick- LastRenderTick < MinDeltaTime)
                {
                    
                    Thread.Sleep((int)MinDeltaTime);

                }
                LastRenderTick = currentTick;
                
            }
        }
    }
}