using Demo.Items;
using Demo.sql;
using MySql.Data.MySqlClient;
using Prism.Commands;
using Prism.Mvvm;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Demo.ViewModes
{
    //绑定两种方式，一是AddINotifyPropertyChangedInterface,二是自定义基类BindableBase
    [AddINotifyPropertyChangedInterface]
    public class MainWindowViewModel: BindableBase
    {
     

        public DelegateCommand<object> SelectItemChangedCommand { get; set; }
        public DelegateCommand ClickCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand CaptureCommand { get; set; }
        public AddWindow addWindow = null;
        public ObservableCollection<Friend> friends { get; set; } = new ObservableCollection<Friend>();
        public Friend friend { get; set; }


        public MainWindowViewModel()
        {

            FriendServer friendServer = new FriendServer();
            DataTable table = friendServer.GetTable();
            foreach(DataRow item in table.Rows)
            {
                using (MemoryStream memory = new MemoryStream((byte[])item["Head"]))
                {
             
                    Bitmap bitmap = (Bitmap)System.Drawing.Image.FromStream(memory); 
                    BitmapSource source = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    friends.Add(new Friend() { Nickname = item["Nickname"].ToString(), Head = source });
                }
                    
            }
            CloseCommand = new DelegateCommand(()=> {

                Application.Current.Shutdown();

           });

            SelectItemChangedCommand = new DelegateCommand<object>((p)=>{

              

                ListView lv = p as ListView;
                friend = lv.SelectedItem as Friend;
             
                //Head= friend.Head;
                //Nickname = friend.Nickname;
            });
            AddCommand = new DelegateCommand(() =>
            {

                addWindow = new AddWindow();
                addWindow.ShowDialog();


            });
            ClickCommand = new DelegateCommand(() =>
            {
                MessageBox.Show("你好");
            });
            CaptureCommand = new DelegateCommand(() =>
            {
                CaptureWindow captureWindow = new CaptureWindow();
                captureWindow.ShowDialog();
          

            });
        }
       

    }
}
