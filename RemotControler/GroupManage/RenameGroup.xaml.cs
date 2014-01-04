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
using RemotControler.DAL;
using RemotControler.Model;

namespace RemotControler.GroupManage
{
    /// <summary>
    /// RenameGroup.xaml 的交互逻辑
    /// </summary>
    public partial class RenameGroup : Window
    {
        public RenameGroup()
        {
            InitializeComponent();

            cbxGroup.ItemsSource = Menu_Data.Group_LIst;
            cbxGroup.DisplayMemberPath = "Text";
            cbxGroup.SelectedValuePath = "Value";
            cbxGroup.SelectedIndex = 0;
        }

        private void btnConfirm_Click_1(object sender, RoutedEventArgs e)
        {
            ISvrDAL svrDAL = SvrDAL.Instance;
            svrDAL.RenameGroup(txtName.Text, cbxGroup.SelectedValue.ToString());

            MessageBox.Show("修改成功。", "Success");
            this.Close();
        }
    }
}
