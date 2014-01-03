using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemotControler.Model;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace RemotControler.ViewModel
{
    public class Server_DataView
    {
        private DelegateCommand _selectOrUnSelectAll;
        private DelegateCommand _connected;
        public ObservableCollection<Server_Data> model { get; set; }

        public Server_DataView()
        {
            model = new ObservableCollection<Server_Data>();
        }

        #region Command
        public ICommand SelectOrUnSelectAll
        {
            get
            {
                if (_selectOrUnSelectAll == null)
                {
                    _selectOrUnSelectAll = new DelegateCommand();
                    _selectOrUnSelectAll.ExecuteCommand = new Action<object>(SelectAll);
                }
                return _selectOrUnSelectAll;
            }
        }
        #endregion

        #region PrivateMethod
        private bool isSelectFlag = false;
        protected void SelectAll(object obj)
        {
            if (model != null && isSelectFlag == false)
            {
                for (int i = 0; i < model.Count; i++)
                {
                    model[i].IsSelect = true;
                }
                isSelectFlag = true;
            }
            else if (model != null && isSelectFlag == true)
            {
                for (int i = 0; i < model.Count; i++)
                {
                    model[i].IsSelect = false;
                }
                isSelectFlag = false;
            }
        }
        #endregion
    }
}
