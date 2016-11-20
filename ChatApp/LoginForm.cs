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

namespace ChatApp
{
    public partial class LoginForm : Form
    {
        IPAddress endPointAddress;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (CheckValidInput())
            {
                this.Hide();
                ClientForm client = new ClientForm(NameTextBox.Text, endPointAddress);
                client.Show();

                client.Closed += (s, args) => this.Close();
            }
        }

        private bool CheckValidInput()
        {
            return (NameTextBox.Text.Length > 0 && IPAddress.TryParse(IPAddressTextBox.Text, out endPointAddress) && !NameTextBox.Text.Contains(" "));
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            ConnectButton.Enabled = (CheckValidInput());
        }

        private void IPAddressTextBox_TextChanged(object sender, EventArgs e)
        {
            ConnectButton.Enabled = (CheckValidInput());
        }

        private void HostConnect_Click(object sender, EventArgs e)
        {
            IPAddressTextBox.Text = GetLocalIPAddress().ToString();
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
