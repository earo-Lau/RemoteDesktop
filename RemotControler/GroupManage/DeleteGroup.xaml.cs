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
    /// DeleteGroup.xaml 的交互逻辑
    /// </summary>
    public partial class DeleteGroup : Window
    {
        public DeleteGroup()
        {
            InitializeComponent();

            cbxGroup.ItemsSource = Menu_Data.Group_LIst;
            cbxGroup.DisplayMemberPath = "Text";
            cbxGroup.SelectedValuePath = "Value";
            cbxGroup.SelectedIndex = 0;
        }

        private void btnConfirm_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("确定删除选择的群组？\n删除群组将会同时删除该群组内的计算机。", "确定删除", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                ISvrDAL svrDAL = SvrDAL.Instance;
                svrDAL.DeleteGroup(cbxGroup.SelectedValue.ToString());

                MessageBox.Show("删除成功。", "Success");
                this.Close();
            }
        }
    }
}
