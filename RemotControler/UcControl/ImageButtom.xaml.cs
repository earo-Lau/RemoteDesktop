using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RemotControler.UcControl
{
    /// <summary>
    /// ImageButtom.xaml 的交互逻辑
    /// </summary>
    public partial class ImageButtom : UserControl
    {
        public ImageButtom()
        {
            InitializeComponent();
        }

        ////为自定义控件注册一个属性,ImageSource来作为填充在按钮中的图片
        public static readonly DependencyProperty ImageSourceProperty;
        //    DependencyProperty.Register("ImageSource", typeof(BitmapSource), typeof(ImageButtom),
        //    new FrameworkPropertyMetadata(new PropertyChangedCallback(OnSourceImageChange)));
        public static BitmapSource ImageSource
        {
            get
            {
                return (BitmapSource)GetValue(ImageSourceProperty);
            }
            set
            {
                SetValue(ImageSourceProperty, value);
            }
        }

    }
}
