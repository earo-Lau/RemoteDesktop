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
using RemotControler.Extend;
using RemotControler.Model;
using RemotControler.ViewModel;

namespace RemotControler.ComputerManage
{
    /// <summary>
    /// EditSvr.xaml 的交互逻辑
    /// </summary>
    public partial class EditSvr : Window
    {
        Computer_ViewModel vm;
        public EditSvr(Server_Data model)
        {
            InitializeComponent();
            vm = new Computer_ViewModel(model);

            InitDetail();
        }

        private void InitDetail()
        {
            cbxColor.ItemsSource = Menu_Data.Color_List;
            txtSN.Text = vm.model.SN;
            txtPort.Text = vm.model.Port;
            txtUser.Text = vm.model.UserName;
            txtPwd.Password = PasswordHelper.DecodePwd(vm.model.Pwd);
            txtWidth.Text = vm.model.Width.ToString();
            txtHeight.Text = vm.model.Height.ToString();
            txtRemark.Text = vm.model.Remark;

            cbxColor.DisplayMemberPath = "Text";
            cbxColor.SelectedValuePath = "Value";
            cbxColor.SelectedValue = vm.model.Color.ToString();

            cbxGroup.ItemsSource = Menu_Data.Group_LIst;
            cbxGroup.DisplayMemberPath = "Text";
            cbxGroup.SelectedValuePath = "Value";
            cbxGroup.SelectedValue = vm.model.Group;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnComfirm_Click(object sender, RoutedEventArgs e)
        {
            Server_Data model = new Server_Data();
            model.SN = txtSN.Text;
            model.Port = txtPort.Text;
            model.UserName = txtUser.Text;
            model.Pwd = PasswordHelper.EncodePwd(txtPwd.Password);
            model.Group = cbxGroup.SelectedValue.ToString();
            model.Width = string.IsNullOrEmpty(txtWidth.Text) ? "0" : txtWidth.Text;
            model.Height = string.IsNullOrEmpty(txtHeight.Text) ? "0" : txtHeight.Text;
            model.Color = cbxColor.SelectedValue.ToString();
            model.Remark = txtRemark.Text;

            DAL.ISvrDAL svrDAL = DAL.SvrDAL.Instance;
            svrDAL.EditSvr(model, vm.model);
            MessageBox.Show("修改成功");
            this.Close();
        }
    }
}
