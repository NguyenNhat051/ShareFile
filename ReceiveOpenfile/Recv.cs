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
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReceiveOpenfile
{
    public partial class Recv : Form
    {
        string IP = "127.0.0.1";
        TcpListener tcpListener;
        byte[] key;
        byte[] IV;
        public Recv()
        {
            InitializeComponent();
            key = FileToByteArray("key.dat");
            IV = FileToByteArray("IV.dat");
        }

        private void Receive()
        {
            tcpListener = new TcpListener(IPAddress.Any, 7979);
            tcpListener.Start();
            Thread t = new Thread(() => 
            {
                while (true)
                {
                    TcpClient client = tcpListener.AcceptTcpClient();
                    Thread recv = new Thread(() => RecvFile(client));
                    recv.IsBackground = true;
                    recv.Start();
                }
            });
            t.IsBackground = true;
            t.Start();
        }

        private void RecvFile(TcpClient client)
        {
            string path = null;
            var localEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
            string targetIP = localEndPoint.Address.ToString();
            path = DecryptStringFromBytes_Aes(Receivebytes(client.GetStream()),key,IV);//Recv and decrypt the path
            if (path.StartsWith("~") && File.Exists(path.Remove(0, 1)))
            {
                path = path.Remove(0, 1);
                Invoke((MethodInvoker)delegate
                {
                    pathLabel.Text = path;
                });
                DialogResult result = MessageBox.Show("Do you want to encrypt the file ?", "Download request !", MessageBoxButtons.YesNo);
                if (result==System.Windows.Forms.DialogResult.Yes)
                {
                    FileEncrypt(path, key, IV);
                    path = path + ".aes";
                }
                //Send thông tin file 
                string fileName = Path.GetFileName(path);
                FileInfo fi = new FileInfo(path);
                client = new TcpClient();
                client.Connect(new IPEndPoint(IPAddress.Parse(targetIP), 6969));
                byte[] fileNameData = EncryptStringToBytes_Aes(fileName + "@" + fi.Length.ToString() + "@" + this.IP + "@" + Environment.MachineName, key, IV);
                Send(client.GetStream(), fileNameData);
                client.GetStream().Close();
                client.Close();
                //Send file
                client = new TcpClient();
                client.Connect(new IPEndPoint(IPAddress.Parse(targetIP), 6969));
                client.Client.SendFile(path);
                client.GetStream().Close();
                client.Close();
                path = null;
                Invoke((MethodInvoker)delegate
                {
                    infoLabel.ForeColor = Color.Green;
                    infoLabel.Text = "File sent to " + targetIP;
                });
            }
            else if (File.Exists(path))
            {
                try
                {
                    Invoke((MethodInvoker)delegate
                    {
                        pathLabel.Text = path;
                    });
                    Process.Start(path);
                    path = null;
                    Invoke((MethodInvoker)delegate
                    {
                        infoLabel.ForeColor = Color.Green;
                        infoLabel.Text = "File Opened !";
                    });
                    string mes = "File Opened !";
                    client = new TcpClient();
                    client.Connect(new IPEndPoint(IPAddress.Parse(targetIP), 6969));
                    Send(client.GetStream(),EncryptStringToBytes_Aes(mes,key,IV));
                    client.GetStream().Close();
                    client.Close();
                }
                catch (Exception)
                {
                    string mes = "Fail to OpenFile !";
                    client = new TcpClient();
                    client.Connect(new IPEndPoint(IPAddress.Parse(targetIP), 6969));
                    Send(client.GetStream(), EncryptStringToBytes_Aes(mes, key, IV));
                    client.GetStream().Close();
                    client.Close();
                    Invoke((MethodInvoker)delegate
                    {
                        infoLabel.ForeColor = Color.Red;
                        infoLabel.Text = "Fail to OpenFile !";
                    });
                }
            }
            else
            {
                Invoke((MethodInvoker)delegate
                {
                    pathLabel.Text = path;
                });
                string mes = "Invalid Path !";
                client = new TcpClient();
                client.Connect(new IPEndPoint(IPAddress.Parse(targetIP), 6969));
                Send(client.GetStream(), EncryptStringToBytes_Aes(mes, key, IV));
                Invoke((MethodInvoker)delegate
                {
                    infoLabel.ForeColor = Color.Red;
                    infoLabel.Text = "Invalid Path !";
                });
                //MessageBox.Show("Invalid Path !", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                client.GetStream().Close();
                client.Close();
            }  
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
            Invoke((MethodInvoker)delegate
            {
                label1.Text = ": " + this.IP;
            });
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
        private void FileEncrypt(string inputFile, byte[] Key, byte[] IV)
        {
            //create output file name
            FileStream fsCrypt = new FileStream(inputFile + ".aes", FileMode.Create);

            //Set Rijndael symmetric encryption algorithm
            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            AES.Padding = PaddingMode.PKCS7;

            AES.Key = Key;
            AES.IV = IV;
            //Cipher modes
            AES.Mode = CipherMode.CFB;

            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write);

            FileStream fsIn = new FileStream(inputFile, FileMode.Open);

            //create a buffer (1mb) so only this amount will allocate in the memory and not the whole file
            byte[] buffer = new byte[1048576];
            int read;

            try
            {
                while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                {
                    cs.Write(buffer, 0, read);
                }
                // Close up
                fsIn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                cs.Close();
                fsCrypt.Close();
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
    }
}
