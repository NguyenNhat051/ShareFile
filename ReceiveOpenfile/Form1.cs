using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReceiveOpenfile
{
    public partial class Form1 : Form
    {
        string IP = "127.0.0.1";
        TcpListener tcpListener;
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Receive()
        {
            tcpListener = new TcpListener(IPAddress.Any, 6969);
            tcpListener.Start();
            Thread t = new Thread(() => {
            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                var localEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
                string targetIP = localEndPoint.Address.ToString();
                StreamReader reader = new StreamReader(client.GetStream());
                string path = reader.ReadLine();
                    if (path.StartsWith("~") && File.Exists(path.Remove(0, 1)))
                    {
                        //Bây giờ app chạy với vai trò là client
                        string fileName = Path.GetFileName(path.Remove(0, 1));
                        Socket socketForClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        socketForClient.Connect(new IPEndPoint(IPAddress.Parse(targetIP), 6969));
                        byte[] fileNameData = Encoding.Default.GetBytes(fileName + "@" + this.IP + "@" + Environment.MachineName);
                        socketForClient.Send(fileNameData);
                        socketForClient.Shutdown(SocketShutdown.Both);
                        socketForClient.Close();
                        //
                        socketForClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        socketForClient.Connect(new IPEndPoint(IPAddress.Parse(targetIP), 6969));
                        socketForClient.SendFile(path.Remove(0, 1));
                        socketForClient.Shutdown(SocketShutdown.Both);
                        socketForClient.Close();
                        path = null;
                        infoLabel.ForeColor = Color.Green;
                        infoLabel.Text = "File sent to " + targetIP;
                    }
                    else if (File.Exists(path)) 
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            pathLabel.Text = path;
                        });
                        Process.Start(path);
                        path = null;
                        infoLabel.ForeColor = Color.Green;
                        infoLabel.Text = "File Opened !";
                        string mes = "File Opened !";
                        Socket socketForClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        socketForClient.Connect(new IPEndPoint(IPAddress.Parse(targetIP), 6969));
                        byte[] Data = Encoding.Default.GetBytes(mes);
                        socketForClient.Send(Data);
                        socketForClient.Shutdown(SocketShutdown.Both);
                        socketForClient.Close();
                    }
                    else
                    {
                        string mes = "Invalid Path !";
                        Socket socketForClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        socketForClient.Connect(new IPEndPoint(IPAddress.Parse(targetIP), 6969));
                        byte[] Data = Encoding.Default.GetBytes(mes);
                        socketForClient.Send(Data);
                        socketForClient.Shutdown(SocketShutdown.Both);
                        socketForClient.Close();
                        infoLabel.ForeColor = Color.Red;
                        infoLabel.Text = "Invalid Path !";
                        //MessageBox.Show("Invalid Path !", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    client.GetStream().Close();
                    client.Close();
                }
            });
            t.IsBackground = true;
            t.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Receive();
            infoLabel.ForeColor = Color.Green;
            infoLabel.Text = "Waiting...";
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList) //Lấy địa chỉ Ip của máy
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    this.IP = ip.ToString();
                }
            }
            label1.Text = ": "+this.IP;
        }
    }
}
