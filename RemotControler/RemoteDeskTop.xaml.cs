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
using System.Collections.ObjectModel;
using RemotControler.Model;
using System.ComponentModel;
using RemotControler.ViewModel;
using AxMSTSCLib;
using MSTSCLib;

namespace RemotControler
{
    /// <summary>
    /// RemoteDeskTop.xaml 的交互逻辑
    /// </summary>
    public partial class RemoteDeskTop : Page
    {
        ObservableCollection<Server_Data> data = new ObservableCollection<Server_Data>();

        public RemoteDeskTop()
        {
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            Server_DataView vm = new Server_DataView();
            for (int i = 0; i < 100; i++)
            {
                Server_Data m = new Server_Data()
                {
                    Group = (i / 2).ToString(),
                    SN = Guid.NewGuid().ToString(),
                    UserName = "RX-" + i,
                    IsSelect = i % 2 == 0,
                    Pwd = "earo123456"
                };
                vm.model.Add(m);
            }
            ICollectionView vw = CollectionViewSource.GetDefaultView(vm.model);
            vw.GroupDescriptions.Add(new PropertyGroupDescription("Group"));

            dataGrid.DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var s = dataGrid.SelectedItem as Server_Data;
            RemoteWindow remote = new RemoteWindow(s);
            remote.Show();
        }
    }
}
