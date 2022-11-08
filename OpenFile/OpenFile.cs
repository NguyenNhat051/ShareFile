using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenFile
{
    public partial class OpenFile : Form
    {
        Thread searchPC;
        Thread recv;
        string IP = "127.0.0.1";
        string targetIP;
        string targetName;
        TcpClient tcpClient;
        TcpListener listener;
        byte[] key;
        byte[] IV;
        public OpenFile()
        {
            InitializeComponent();
            key = FileToByteArray("key.dat");
            IV = FileToByteArray("IV.dat");
        }
        
        private void startBtn_Click(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                progressBar1.Maximum = 255;
            });
            bool isNetworkUp = NetworkInterface.GetIsNetworkAvailable();
            if (isNetworkUp)
            {
                Invoke((MethodInvoker)delegate
                {
                    notificationLabel.ForeColor = Color.Blue;
                    notificationLabel.Text = "Searching ...";
                });
                onlinePCList.Items.Clear();
                string myipsplit = string.Empty;
                string localhostname = Dns.GetHostName();
                IPAddress[] paddresses = Dns.GetHostAddresses(localhostname);
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList) //Lấy địa chỉ Ip của máy
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        this.IP = ip.ToString();
                    }
                }
                Invoke((MethodInvoker)delegate
                {
                    infoLabel.Text = "This Computer: " + this.IP;
                });
                string[] myiparray = this.IP.Split(new[] { '.' });
                for (int j = 1; j < myiparray.Length; j++)
                    myipsplit += myiparray[j - 1] + "."; // Vd:192.168.1.
                searchPC = new Thread(() =>
                {
                    for (int id = 1; id <= 255; id++)
                    {
                        try
                        {
                            var pingSender = new Ping();
                            string ls = myipsplit + id.ToString();
                            PingReply reply = pingSender.Send(ls, 100);
                            if (reply != null)
                                if (reply.Status == IPStatus.Success)
                                {
                                    string name;
                                    try
                                    {
                                        IPHostEntry hostEntry = Dns.GetHostEntry(reply.Address.ToString());
                                        name = hostEntry.HostName;
                                    }
                                    catch (SocketException ex)
                                    {
                                        name = ex.Message;
                                    }
                                    Invoke((MethodInvoker)delegate
                                    {
                                        ListViewItem item = new ListViewItem();
                                        item.Text = reply.Address.ToString();
                                        item.SubItems.Add(name);
                                        onlinePCList.Items.Add(item);
                                    });
                                }
                            Invoke((MethodInvoker)delegate
                            {
                                progressBar1.Value = id;
                            });
                            pingSender.Dispose();
                        }
                        catch (Exception)
                        { 
                           //StopSearching...
                        }
                    }
                    Invoke((MethodInvoker)delegate
                    {
                        progressBar1.Value = 1;
                        notificationLabel.ForeColor = Color.Green;
                        notificationLabel.Text = "Application is Online";
                    });
                });
                searchPC.IsBackground = true;
                searchPC.Start();
            }
            else
            {
                Invoke((MethodInvoker)delegate
                {
                    notificationLabel.ForeColor = Color.Red;
                    notificationLabel.Text = "Application is Offline";
                });
                MessageBox.Show("Not connected to LAN");
            }
        }
        private void stopBtn_Click(object sender, EventArgs e)
        {
            if (recv != null)
            { 
                searchPC.Abort();
            }
            bool isNetworkUp = NetworkInterface.GetIsNetworkAvailable();
            if (isNetworkUp)
            {
                Invoke((MethodInvoker)delegate
                {
                    progressBar1.Value = 1;
                    notificationLabel.ForeColor = Color.Green;
                    notificationLabel.Text = "Application is Online";
                });
            }
            else
            {
                Invoke((MethodInvoker)delegate
                {
                    progressBar1.Value = 1;
                    notificationLabel.ForeColor = Color.Red;
                    notificationLabel.Text = "Application is Offline";
                });
                MessageBox.Show("Not connected to LAN");
            }
        }
        private void browseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browse = new FolderBrowserDialog();
            if (browse.ShowDialog() == DialogResult.OK)
            {
                string savePath = browse.SelectedPath;
                fileSaveLocationLabel.Text = savePath;
            }
        }
        private void btnOpenfile_Click(object sender, EventArgs e)
        {
            targetIP = null;
            targetName = null;
            if ((onlinePCList.SelectedIndices.Count > 0) || ipBox.Text != "")
            {
                if (ipBox.Text != "")
                {
                    targetIP = ipBox.Text;
                    targetName = "";
                }
                else
                {
                    targetIP = onlinePCList.SelectedItems[0].Text;
                    targetName = onlinePCList.SelectedItems[0].SubItems[1].Text;
                }
                try
                {
                    Ping p = new Ping();
                    PingReply r;
                    r = p.Send(targetIP);
                    if (!(r.Status == IPStatus.Success))
                    {
                        Exception myex = new Exception();
                        throw myex;
                    }
                    //MessageBox.Show("Connected");
                    if (txtFilePath.Text==string.Empty)
                    {
                        MessageBox.Show("Please enter the path!");
                    }
                    else
                    {
                        tcpClient = new TcpClient();
                        tcpClient.Connect(new IPEndPoint(IPAddress.Parse(targetIP), 7979));
                        Send(tcpClient.GetStream(), EncryptStringToBytes_Aes(txtFilePath.Text,key,IV));
                    }
                    tcpClient.GetStream().Close();
                    tcpClient.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Target computer is not available.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnDownload_Click(object sender, EventArgs e)
        {
            targetIP = null;
            targetName = null;
            if ((onlinePCList.SelectedIndices.Count > 0) || ipBox.Text != "")
            {
                if (ipBox.Text != "")
                {
                    targetIP = ipBox.Text;
                    targetName = "";
                }
                else
                {
                    targetIP = onlinePCList.SelectedItems[0].Text;
                    targetName = onlinePCList.SelectedItems[0].SubItems[1].Text;
                }
                try
                {
                    Ping p = new Ping();
                    PingReply r;
                    r = p.Send(targetIP);
                    if (!(r.Status == IPStatus.Success))
                    {
                        Exception myex = new Exception();
                        throw myex;
                    }

                    tcpClient = new TcpClient();
                    tcpClient.Connect(new IPEndPoint(IPAddress.Parse(targetIP), 7979));
                    //MessageBox.Show("Connected");
                    if (txtFilePath.Text == "")
                    {
                        MessageBox.Show("Please enter the path!");
                    }
                    else
                    {
                        Send(tcpClient.GetStream(), EncryptStringToBytes_Aes("~" + txtFilePath.Text, key, IV));
                    }
                    tcpClient.GetStream().Close();
                    tcpClient.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Target computer is not available.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void OpenFile_FormClosing(object sender, FormClosingEventArgs e)
        {
            //listener.Stop();
        }
        void ReceiveFile()
        {
            listener = new TcpListener(IPAddress.Any, 6969);
            listener.Start();
            recv = new Thread(() => {
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    Byte[] bytes = new Byte[256];
                    String data = null;
                    data = DecryptStringFromBytes_Aes(Receivebytes(client.GetStream()), key, IV);
                    if (data == "File Opened !")//File đã được mở ở client
                    {
                        MessageBox.Show("File Opened !");
                        client.GetStream().Close();
                        client.Close();
                    }
                    else if (data == "Fail to OpenFile !")
                    {
                        MessageBox.Show("Fail to OpenFile !");
                        client.GetStream().Close();
                        client.Close();
                    }
                    else if (data== "Invalid Path !") //Client sai path
                    {
                        MessageBox.Show("Invalid Path !", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        client.GetStream().Close();
                        client.Close();
                    }
                    else //Download file
                    {
                        string[] msg = data.Split('@'); //Filename@senderIP@senderMachineName
                        string fileName = msg[0];
                        string fileSize = msg[1];
                        //string senderIP = msg[2];
                        //string senderMachineName = msg[3];
                        client.GetStream().Close();
                        client.Close();
                        //MessageBox.Show(fileName);
                        client = listener.AcceptTcpClient();
                        stream = client.GetStream();
                        string savePath = fileSaveLocationLabel.Text + "\\" + fileName;

                        Invoke((MethodInvoker)delegate
                        {
                            progressBar1.Maximum = int.Parse(fileSize) + 1;
                            notificationLabel.ForeColor = Color.Blue;
                            notificationLabel.Text = "Downloading ...";
                        });

                        using (var output = File.Create(savePath))
                        {
                            //file chia nhỏ thành 1KB
                            var buffer = new byte[1024];
                            int bytesRead;
                            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                output.Write(buffer, 0, bytesRead);
                                Invoke((MethodInvoker)delegate
                                {
                                    progressBar1.Value += bytesRead;
                                });
                            }
                        }
                        Invoke((MethodInvoker)delegate
                        {
                            progressBar1.Value = 1;
                            notificationLabel.ForeColor = Color.Green;
                            notificationLabel.Text = "Application is Online";
                        });
                        MessageBox.Show("File saved !");
                        client.GetStream().Close();
                        client.Close();
                    }
                }
            });
            recv.IsBackground = true;
            recv.Start();
        }
        private void onlinePCList_Click(object sender, EventArgs e)
        {
            ipBox.Text = onlinePCList.SelectedItems[0].Text;
        }
        private void OpenFile_Load(object sender, EventArgs e)
        {
            ReceiveFile();
            fileSaveLocationLabel.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }
        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public byte[] FileToByteArray(string fileName)
        {
            return File.ReadAllBytes(fileName);
        }
        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }
        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
        private void FileDecrypt(string inputFile, string outputFile, byte[] Key, byte[] IV)
        {
            FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            AES.Key = Key;
            AES.IV = IV;
            AES.Padding = PaddingMode.PKCS7;
            AES.Mode = CipherMode.CFB;

            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read);

            FileStream fsOut = new FileStream(outputFile, FileMode.Create);

            int read;
            byte[] buffer = new byte[1048576];

            try
            {
                while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    Application.DoEvents();
                    fsOut.Write(buffer, 0, read);
                }
            }
            catch (CryptographicException ex_CryptographicException)
            {
                Console.WriteLine("CryptographicException error: " + ex_CryptographicException.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            try
            {
                cs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error by closing CryptoStream: " + ex.Message);
            }
            finally
            {
                fsOut.Close();
                fsCrypt.Close();
            }
        }
        private byte[] PerformCryptography(byte[] data, ICryptoTransform cryptoTransform)
        {
            using (var ms = new MemoryStream())
            using (var cryptoStream = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write))
            {
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();

                return ms.ToArray();
            }
        }
        void Send(NetworkStream netstr, byte[] message)
        {
            try
            {
                //byte[] send = Encoding.UTF8.GetBytes(message.ToCharArray(), 0, message.Length);
                netstr.Write(message, 0, message.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!\n" + ex.Message + "\n" + ex.StackTrace);
            }
        }
        byte[] Receivebytes(NetworkStream netstr)
        {
            try
            {
                // Buffer to store the response bytes.
                byte[] recv = new Byte[256];

                // Read the first batch of the TcpServer response bytes.
                int bytes = netstr.Read(recv, 0, recv.Length); //(This receives the data using the byte method)

                byte[] a = new byte[bytes];

                for (int i = 0; i < bytes; i++)
                {
                    a[i] = recv[i];
                }

                return a;
            }
            catch (Exception ex)
            {
               MessageBox.Show("Error!\n" + ex.Message + "\n" + ex.StackTrace);
               return null;
            }

        }
        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "encrypt file (*.aes)|*.aes";
            if (ofd.ShowDialog() == DialogResult.OK) 
            {
                string fileName = ofd.FileName.Replace(".aes", string.Empty);
                FileDecrypt(ofd.FileName, fileName, key, IV);
            }
        }
    }
}
