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
        private const string PATH = "Contents/SvrList.xml";
        protected XDocument doc = XDocument.Load(PATH);
        public static SvrDAL Instance = new SvrDAL();

        private SvrDAL()
        {
        }

        #region SvrManage
        public List<Server_Data> ReadSvr()
        {
            var svrList = doc.Elements("root").Elements("Group").Aggregate(new List<Server_Data>(),
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
            var gTarget = doc.Elements("root").Elements("Group").SingleOrDefault(g => g.Attribute("Value").Value == model.Group);
            //XElement svr = new XElement("<Server SN='" + model.SN + "' Port='" + model.Port + "' UserName='" + model.UserName +
            //    "' Pwd='" + model.Pwd + "' Width='" + model.Width + "' Height='" + model.Height + "' Color='" + model.Color + "'>" +
            //    model.Remark + "</Server>");
            XElement svr = new XElement("Server");
            svr.SetAttributeValue("SN", model.SN);
            svr.SetAttributeValue("Port", model.Port);
            svr.SetAttributeValue("UserName", model.UserName);
            svr.SetAttributeValue("Pwd", model.Pwd);
            svr.SetAttributeValue("Width", model.Width);
            svr.SetAttributeValue("Height", model.Height);
            svr.SetAttributeValue("Color", model.Color);
            svr.Value = model.Remark;

            gTarget.Add(svr);
            doc.Save(PATH);
        }

        public void EditSvr(Server_Data model, Server_Data oModel)
        {
            var oGroup = doc.Elements("root").Elements("Group").SingleOrDefault(g => g.Attribute("Value").Value == oModel.Group);
            var svr = oGroup.Elements("Server").SingleOrDefault(s => s.Attribute("SN").Value == oModel.SN
                && s.Attribute("UserName").Value == oModel.UserName
                && s.Attribute("Port").Value == oModel.Port
                && s.Attribute("Pwd").Value == oModel.Pwd
                && s.Attribute("Width").Value == oModel.Width.ToString()
                && s.Attribute("Height").Value == oModel.Height.ToString()
                && s.Attribute("Color").Value == oModel.Color.ToString()
                && s.Value == oModel.Remark);

            svr.SetAttributeValue("SN", model.SN);
            svr.SetAttributeValue("Port", model.Port);
            svr.SetAttributeValue("UserName", model.UserName);
            svr.SetAttributeValue("Pwd", model.Pwd);
            svr.SetAttributeValue("Width", model.Width);
            svr.SetAttributeValue("Height", model.Height);
            svr.SetAttributeValue("Color", model.Color);
            svr.Value = model.Remark;

            if (model.Group != oModel.Group)
            {
                svr.Remove();

                var tGroup = doc.Elements("root").Elements("Group").SingleOrDefault(g => g.Attribute("Value").Value == model.Group);
                tGroup.Add(svr);
            }

            doc.Save(PATH);
        }

        public void DeleteSvr(Server_Data model)
        {
            var gTarget = doc.Elements("root").Elements("Group").SingleOrDefault(g => g.Attribute("Value").Value == model.Group);
            var svr = gTarget.Elements("Server").SingleOrDefault(s => s.Attribute("SN").Value == model.SN
                && s.Attribute("UserName").Value == model.UserName
                && s.Attribute("Port").Value == model.Port
                && s.Attribute("Pwd").Value == model.Pwd
                && s.Attribute("Width").Value == model.Width.ToString()
                && s.Attribute("Height").Value == model.Height.ToString()
                && s.Attribute("Color").Value == model.Color.ToString()
                && s.Value == model.Remark);

            if (svr != null)
            {
                svr.Remove();
            }

            doc.Save(PATH);
        }
        #endregion

        #region Group
        public List<string> GetGroupList()
        {
            var gList = doc.Elements("root").Elements("Group").Select(g => g.Attribute("Value").Value).ToList();
            return gList;
        }

        public void AddGroup(string name)
        {
            XElement group = new XElement("Group");
            group.SetAttributeValue("Value", name);

            doc.Element("root").Add(group);
            doc.Save(PATH);
        }

        public void RenameGroup(string name, string oName)
        {
            var group = doc.Element("root").Elements("Group").SingleOrDefault(g => g.Attribute("Value").Value == oName);
            if (group != null)
            {
                group.SetAttributeValue("Value", name);
                doc.Save(PATH);
            }
        }

        public void DeleteGroup(string name)
        {
            var group = doc.Element("root").Elements("Group").SingleOrDefault(g => g.Attribute("Value").Value == name);
            if (group != null)
            {
                group.Remove();
                doc.Save(PATH);
            }
        }

        #endregion
    }
}
