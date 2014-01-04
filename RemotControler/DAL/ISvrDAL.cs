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
        void EditSvr(Server_Data model, Server_Data oModel);
        void DeleteSvr(Server_Data model);

        List<string> GetGroupList();
        void AddGroup(string name);
        void RenameGroup(string name, string oName);
        void DeleteGroup(string name);
    }
}
