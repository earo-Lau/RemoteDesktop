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
using RemotControler.DAL;
using System.Windows.Controls.Primitives;
using RemotControler.ComputerManage;

namespace RemotControler
{
    public delegate Point GetDragDropPosition(IInputElement theElement);

    /// <summary>
    /// RemoteDeskTop.xaml 的交互逻辑
    /// </summary>
    public partial class RemoteDeskTop : Page
    {
        Server_DataView vm;

        public RemoteDeskTop()
        {
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            vm = new Server_DataView();
            LoadModel();

            ICollectionView vw = CollectionViewSource.GetDefaultView(vm.model);
            vw.GroupDescriptions.Add(new PropertyGroupDescription("Group"));

            dataGrid.DataContext = vm;

            //this.dataGrid.PreviewMouseLeftButtonDown +=new MouseButtonEventHandler(dataGrid_PreviewMouseLeftButtonDown);
            ////The Drop Event
            //this.dataGrid.Drop += new DragEventHandler(dataGrid_Drop);

            LoadGroup();
        }

        private void LoadModel()
        {
            vm.model.Clear();
            ISvrDAL svrDAL = SvrDAL.Instance;
            svrDAL.ReadSvr().ForEach(s => vm.model.Add(s));
        }

        private void LoadGroup()
        {
            IList<MenuItem> gMenu = new List<MenuItem>();
            foreach (var g in Menu_Data.Group_LIst)
            {
                MenuItem item = new MenuItem();
                item.Header = g.Text;

                item.Click += MoveGroup_Click_1;
                gMenu.Add(item);
            }
            moveGroup.ItemsSource = gMenu;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var s = dataGrid.SelectedItem as Server_Data;
            RemoteWindow remote = new RemoteWindow(s);
            remote.Show();
        }

        private void MoveGroup(string group, Server_Data model)
        {
            var clone = model.Clone() as Server_Data;
            clone.Group = group;

            ISvrDAL svrDAL = SvrDAL.Instance;
            svrDAL.EditSvr(clone, model);

            LoadModel();
        }

        #region Drag&Drop

        int prevRowIndex = -1;
        bool isClick = false, isDrag = false;
        /// <summary>
        /// Defines the Drop Position based upon the index.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dataGrid_Drop(object sender, DragEventArgs e)
        {
            if (prevRowIndex < 0)
                return;

            int index = this.GetDataGridItemCurrentRowIndex(e.GetPosition);

            //The current Rowindex is -1 (No selected)
            if (index < 0)
                return;
            //If Drag-Drop Location are same
            if (index == prevRowIndex)
                return;
            //If the Drop Index is the last Row of DataGrid(
            // Note: This Row is typically used for performing Insert operation)
            if (index == dataGrid.Items.Count)
            {
                MessageBox.Show("This row-index cannot be used for Drop Operations");
                return;
            }

            Server_DataView vm = dataGrid.DataContext as Server_DataView;

            Server_Data movedSvr = vm.model[prevRowIndex];
            string group = vm.model[index].Group;

            MoveGroup(group, movedSvr);
        }

        void dataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            prevRowIndex = GetDataGridItemCurrentRowIndex(e.GetPosition);

            if (prevRowIndex < 0)
                return;
            dataGrid.SelectedIndex = prevRowIndex;

            isClick = true;
        }

        private void dataGrid_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (isClick)
            {
                isDrag = true;

                Server_Data selectedSvr = dataGrid.Items[prevRowIndex] as Server_Data;

                if (selectedSvr == null)
                    return;

                //Now Create a Drag Rectangle with Mouse Drag-Effect
                //Here you can select the Effect as per your choice
                DragDropEffects dragdropeffects = DragDropEffects.Move;

                if (DragDrop.DoDragDrop(dataGrid, selectedSvr, dragdropeffects) != DragDropEffects.None)
                {
                    //Now This Item will be dropped at new location and so the new Selected Item
                    dataGrid.SelectedItem = selectedSvr;
                    //selectedSvr.IsSelect = selectedSvr.IsSelect ? false : true;
                    isClick = false;
                    isDrag = false;
                }
            }
        }

        private void dataGrid_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            if (isClick)
            {
                isClick = false;
            }
        }

        /// <summary>
        /// Method checks whether the mouse is on the required Target
        /// Input Parameter (1) "Visual" -> Used to provide Rendering support to WPF
        /// Input Paraneter (2) "User Defined Delegate" positioning for Operation
        /// </summary>
        /// <param name="theTarget"></param>
        /// <param name="pos"></param>
        /// <returns>The "Rect" Information for specific Position</returns>
        private bool IsTheMouseOnTargetRow(Visual theTarget, GetDragDropPosition pos)
        {
            Rect posBounds = VisualTreeHelper.GetDescendantBounds(theTarget);
            Point theMousePos = pos((IInputElement)theTarget);
            return posBounds.Contains(theMousePos);
        }

        /// <summary>
        /// Returns the selected DataGridRow
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private DataGridRow GetDataGridRowItem(int index)
        {
            if (dataGrid.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                return null;

            return dataGrid.ItemContainerGenerator.ContainerFromIndex(index)
                                                            as DataGridRow;
        }

        /// <summary>
        /// Returns the Index of the Current Row.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private int GetDataGridItemCurrentRowIndex(GetDragDropPosition pos)
        {
            int curIndex = -1;
            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                DataGridRow itm = GetDataGridRowItem(i);
                if (IsTheMouseOnTargetRow(itm, pos))
                {
                    curIndex = i;
                    break;
                }
            }
            return curIndex;
        }
        #endregion

        #region Computer_Mange
        private void AddSvr_Click_1(object sender, RoutedEventArgs e)
        {
            AddSvr window = new AddSvr();
            window.ShowDialog();
            LoadModel();
        }

        private void EditSvr_Click_1(object sender, RoutedEventArgs e)
        {
            var vm = dataGrid.DataContext as Server_DataView;
            var svrList = vm.model.Where(svr => svr.IsSelect).ToList();
            foreach (var svr in svrList)
            {
                EditSvr window = new EditSvr(svr);
                window.ShowDialog();
            }
            LoadModel();
        }

        private void DeleteSvr_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("确定删除选择的计算机？", "确定删除", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                var vm = dataGrid.DataContext as Server_DataView;
                var svrList = vm.model.Where(svr => svr.IsSelect).ToList();
                foreach (var svr in svrList)
                {
                    ISvrDAL svrDAL = SvrDAL.Instance;
                    svrDAL.DeleteSvr(svr);
                }

                LoadModel();
            }
        }
        #endregion

        #region Group_Manage
        private void AddGroup_Click_1(object sender, RoutedEventArgs e)
        {
            GroupManage.AddGroup window = new GroupManage.AddGroup();
            window.ShowDialog();
            LoadModel();
            LoadGroup();
        }

        private void RenameGroup_Click_1(object sender, RoutedEventArgs e)
        {
            GroupManage.RenameGroup window = new GroupManage.RenameGroup();
            window.ShowDialog();
            LoadModel();
            LoadGroup();
        }

        private void DeleteGroup_Click_1(object sender, RoutedEventArgs e)
        {
            GroupManage.DeleteGroup window = new GroupManage.DeleteGroup();
            window.ShowDialog();
            LoadModel();
            LoadGroup();
        }
        #endregion

        #region ContextMenu
        private void DeleteThis_Click_1(object sender, RoutedEventArgs e)
        {
            Server_Data svr = this.dataGrid.SelectedItem as Server_Data;
            if (svr == null)
                return;

            if (MessageBox.Show("确定删除选择的计算机？", "确定删除", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                ISvrDAL svrDAL = SvrDAL.Instance;
                svrDAL.DeleteSvr(svr);

                LoadModel();
            }
        }

        private void MoveGroup_Click_1(object sender, RoutedEventArgs e)
        {
            var group = ((MenuItem)sender).Header.ToString();
            Server_Data svr = dataGrid.SelectedItem as Server_Data;
            if (svr != null)
            {
                MoveGroup(group, svr);
            }
        }

        #endregion


    }
}
