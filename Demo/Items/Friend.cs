using Prism.Mvvm;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Demo.Items
{
    [AddINotifyPropertyChangedInterface]
    public class Friend: BindableBase
    {

        //private BitmapImage head;

        //public BitmapImage Head
        //{
        //    get { return head; }
        //    set { SetProperty(ref head, value); }
        //}
        private Bitmap head;
        public Bitmap Head
        {
            get { return head; }
            set { SetProperty(ref head, value); }
        }


        private string nickname;
        public string Nickname
        {
            get { return nickname; }
            set { SetProperty(ref nickname, value); }
        }

        public Friend()
        {
           

        }
    }
}
