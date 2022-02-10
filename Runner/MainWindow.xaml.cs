using System.Windows;
using System.Windows.Controls;
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
        private Image _image = new Image();

        private Screen _screen = new Screen();
        public MainWindow()
        {
            //SetSettings
            _image.Stretch = Stretch.None;
            _image.Margin = new Thickness(0);
            
            //create window and Start app
            InitializeComponent();
            this.RunApp();
        }

        public void RunApp()
        {
            _image.Source = _screen.Display();
            MainGrid.Children.Add(_image);  
        }
    }
}