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
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Demo.ViewModes
{
    [AddINotifyPropertyChangedInterface]
    public class AddWindowViewModel : BindableBase
    {
        public string text { get; set; }
        public ContentControl page { get; set; }
        public DelegateCommand CloseAddCommand { get; set; }
        public DelegateCommand ClickCommand { get; set; }
        public ObservableCollection<Friend> fri { get; set; } = new ObservableCollection<Friend>();
        public ObservableCollection<Friend> newfri { get; set; } = new ObservableCollection<Friend>();
        public AddUserControl addUserControl { get; set; } = new AddUserControl();
        public ListBoxControl listControl { get; set; } = new ListBoxControl();
        public DelegateCommand SearchCommand { get; set; }
        public AddWindowViewModel()
        {
            page = listControl;
            addUserControl.model = this;
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
            SearchCommand = new DelegateCommand(() =>
             {
                 newfri.Clear();
                 page = addUserControl;
                 FriendServer se = new FriendServer();
                 DataTable tabel = se.GetData(text.ToString());
                 for (int i = 0; i < tabel.Rows.Count; i++)
                 {
                     Friend fg = new Friend();
                     fg.Nickname = tabel.Rows[i]["Nickname"].ToString();
                     using (MemoryStream memory = new MemoryStream((byte[])tabel.Rows[i]["Head"]))
                     {
                         Bitmap bitmap = (Bitmap)System.Drawing.Image.FromStream(memory);
                         BitmapSource source = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                         fg.Head = source;
                     }
                     newfri.Add(fg);
                 }

             });
  

        }

    }
    
    
}
