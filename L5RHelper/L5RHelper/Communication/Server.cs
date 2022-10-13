using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Mono.Nat;
using Xamarin.Forms;

namespace L5RHelper
{
    public class Server
    {
        private string _ipServer;
        public string IpServer
        {
            get => _ipServer;

            set
            {
                _ipServer = value;
            }
        }

        private string Localhost;

        private int PortIn;
        private INatDevice Device;

        public Server(int portIn)
        {
            PortIn = portIn;

            IPAddress[] localIp = Dns.GetHostAddresses(Dns.GetHostName());

            foreach (IPAddress address in localIp)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    Localhost = address.ToString();
                }
            }

            Task.Run(() => ServiceReceiverAsync());

            NatUtility.DeviceFound += DeviceFound;
            //NatUtility.+= DeviceLost;
            NatUtility.StartDiscovery();
        }

        private void ServiceReceiverAsync()
        {
            Debug.WriteLine("Servicio receive");

            TcpClient server = null;
            TcpListener listener;

            
            try
            {
                Debug.WriteLine("host: " + Localhost + " Port: " + PortIn);
                IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(Localhost), PortIn);
                listener = new TcpListener(endpoint);

                listener.Start();
                server = listener.AcceptTcpClient();

                while (server.Connected)
                {
                    Debug.WriteLine("recibiendo");

                    const int bytesize = 1024 * 1024;
                    byte[] buffer = new byte[bytesize];
                    int lengthData = server.GetStream().Read(buffer, 0, bytesize);
                    string data = Encoding.UTF8.GetString(buffer);

                    Debug.WriteLine("Mensaje Recibido: " + data);

                    // des-serializar XElement.
                    Message ComandoRecibido = Message.ToObject(data);

                    // si se ha recibido un comando que no se ha podido des-serializar, lo ignoramos
                    if (ComandoRecibido == null)
                    {
                        //Debug.WriteLine("OP6. El mensaje recibido está vacio");
                        continue;
                    }

                    foreach (Die die in ComandoRecibido.Dice)
                    {
                        Debug.WriteLine(die.getString());
                    }
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                server.Dispose();
                server.Close();
            }                        
        }

        private void DeviceFound(object sender, DeviceEventArgs args)
        {
            Debug.WriteLine("DeviceFound");

            Device = args.Device;

            IpServer = Device.GetExternalIPAsync().Result.ToString();

            Debug.WriteLine("IpServer: " + IpServer);

            Mapping mapping = new Mapping(Protocol.Tcp, PortIn, PortIn, 7200, "L5RHelper");
            
            _ = Device.CreatePortMap(mapping);

            //Debug.WriteLine("Mapping: " + mappingCreate.ToString());

            MessagingCenter.Send<Server, string>(this, "IpServer", IpServer);
        }
    }
}


/*string externalIP;
           externalIP = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
           externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                        .Matches(externalIP)[0].ToString();*/