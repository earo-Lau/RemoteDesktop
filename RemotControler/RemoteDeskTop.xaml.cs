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
        ObservableCollection<Server_Data> data = new ObservableCollection<Server_Data>();

        public RemoteDeskTop()
        {
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            Server_DataView vm = new Server_DataView();
            ISvrDAL svrHandler = SvrDAL.Instance;
            svrHandler.ReadSvr().ForEach(s => vm.model.Add(s));
            
            ICollectionView vw = CollectionViewSource.GetDefaultView(vm.model);
            vw.GroupDescriptions.Add(new PropertyGroupDescription("Group"));

            dataGrid.DataContext = vm;

            //this.dataGrid.PreviewMouseLeftButtonDown +=
            //    new MouseButtonEventHandler(dataGrid_PreviewMouseLeftButtonDown);
            ////The Drop Event
            //this.dataGrid.Drop += new DragEventHandler(dataGrid_Drop);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var s = dataGrid.SelectedItem as Server_Data;
            RemoteWindow remote = new RemoteWindow(s);
            remote.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            AddSvr window = new AddSvr();
            window.Show();
        }

        #region Drag&Drop

        int prevRowIndex = -1;
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
            vm.model.RemoveAt(prevRowIndex);
            movedSvr.Group = group;
            vm.model.Insert(index, movedSvr);
        }

        void dataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            prevRowIndex = GetDataGridItemCurrentRowIndex(e.GetPosition);

            if (prevRowIndex < 0)
                return;
            dataGrid.SelectedIndex = prevRowIndex;

            Server_Data selectedSvr = dataGrid.Items[prevRowIndex] as Server_Data;

            if (selectedSvr == null)
                return;

            //Now Create a Drag Rectangle with Mouse Drag-Effect
            //Here you can select the Effect as per your choice

            DragDropEffects dragdropeffects = DragDropEffects.Move;

            if (DragDrop.DoDragDrop(dataGrid, selectedSvr, dragdropeffects)
                                != DragDropEffects.None)
            {
                //Now This Item will be dropped at new location and so the new Selected Item
                dataGrid.SelectedItem = selectedSvr;
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
            if (dataGrid.ItemContainerGenerator.Status
                    != GeneratorStatus.ContainersGenerated)
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

    }
}
