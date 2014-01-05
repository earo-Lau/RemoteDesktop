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
using RemotControler.Extend;

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
            try
            {
                rdp = new AxMSTSCLib.AxMsRdpClient2();
                this.WindowState = WindowState.Maximized;
                rdp.Dock = DockStyle.Fill;
                winFormsContainer.Child = rdp;
                ((System.ComponentModel.ISupportInitialize)(rdp)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(rdp)).EndInit();

                int width = 0;
                int height = 0;
                int.TryParse(model.Width, out width);
                int.TryParse(model.Height, out height);

                rdp.DesktopWidth = width > 0 ? width : Screen.PrimaryScreen.Bounds.Width;
                rdp.DesktopHeight = height >0 ? height: Screen.PrimaryScreen.Bounds.Height;

                rdp.Server = model.SN;
                if (!string.IsNullOrEmpty(model.Port))
                {
                    rdp.AdvancedSettings2.RDPPort = int.Parse(model.Port);
                }
                rdp.UserName = model.UserName;
                rdp.AdvancedSettings2.ClearTextPassword = PasswordHelper.DecodePwd(model.Pwd);
                int color = 0;
                int.TryParse(model.Color, out color);
                rdp.ColorDepth = color;
                rdp.FullScreen = false;
                rdp.Connect();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("设置有误，请修改远程计算机的设置。", "Error");
            }
        }
    }
}
