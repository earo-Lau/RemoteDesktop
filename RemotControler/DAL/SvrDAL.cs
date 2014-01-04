using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using RemotControler.Model;

namespace RemotControler.DAL
{
    public class SvrDAL : ISvrDAL
    {
        XElement root = XElement.Load("Contents/SvrList.xml");
        public static SvrDAL Instance = new SvrDAL();

        private SvrDAL()
        {
        }

        public List<Server_Data> ReadSvr()
        {
            var svrList = root.Elements("Group").Aggregate(new List<Server_Data>(),
                (list, e) =>
                {
                    string group = e.Attribute("Value").Value;
                    foreach (var x in e.Elements("Server"))
                    {
                        list.Add(new Server_Data()
                        {
                            Group = group,
                            SN = x.Attribute("SN").Value,
                            Port = x.Attribute("Port").Value,
                            UserName = x.Attribute("UserName").Value,
                            Pwd = x.Attribute("Pwd").Value,
                            Width = int.Parse(x.Attribute("Width").Value),
                            Height = int.Parse(x.Attribute("Height").Value),
                            Color = int.Parse(x.Attribute("Color").Value),
                            Remark = x.Value
                        });
                    }
                    return list;
                }
            ).ToList();

            return svrList;
        }

        public void AddSvr(Server_Data model)
        {
            throw new NotImplementedException();
        }

        public void DeleteSvr(Server_Data model)
        {
            throw new NotImplementedException();
        }


        public List<string> GetGroupList()
        {
            var gList = root.Elements("Group").Select(g => g.Attribute("Value").ToString()).ToList();
            return gList;
        }
    }
}
