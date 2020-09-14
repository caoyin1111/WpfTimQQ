using Demo.Items;
using Prism.Commands;
using Prism.Mvvm;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Demo.ViewModes
{
    [AddINotifyPropertyChangedInterface]
    class AddWindowViewModel : BindableBase
    {
        public DelegateCommand CloseAddCommand { get; set; }
        public DelegateCommand ClickCommand { get; set; }
        public ObservableCollection<Friend> friend2;
        public ObservableCollection<Friend> Friend2
        {
            get { return friend2; }
            set { SetProperty(ref friend2, value); }
        }
        public AddWindowViewModel()
        {
            //friend2 = new ObservableCollection<Friend>();
            //friends = new List<Friend>();
            //friend2.Add(new Friend() { Nickname = "Go to hell", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head1.jpg")) });
            //friend2.Add(new Friend() { Nickname = "糖宝", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head2.jpg")) });
            //friend2.Add(new Friend() { Nickname = "胖虎", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head3.jpg")) });
            //friend2.Add(new Friend() { Nickname = "小花", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head4.jpg")) });
            //friend2.Add(new Friend() { Nickname = "隔壁老王", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head5.jpg")) });
            //friend2.Add(new Friend() { Nickname = "狗子", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head6.jpg")) });
            //ClickCommand = new DelegateCommand(() =>
            //{
            //    MessageBox.Show("你好");
            //});
        }
    }
    
}
