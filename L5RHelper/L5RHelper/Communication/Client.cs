using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace L5RHelper
{
    public class Client
    {
        private string IpServer;
        private TcpClient ClientTCP;

        private int PortOut;

        public Client(string ipServer, int portOut)
        {
            PortOut = portOut;
            IpServer = ipServer;
            ClientTCP = new TcpClient();
        }

        public bool SendRoll(Message roll)
        {
            string messageString = roll?.ToString();

            Debug.WriteLine(messageString);

            byte[] command = Encoding.ASCII.GetBytes(messageString);

            return SendUDPMessage(command, command.Length);
        }
        
        public async Task<bool> ConnectAsync()
        {
            await ClientTCP.ConnectAsync(IpServer, PortOut);

            if (ClientTCP.Connected)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool SendUDPMessage(byte[] command, int length)
        {
            if(ClientTCP.Connected)
            {
                NetworkStream stream = ClientTCP.GetStream();
                stream.Write(command, 0, length);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
