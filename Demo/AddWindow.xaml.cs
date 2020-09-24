using Demo.Items;
using Demo.sql;
using Demo.ViewModes;
using Prism.Mvvm;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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
        public AddWindowViewModel addWindowViewModel { get; set; }
        public ObservableCollection<Friend> nFriend { get; set; } = new ObservableCollection<Friend>();
        public AddWindow()
        {
            addWindowViewModel = new AddWindowViewModel();
            InitializeComponent();
            Loaded += AddMainWindow_Loaded;

        }

        private void AddMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = addWindowViewModel;

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
        public Friend searchFriend { get; set; } = new Friend();
        private void Search(object sender, RoutedEventArgs e)
        {
           
       
       

        }
    }
}
