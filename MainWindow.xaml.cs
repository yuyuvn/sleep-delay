using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SleepTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Timer(Properties.Settings.Default.DefaultDealy);
        }

        public void Run(object sender, EventArgs e)
        {
            ((Timer)DataContext).Toogle();
            WindowState = WindowState.Minimized;
        }

        public void Reset(object sender, EventArgs e)
        {
            ((Timer)DataContext).Reset();
        }

        public void Plus1Minutes(object sender, EventArgs e)
        {
            ((Timer)DataContext).Plus(1);
        }

        public void Plus5Minutes(object sender, EventArgs e)
        {
            ((Timer)DataContext).Plus(5);
        }

        public void Plus10Minutes(object sender, EventArgs e)
        {
            ((Timer)DataContext).Plus(10);
        }

        public void Minute1Minutes(object sender, EventArgs e)
        {
            ((Timer)DataContext).Plus(-1);
        }

        public void Minute5Minutes(object sender, EventArgs e)
        {
            ((Timer)DataContext).Plus(-5);
        }

        public void Minute10Minutes(object sender, EventArgs e)
        {
            ((Timer)DataContext).Plus(-10);
        }

        public void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                this.Run(sender, e);
            }
        }

        private void WindowMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
