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
using System.Windows.Shapes;
using RemotControler.Model;

namespace RemotControler.ComputerManage
{
    /// <summary>
    /// AddSvr.xaml 的交互逻辑
    /// </summary>
    public partial class AddSvr : Window
    {
        
        public AddSvr()
        {
            InitializeComponent();
            cbxColor.ItemsSource = Menu_Data.Color_List;
            cbxColor.DisplayMemberPath = "Text";
            cbxColor.SelectedValuePath = "Value";
            cbxColor.SelectedValue = "32";
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnComfirm_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
