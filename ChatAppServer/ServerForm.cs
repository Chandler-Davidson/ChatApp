using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatAppServer
{
    public partial class ServerForm : Form
    {
        private struct Client
        {
            public EndPoint endPoint;
            public string name;
        }

        private ArrayList clientList;
        private Socket serverSocket;
        private byte[] dataStream = new byte[1024];
        private delegate void UpdateStatusDelegate(string status);
        private UpdateStatusDelegate updateStatusDelegate = null;

        public ServerForm()
        {
            InitializeComponent();
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            this.clientList = new ArrayList();

            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint server = new IPEndPoint(IPAddress.Any, 30000);
            serverSocket.Bind(server);

            IPEndPoint clients = new IPEndPoint(IPAddress.Any, 0);
            EndPoint epSender = (EndPoint)clients;
            serverSocket.BeginReceiveFrom(this.dataStream, 0, this.dataStream.Length, SocketFlags.None, ref epSender, new AsyncCallback(ReceiveData), epSender);

            StatusLabel.Text = "Listening";
            IPLabel.Text = GetLocalIPAddress();
        }

        private void SendData(IAsyncResult asyncResult)
        {
            try
            {
                serverSocket.EndSend(asyncResult);
            }

            catch (Exception e)
            {
                MessageBox.Show("Message not sent: " + e.Message);
            }
        }

        private void ReceiveData(IAsyncResult asyncResult)
        {
            byte[] data;

            Packet receivedData = new Packet(this.dataStream);
            Packet sendData = new Packet();
            IPEndPoint clients = new IPEndPoint(IPAddress.Any, 0);
            EndPoint senderEndPoint = (EndPoint)clients;

            serverSocket.EndReceiveFrom(asyncResult, ref senderEndPoint);

            sendData.DataID = receivedData.DataID;
            sendData.ChatName = receivedData.ChatName;

            switch(receivedData.DataID)
            {
                case Packet.MessageType.Message:
                    sendData.ChatMessage = receivedData.ChatName + ": " + receivedData.ChatMessage;
                    break;

                case Packet.MessageType.Login:
                    Client client = new Client();
                    client.endPoint = senderEndPoint;
                    client.endPoint = senderEndPoint;
                    client.name = receivedData.ChatName;

                    this.clientList.Add(client);

                    sendData.ChatMessage = "--- " + receivedData.ChatName + " has logged in ---";
                    break;

                case Packet.MessageType.Logout:
                    foreach (Client c in this.clientList)
                    {
                        if (c.endPoint.Equals(senderEndPoint))
                        {
                            this.clientList.Remove(c);
                            break;
                        }
                    }

                    sendData.ChatMessage = "--- " + receivedData.ChatName + " has logged out ---";
                    break;
            }

            data = sendData.GetDataStream();

            foreach (Client client in this.clientList)
            {
                if (client.endPoint != senderEndPoint || sendData.DataID != Packet.MessageType.Login)
                {
                    serverSocket.BeginSendTo(data, 0, data.Length, SocketFlags.None, client.endPoint, new AsyncCallback(this.SendData), client.endPoint);
                }
            }

            serverSocket.BeginReceiveFrom(this.dataStream, 0, this.dataStream.Length, SocketFlags.None, ref senderEndPoint, new AsyncCallback(this.ReceiveData), senderEndPoint);

            this.Invoke(this.updateStatusDelegate, new object[] { sendData.ChatMessage });
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }
}
