using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PE2A_WF_Student
{
    public class Util
    {
        public static void SendBroadCast(String message, int receiverListeningPort)
        {
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Broadcast, receiverListeningPort);
            Socket clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            byte[] bytes = Encoding.ASCII.GetBytes(message);
            clientSock.SetSocketOption(SocketOptionLevel.Socket,
            SocketOptionName.Broadcast, 1);
            clientSock.SendTo(bytes, ipEnd);
            clientSock.Close();
        }
        private static Socket listeningSocket;
        public static string GetMessageFromTCPConnection(int listeningPort, int maximumRequest)
        {
            listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Byte[] buffer = new byte[1024];
            IPEndPoint senders = new IPEndPoint(IPAddress.Any, listeningPort);
            listeningSocket.Bind(senders);
            listeningSocket.Listen(maximumRequest);
            Socket conn = listeningSocket.Accept();
            int size = conn.Receive(buffer);
            ASCIIEncoding eEncpding = new ASCIIEncoding();
            string receivedMessage = eEncpding.GetString(buffer);
            listeningSocket.Close();
            return receivedMessage.Substring(0, size);
        }



        public static IPAddress GetLocalIPAddress()
        {
            string hostName = Dns.GetHostName();
            string ip = Dns.GetHostByName(hostName).AddressList[0].ToString();
            return IPAddress.Parse(ip);
        }

        public static void sendMessage(byte[] bytes, TcpClient client)
        {
            // Client must be connected to   
            client.GetStream() // Get the stream and write the bytes to it  
                  .Write(bytes, 0,
                  bytes.Length); // Send the stream  
        }
        public static string receiveMessage(byte[] bytes)
        {
            string message = System.Text.Encoding.Unicode.GetString(bytes);

            string messageToPrint = null;
            foreach (var nullChar in message)
            {
                if (nullChar != '\0')
                {
                    messageToPrint += nullChar;
                }
            }
            return messageToPrint;
        }


        private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] keyBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(keyBytes, saltBytes, 1000);
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }

                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        public static string Decode(string encryptedText, string key)
        {
            if (encryptedText == null)
            {
                return null;
            }

            if (key == null)
            {
                key = String.Empty;
            }

            // Get the bytes of the string
            var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
            var keyBytes = Encoding.UTF8.GetBytes(key);

            keyBytes = SHA256.Create().ComputeHash(keyBytes);

            var bytesDecrypted = Decrypt(bytesToBeDecrypted, keyBytes);

            return Encoding.UTF8.GetString(bytesDecrypted);
        }
        public static String DestinationOutputPath(string studentCode)
        {
            string studentFile = studentCode + ".zip";
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            string destination = Directory.GetParent(startupPath).Parent.FullName + @"\Submission\" + studentFile;
            return destination;
        }

        public static byte[] ConvertStreamToByte(NetworkStream networkStream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                try
                {
                    networkStream.CopyToAsync(memoryStream);
                    Thread.Sleep(2000);
                }catch(Exception e)
                {
                    throw;
                }
                return memoryStream.ToArray();
            }
        }
        public static void UnarchiveFile(string filePath, string destDirectory)
        {
            string filenameExtension = Path.GetExtension(filePath);

            if (Directory.Exists(destDirectory))
            {
                if (filenameExtension.Equals(Constant.ZIP_EXTENSION, StringComparison.OrdinalIgnoreCase))
                {
                    using (var zipArchive = ZipArchive.Open(filePath))
                    {
                        foreach (var entry in zipArchive.Entries)
                        {
                            entry.WriteToDirectory(destDirectory, new ExtractionOptions()
                            {
                                Overwrite = true,
                                ExtractFullPath = true,
                            });
                        }
                    }
                }
                else if (filenameExtension.Equals(Constant.RAR_EXTENSION, StringComparison.OrdinalIgnoreCase))
                {
                    using (var rarArchive = RarArchive.Open(filePath))
                    {
                        foreach (var entry in rarArchive.Entries)
                        {
                            entry.WriteToDirectory(destDirectory, new ExtractionOptions()
                            {
                                Overwrite = true,
                                ExtractFullPath = true,
                            });
                        }
                    }
                }
            }
        }

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

    }
}
