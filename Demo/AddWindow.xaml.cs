using Demo.ViewModes;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Demo
{
    /// <summary>
    /// AddWindow.xaml 的交互逻辑
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
            Loaded += AddMainWindow_Loaded;


        }

        private void AddMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new AddWindowViewModel();

        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void dianji(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
