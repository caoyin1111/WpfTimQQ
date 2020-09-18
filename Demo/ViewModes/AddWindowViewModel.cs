using Demo.Items;
using Demo.sql;
using Prism.Commands;
using Prism.Mvvm;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Demo.ViewModes
{
    [AddINotifyPropertyChangedInterface]
    class AddWindowViewModel : BindableBase
    {
        public DelegateCommand CloseAddCommand { get; set; }
        public DelegateCommand ClickCommand { get; set; }
        public ObservableCollection<Friend> fri { get; set; } = new ObservableCollection<Friend>();
        public AddWindowViewModel()
        {
            FriendServer friendServer = new FriendServer();
            DataTable table = friendServer.GetTable();
            foreach (DataRow item in table.Rows)
            {
                using (MemoryStream memory = new MemoryStream((byte[])item["Head"]))
                {

                    Bitmap bitmap = (Bitmap)System.Drawing.Image.FromStream(memory);
                    BitmapSource source = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    fri.Add(new Friend() { Nickname = item["Nickname"].ToString(), Head = source });
                }

            }
        }
    }
    
    
}
