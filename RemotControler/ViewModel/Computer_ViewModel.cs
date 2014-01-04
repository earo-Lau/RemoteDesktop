using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemotControler.Model;

namespace RemotControler.ViewModel
{
    public class Computer_ViewModel
    {
        public Server_Data model { get; set; }

        public Computer_ViewModel()
        {
        }

        public Computer_ViewModel(Server_Data model)
        {
            this.model = model;
        }
    }
}
