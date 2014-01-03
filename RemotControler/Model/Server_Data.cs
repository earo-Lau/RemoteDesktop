using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemotControler.ViewModel;

namespace RemotControler.Model
{
    [Serializable]
    public class Server_Data : ViewModelBase
    {
        #region LoginInfo
        private string _sn;
        private string _port;
        private string _userName;
        private string _pwd;
        private string _status;
        private string _remark;

        public string SN
        {
            get { return _sn; }
            set { _sn = value; }
        }

        public string Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        #endregion

        #region Extend
        private string _group;

        public string Group
        {
            get { return _group; }
            set { _group = value; }
        }

        private bool _isSelect;

        public bool IsSelect
        {
            get { return _isSelect; }
            set
            {
                _isSelect = value;
                OnPropertyChanged("IsSelect");
            }
        }

        private int _width;
        private int _height;

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }
        #endregion
    }
}
