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
using AxMSTSCLib;
using MSTSCLib;
using RemotControler.Model;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace RemotControler
{
    /// <summary>
    /// RemoteWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RemoteWindow : Window
    {
        AxMsTscAxNotSafeForScripting rdp;
        public RemoteWindow()
        {
            InitializeComponent();
        }

        public RemoteWindow(Server_Data model)
        {
            InitializeComponent();
            Init(model);
        }

        public void Init(Server_Data model)
        {
            rdp = new AxMsTscAxNotSafeForScripting();
            winFormsContainer.Child = rdp;
            //this.WindowState = WindowState.Maximized;
            rdp.Width = model.Width > 0 ? model.Width : 800;
            rdp.Height = model.Height > 0 ? model.Height : 600;
            
            ((System.ComponentModel.ISupportInitialize)(rdp)).BeginInit(); 
            ((System.ComponentModel.ISupportInitialize)(rdp)).EndInit();

            rdp.Server = "192.168.9.20";
            rdp.UserName = "113064";
            IMsTscNonScriptable secured = (IMsTscNonScriptable)rdp.GetOcx();
            secured.ClearTextPassword = model.Pwd;
            rdp.Connect();
        }

        private void content_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            rdp.Width = (int)this.ActualWidth;
            rdp.Height = (int)this.ActualHeight;
            rdp.Refresh();

            ((System.ComponentModel.ISupportInitialize)(rdp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(rdp)).EndInit();
        }
    }
}
