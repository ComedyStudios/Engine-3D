using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EngineLib;
namespace Engine_3D
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WriteableBitmap _bmpLive;
        private Screen _screen;
        private readonly Image _image = new();
        private long _lastRenderTick;
        private const long MinDeltaTime = (long)1000 / 30;
        private readonly Label _label = new();
        private Input _input = new ();
        private Point _lastMousePosition = new Point(0, 0); 
        public MainWindow()
        {
            //SetSettings
            _input.MovementSpeed = 8;
            _image.Stretch = Stretch.None;
            _image.Margin = new Thickness(0);
            
            //create window and Start app
            InitializeComponent();
            MainGrid.Children.Add(_image);
            MainGrid.Children.Add(_label);
            
            RunApp(480  , 850);
        }

        private void RunApp(int windowHeight,int windowWidth)
        {
            _lastRenderTick = DateTime.Now.Ticks;
            _bmpLive = new WriteableBitmap(windowWidth, windowHeight, 96, 96, PixelFormats.Bgr32, null);
            _screen = new Screen(_bmpLive);
            _screen.Display(_bmpLive);
            _image.Source = _bmpLive;
            
             var t = new Thread(() => RenderContinuously());
             t.Start();
        }
        
        
        private void RenderContinuously()
        {
            long currentTickDelta = 0;
            while (true)
            {
                var pBackBuffer = IntPtr.Zero;
                int backBufferStride = 0;
                int bitsPerPixel = 32;
                int width = 0;
                int height = 0;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    //lock bitmap in ui thread
                    _bmpLive.Lock();
                    pBackBuffer = _bmpLive.BackBuffer;//Make pointer available to background thread
                    backBufferStride = _bmpLive.BackBufferStride;
                    bitsPerPixel = _bmpLive.Format.BitsPerPixel;
                    width = (int)_bmpLive.Width;
                    height = (int)_bmpLive.Height;
                    _input.DeltaTime = (float)currentTickDelta / 1000;

                });

                
                //Back to the worker thread
                _screen.DisplayUnsafe(pBackBuffer, backBufferStride, bitsPerPixel, width, height, _input);
                
                var currentTick = DateTime.Now.Ticks;
                currentTickDelta = (currentTick - _lastRenderTick)/ 10000;
                Application.Current.Dispatcher.Invoke(() =>
                {//UI thread does post update operations
                    _bmpLive.AddDirtyRect(new System.Windows.Int32Rect(0, 0, width, height));
                    _bmpLive.Unlock();
                    _label.Content = currentTickDelta.ToString() + " " +_input.MouseDeltaY + " " + _input.MouseDeltaX;
                });

                
                if (currentTickDelta  < MinDeltaTime)
                {
                    Thread.Sleep((int)MinDeltaTime);
                }
                
                _lastRenderTick = currentTick;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            var currMousePoint = e.GetPosition(this);
            _input.MouseDeltaX = currMousePoint.X - _lastMousePosition.X;
            _input.MouseDeltaY = (currMousePoint.Y - _lastMousePosition.Y);
            _lastMousePosition.Y = currMousePoint.Y;
            _lastMousePosition.X = currMousePoint.X;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                    _input.HorizontalMovement = -1;
                    break;
                case Key.D:
                    _input.HorizontalMovement = 1;
                    break;
                case Key.W:
                    _input.VerticalMovement = 1;
                    break;
                case Key.S:
                    _input.VerticalMovement = -1;
                    break;
                default:
                    break;
            }
        }
        
        protected override void OnKeyUp(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                    _input.HorizontalMovement = 0;
                    break;
                case Key.D:
                    _input.HorizontalMovement = 0;
                    break;
                case Key.W:
                    _input.VerticalMovement = 0;
                    break;
                case Key.S:
                    _input.VerticalMovement = 0;
                    break;
                default:
                    break;
            }
        }
        
    }
}