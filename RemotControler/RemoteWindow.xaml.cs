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
        protected AxMSTSCLib.AxMsRdpClient2 rdp;
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
            rdp = new AxMSTSCLib.AxMsRdpClient2();
            this.WindowState = WindowState.Maximized;
            rdp.Dock = DockStyle.Fill;
            winFormsContainer.Child = rdp;
            ((System.ComponentModel.ISupportInitialize)(rdp)).BeginInit(); 
            ((System.ComponentModel.ISupportInitialize)(rdp)).EndInit();

            rdp.DesktopHeight = Screen.PrimaryScreen.Bounds.Height;
            rdp.DesktopWidth = Screen.PrimaryScreen.Bounds.Width;
            rdp.Server = "103.27.126.234";
            rdp.AdvancedSettings2.RDPPort = 3427;
            rdp.UserName = "Administrator";
            rdp.AdvancedSettings2.ClearTextPassword = "D17A9B67A059B171";
            rdp.ColorDepth = 16;
            rdp.FullScreen = false;
            rdp.Connect();
        }
    }
}
