using System;
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

using ChatApp;
using ChatAppServer;
using static ChatAppServer.Packet;

namespace ChatApp
{
    public partial class ClientForm : Form
    {
        public string userName { get; set; }
        public Socket clientSocket { get; set; }
        public IPAddress serverIP { get; set; }
        private EndPoint serverEndPoint;
        private byte[] dataStream = new byte[1024];

        private delegate void DisplayMessageDelegate(string message);
        private DisplayMessageDelegate displayMessageDelegate = null;

        #region Constructor
        public ClientForm(string userName, IPAddress endPointAddress)
        {
            InitializeComponent();
            try
            {
                this.userName = userName;
                this.serverIP = endPointAddress;

                Packet sendData = new Packet();
                sendData.ChatName = this.userName;
                sendData.ChatMessage = null;
                sendData.DataID = Packet.MessageType.Login;

                this.clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                IPEndPoint server = new IPEndPoint(serverIP, 30000);
                serverEndPoint = (EndPoint)server;

                byte[] data = sendData.GetDataStream();
                clientSocket.BeginSendTo(data, 0, data.Length, SocketFlags.None, serverEndPoint, new AsyncCallback(this.SendData), null);

                this.dataStream = new byte[1024];

                clientSocket.BeginReceiveFrom(this.dataStream, 0, this.dataStream.Length, SocketFlags.None, ref serverEndPoint, new AsyncCallback(this.ReceiveData), null);
            
            }

            catch (Exception e)
            {
                MessageBox.Show("Message was not sent: " + e.Message);
            }

        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            this.displayMessageDelegate = new DisplayMessageDelegate(this.DisplayMessage);
            ConnectedUserTextBox.AppendText(userName + " (You)" + Environment.NewLine);
        }
        #endregion

        #region Send and Receive
        private void SendData(IAsyncResult a)
        {
            try
            {
                clientSocket.EndSend(a);
            }
            catch (Exception e)
            {
                MessageBox.Show("Message was not sent: " + e.Message);
            }
        }

        private void ReceiveData(IAsyncResult a)
        {
            this.clientSocket.EndReceive(a);

            Packet receivedData = new Packet(this.dataStream);

            if (receivedData.DataID == MessageType.Login)
            {
                ConnectedUserTextBox.AppendText(receivedData.ChatName + Environment.NewLine);
            }

            if (receivedData.ChatMessage != null)
                this.Invoke(this.displayMessageDelegate, new object[] { receivedData.ChatMessage });

            this.dataStream = new byte[1024];

            clientSocket.BeginReceiveFrom(this.dataStream, 0, this.dataStream.Length, SocketFlags.None, ref serverEndPoint, new AsyncCallback(this.ReceiveData), null);

            if (!ConnectedUserTextBox.Text.Contains(receivedData.ChatName))
            {
                ConnectedUserTextBox.AppendText(receivedData.ChatName + Environment.NewLine);
            }
        }
        #endregion

        private void DisplayMessage(string message)
        {
            MessageLogTextBox.Text += message + Environment.NewLine;
        }

        #region GUI Events
        private void SendButton_Click(object sender, EventArgs e)
        {
            if (MessageTextBox.Text.Length > 0)
            {
                Packet sendData = new Packet();
                sendData.ChatName = this.userName;
                sendData.ChatMessage = MessageTextBox.Text;
                sendData.DataID = MessageType.Message;

                byte[] byteData = sendData.GetDataStream();

                clientSocket.BeginSendTo(byteData, 0, byteData.Length, SocketFlags.None, serverEndPoint, new AsyncCallback(this.SendData), null);

                MessageTextBox.Text = String.Empty;
            }
        }

        private void ClientForm_FormClosing(object sender, FormClosedEventArgs e)
        {
            if (this.clientSocket != null)
            {
                Packet sendData = new Packet();
                sendData.ChatName = this.userName;
                sendData.ChatMessage = null;
                sendData.DataID = MessageType.Logout;

                byte[] byteData = sendData.GetDataStream();

                this.clientSocket.SendTo(byteData, 0, byteData.Length, SocketFlags.None, serverEndPoint);

                this.clientSocket.Close();
            }
        }
        #endregion
    }
}
