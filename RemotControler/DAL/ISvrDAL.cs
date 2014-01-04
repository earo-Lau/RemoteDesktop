using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemotControler.Model;

namespace RemotControler.DAL
{
    interface ISvrDAL
    {
        List<Server_Data> ReadSvr();
        void AddSvr(Server_Data model);
        void DeleteSvr(Server_Data model);
        List<string> GetGroupList();
    }
}
