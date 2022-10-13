using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace L5RHelper.ViewsModel
{
    public class PageMasterViewModel : BindableObject
    {
        private static int PortInMaster = 60002;
        private static int PortOutMaster = 60001;

        private string _ipServer;
        public string IpServer
        {
            get => _ipServer;
            set
            {
                _ipServer = value;
                OnPropertyChanged();
            }
        }

        private string _portInMasterValue;
        public string PortInMasterValue
        {
            get => _portInMasterValue;
            set
            {
                _portInMasterValue = value;
                OnPropertyChanged();
            }
        }

        private Server ServerConnection;

        public PageMasterViewModel()
        {
            ServerConnection = new Server(PortInMaster);

            MessagingCenter.Subscribe<Server, string>(this, "IpServer", (sender, arg) => { UpdateDataAndConnectionServer(arg); });      
        }
        
        private void UpdateDataAndConnectionServer(string ipServer)
        {
            Debug.WriteLine("Obtenida Ip externa " + ipServer);

            IpServer = ipServer;
            PortInMasterValue = PortInMaster.ToString();
        }

    }
}
