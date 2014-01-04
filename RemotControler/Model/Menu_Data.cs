using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemotControler.DAL;

namespace RemotControler.Model
{
    public class Menu_Data
    {
        #region ColorList
        private static IList<Item_Data> _color_List;

        public static IList<Item_Data> Color_List
        {
            get
            {
                if (_color_List == null) InitColorList();
                return _color_List;
            }
        }

        private static void InitColorList()
        {
            _color_List = new List<Item_Data>();
            _color_List.Add(new Item_Data() { Text = "增强色(15 bit)", Value = "15" });
            _color_List.Add(new Item_Data() { Text = "增强色(16 bit)", Value = "16" });
            _color_List.Add(new Item_Data() { Text = "真色彩(24 bit)", Value = "24" });
            _color_List.Add(new Item_Data() { Text = "最高质量(32 bit)", Value = "32" });
        }
        #endregion

        #region GroupList
        public static IList<Item_Data> Group_LIst
        {
            get
            {
                return GetGroupList(); 
            }
        }

        private static IList<Item_Data> GetGroupList()
        {
            ISvrDAL svr = SvrDAL.Instance;
            var list = svr.GetGroupList().Select(g => new Item_Data() { Text = g, Value = g }).ToList();
            return list;
        }

        #endregion

        public class Item_Data
        {
            private string _text;
            private string _value;

            public string Value
            {
                get { return _value; }
                set { _value = value; }
            }

            public string Text
            {
                get { return _text; }
                set { _text = value; }
            }
        }
    }
}
