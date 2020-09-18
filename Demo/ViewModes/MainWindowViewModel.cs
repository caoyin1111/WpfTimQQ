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
    [AddINotifyPropertyChangedInterface]
    public class MainWindowViewModel: BindableBase
    {
     

        public DelegateCommand<object> SelectItemChangedCommand { get; set; }
        public DelegateCommand ClickCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        public DelegateCommand AddCommand { get; set; }
        public AddWindow addWindow = null;
        public ObservableCollection<Friend> friends { get; set; } = new ObservableCollection<Friend>();
        public Friend friend { get; set; }


        public MainWindowViewModel()
        {


            //MemoryStream stream = new MemoryStream();
            //byte[] bt = null;
            //Properties.Resources.head1.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            //bt = stream.ToArray();
            //string connStr = "server=127.0.0.1;port=3306;user=root;password=1995107;database=qqfriend;Allow User Variables=True";
            //MySqlConnection con = new MySqlConnection(connStr);
            //try
            //{
            //    con.Open();
            //    string st = "insert into friend(Head,Nickname) value(@Head,@Nickname)";
            //    MySqlParameter mySqlParameter = new MySqlParameter("@Head", MySqlDbType.LongBlob);
            //    MySqlParameter mySqlParameter1 = new MySqlParameter("@Nickname", MySqlDbType.VarChar);
            //    mySqlParameter1.Value = "Go to hell";
            //    mySqlParameter.Value = bt;
            //    MySqlCommand mySqlCommand = new MySqlCommand(st, con);
            //    mySqlCommand.Parameters.Add(mySqlParameter);
            //    mySqlCommand.Parameters.Add(mySqlParameter1);
            //    mySqlCommand.ExecuteNonQuery();
            //}
            //catch (Exception e)
            //{

            //}
            //FriendServer friendServer = new FriendServer();

            //friends.Add(new Friend() { Nickname = "Go to hell", Head = Properties.Resources.head1 });
            //friends.Add(new Friend() { Nickname = "糖宝", Head = Properties.Resources.head2 });
            //friends.Add(new Friend() { Nickname = "胖虎", Head = Properties.Resources.head3 });
            //friends.Add(new Friend() { Nickname = "小花", Head = Properties.Resources.head4 });
            //friends.Add(new Friend() { Nickname = "隔壁老王", Head = Properties.Resources.head5 });
            //friends.Add(new Friend() { Nickname = "狗子", Head = Properties.Resources.head6 });
            //foreach (var item in friends)
            //{
            //    friendServer.Add(item);
            //}
            //friends = new List<Friend>();
            //friends.Add(new Friend() { Nickname = "Go to hell", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head1.jpg")) });
            //friends.Add(new Friend() { Nickname = "糖宝", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head2.jpg")) });
            //friends.Add(new Friend() { Nickname = "胖虎", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head3.jpg")) });
            //friends.Add(new Friend() { Nickname = "小花", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head4.jpg")) });
            //friends.Add(new Friend() { Nickname = "隔壁老王", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head5.jpg")) });
            //friends.Add(new Friend() { Nickname = "狗子", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head6.jpg")) });
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
        }
       

    }
}
