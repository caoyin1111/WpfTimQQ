using Demo.Logs;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Demo
{
    /// <summary>
    /// CaptureWindow.xaml 的交互逻辑
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class CaptureWindow : Window
    {
        Bitmap screenSnapshot { get; set; }
        public System.Windows.Point FirstPoint = new System.Windows.Point();
        public System.Windows.Point ForntPoint = new System.Windows.Point();
        BitmapSource bSource;
        /// <summary>
        /// 最小移动距离
        /// </summary>
        private double MinMoveDistance = 5;
        public CaptureWindow()
        {
            InitializeComponent();
            screenSnapshot = GetScreenSnapshot();
            bSource= ToBitmapSource(screenSnapshot);
            bSource.Freeze();
            xxgrid.Background = new ImageBrush(bSource);
         
        }
        public BitmapSource ToBitmapSource(Bitmap bmp)
        {
            BitmapSource returnSource;
            returnSource = Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            return returnSource;
        }
        public Bitmap GetScreenSnapshot()
        {
            try
            {
                System.Drawing.Rectangle rectangle = SystemInformation.VirtualScreen;
                var bitmap = new Bitmap(rectangle.Width, rectangle.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                using(Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(rectangle.X, rectangle.Y, 0, 0, rectangle.Size, CopyPixelOperation.SourceCopy);
                }
                return bitmap;
            }
            catch(Exception ex)
            {
                
            }
            return null;
        }

        //public Rect GetCurrentForImageRect()
        //{
        //    return getRectForImage(GetCurrentRect());
        //}

        public System.Windows.Media.Brush CurrentBrush { get; set; } = new SolidColorBrush(Colors.White);
        public System.Windows.Media.Pen pen = new System.Windows.Media.Pen(System.Windows.Media.Brushes.Red,1);
        private void  DrawSqluarel(DrawingVisual visual)
        {
            using(DrawingContext dc = visual.RenderOpen())
            {
                dc.DrawRectangle(CurrentBrush,pen, new Rect(FirstPoint, ForntPoint));
                dc.Close();
            }
        }
        private bool IsDrag = false;
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (grid.visuals.Count > 0)
            {
                grid.DeleteVisual(grid.visuals[0]);
            }
            IsDrag = true;
            ForntPoint = e.GetPosition(this.gg);
            FirstPoint = e.GetPosition(this.gg);
            if (ForntPoint != FirstPoint)
            {
                DrawingVisual dv = new DrawingVisual();
                DrawSqluarel(dv);
                grid.AddVisual(dv);
            }
    

        }

        private void Canvas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            IsDrag = false;
          
        }

        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(IsDrag)
            {
                System.Windows.Point current = e.GetPosition(gg);
                Vector vector = System.Windows.Point.Subtract(current, ForntPoint);
                double distance = Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2));
                if (distance > MinMoveDistance)
                {
                    this.ForntPoint = current;
                    ForntPoint = current;
                    DrawingVisual ds = new DrawingVisual();
                    DrawSqluarel(ds);
                    grid.AddVisual(ds);


                }
               
            }

        }
     
  
        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            IsDrag = false;
        }

        private void Canvas_Wheel(object sender, MouseWheelEventArgs e)
        {
       

        }
        private Rect GetCurrentRect()
        {
            return new Rect(FirstPoint.X+5, FirstPoint.Y+30
                , ForntPoint.X - FirstPoint.X, ForntPoint.Y - FirstPoint.Y);
         
        }
        public Bitmap CopyFromScreenSnapshot(Rect region)
        {
         
            var sourceRect = ToRectangle(region);
            var destRect = new System.Drawing.Rectangle(new System.Drawing.Point(),
                    sourceRect.Size);

            if (screenSnapshot != null)
            {
                var bitmap = new Bitmap(sourceRect.Width, sourceRect.Height);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.DrawImage(screenSnapshot, destRect, sourceRect, System.Drawing.GraphicsUnit.Pixel);
                }
                return bitmap;
            }

            return null;
        }
        public System.Drawing.Rectangle ToRectangle(Rect rect)
        {
            return new System.Drawing.Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
        }
        /// <summary>
        /// 获取当前的框选相对于图像的区域
        /// </summary>
        /// <returns></returns>
        public Rect GetCurrentForImageRect()
        {
            return getRectForImage(GetCurrentRect());
        }
        private Rect getRectForImage(Rect rect)
        {
            System.Windows.Size controlSize = this.RenderSize;
            return new Rect(rect.X / controlSize.Width * bSource.Width,
                rect.Y / controlSize.Height * bSource.Height, rect.Width / controlSize.Width * bSource.Width
                , rect.Height / controlSize.Height * bSource.Height);
        }
        private void Capture_Click(object sender, RoutedEventArgs e)
        {
            Bitmap bb = CopyFromScreenSnapshot(GetCurrentForImageRect());

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "模板图片|*.jpg";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bb.Save(saveFileDialog.FileName);
            }
            string name = System.IO.Path.GetFileName(saveFileDialog.FileName);

        }

        private void CaptureWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            screenSnapshot = GetScreenSnapshot();
            bSource = ToBitmapSource(screenSnapshot);
            bSource.Freeze();
            xxgrid.Background = new ImageBrush(bSource);

        }
    }
}
