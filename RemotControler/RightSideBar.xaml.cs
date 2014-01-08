using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RemotControler
{
    struct Advertis
    {
        public string url;
        public string img;
    }

    /// <summary>
    /// RightSideBar.xaml 的交互逻辑
    /// </summary>
    public partial class RightSideBar : Page
    {
        Advertis[] ads = new Advertis[3];
        private static int ad_Index = 0;
        Thread oThread;
        public RightSideBar()
        {
            InitializeComponent();
            ads[0] = new Advertis() { url = "http://www.baidu.com", img = "Contents/Image/bdlogo.gif" };
            ads[1] = new Advertis() { url = "http://www.google.com", img = "Contents/Image/glogo.jpg" };
            ads[2] = new Advertis() { url = "http://www.taobao.com", img = "Contents/Image/tblogo.jpg" };

            oThread = new Thread(new System.Threading.ThreadStart(ChangedAds));
            oThread.Start();
        }

        private void hyperLink_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(ads[ad_Index].url);
        }

        private void ChangedAds()
        {
            while (MainWindow.isRun)
            {
                if (ad_Index < 2)
                    ad_Index++;
                else
                    ad_Index = 0;
                BitmapImage bitmap = new BitmapImage(new Uri(ads[ad_Index].img, UriKind.Relative));
                bitmap.Freeze();

                Dispatcher.BeginInvoke((Action)(() =>
                {
                    imgAD.Source = bitmap;
                    imgAD.Stretch = Stretch.UniformToFill;
                }));
                //var tThread = System.Threading.Thread.CurrentThread;
                System.Threading.Thread.Sleep(3000);
            }
        }

        private void Page_Unloaded_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
