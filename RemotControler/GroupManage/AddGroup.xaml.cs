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

namespace RemotControler.GroupManage
{
    /// <summary>
    /// AddGroup.xaml 的交互逻辑
    /// </summary>
    public partial class AddGroup : Window
    {
        public AddGroup()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            ISvrDAL svrDAL = SvrDAL.Instance;
            var gList = svrDAL.GetGroupList();
            if (!gList.Contains(txtGroup.Text))
            {
                svrDAL.AddGroup(txtGroup.Text);
                MessageBox.Show("添加成功。", "Success");
                this.Close();
            }
            else
            {
                MessageBox.Show("抱歉，名称已存在！\n请修改名称后再添加。", "Wand");
            }
        }
    }
}
