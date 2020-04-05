using PE2A_WF_Student.Models;
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
using System.Windows.Forms;

namespace PE2A_WF_Student
{
    public class Util
    {
        private static Socket listeningSocket;

        public static void SendBroadCast(String message, int receiverListeningPort)
        {
            try
            {
                IPEndPoint ipEnd = new IPEndPoint(IPAddress.Broadcast, receiverListeningPort);
                Socket clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                byte[] bytes = Encoding.ASCII.GetBytes(message);
                clientSock.SetSocketOption(SocketOptionLevel.Socket,
                SocketOptionName.Broadcast, 1);
                clientSock.SendTo(bytes, ipEnd);
                clientSock.Close();
            }
            catch (Exception ex)
            {
                LogException("SendBroadCast", ex.Message);
            }

        }
        public static void LogException(String methodName, String errorMessage)
        {
            try
            {
                String currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ");
                String logging = currentTime + methodName + " error : " + "\r\n";
                logging += errorMessage + "\r\n";
                String filePath = ExecutablePath() + Constant.LOG_FILE;
                File.AppendAllText(filePath, logging);
            }
            catch (Exception ex)
            {
                Console.WriteLine("LogException error :" + ex.Message);
            }

        }
        public static string GetMessageFromTCPConnection(int listeningPort, int maximumRequest)
        {
            try
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
            catch (Exception ex)
            {
                LogException("GetMessageFromTCPConnection", ex.Message);

            }
            return null;
        }


        public static IPAddress GetLocalIPAddress()
        {
            try
            {
                string hostName = Dns.GetHostName();
                string ip = Dns.GetHostByName(hostName).AddressList[0].ToString();
                return IPAddress.Parse(ip);
            }
            catch (Exception ex)
            {
                LogException("GetLocalIPAddress", ex.Message);

            }
            return null;

        }

        public static void sendMessage(byte[] bytes, TcpClient client)
        {
            try
            {
                // Client must be connected to   
                client.GetStream() // Get the stream and write the bytes to it  
                      .Write(bytes, 0,
                      bytes.Length); // Send the stream  
            }
            catch (Exception ex)
            {
                LogException("sendMessage", ex.Message);

            }

        }
        public static string receiveMessage(byte[] bytes)
        {
            try
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
            catch (Exception ex)
            {
                LogException("receiveMessage", ex.Message);

            }
            return null;

        }


        private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] keyBytes)
        {
            try
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
            catch (Exception ex)
            {
                LogException("Decrypt", ex.Message);

            }
            return null;

        }

        public static string Decode(string encryptedText, string key)
        {
            try
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
            catch (Exception ex)
            {
                LogException("Decode", ex.Message);

            }
            return null;

        }
        public static String DestinationOutputPath(string studentCode)
        {
            try
            {
                string studentFile = studentCode + ".zip";
                string startupPath = ExecutablePath();
                string destination = Path.Combine(startupPath + @"\Submission\" + studentFile);
                return destination;
            }
            catch (Exception ex)
            {
                LogException("DestinationOutputPath", ex.Message);

            }
            return null;

        }

        public static byte[] ConvertStreamToByte(NetworkStream networkStream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                try
                {
                    networkStream.CopyToAsync(memoryStream);
                    Thread.Sleep(2000);
                }
                catch (Exception ex)
                {
                    LogException("ConvertStreamToByte", ex.Message);
                }
                return memoryStream.ToArray();
            }
        }
        public static void UnarchiveFile(string filePath, string destDirectory)
        {
            try
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
            catch(Exception ex)
            {
                LogException("UnarchiveFile", ex.Message);
            }      
        }

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            try
            {
                DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
                DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);
                CopyAll(diSource, diTarget);
            }
            catch(Exception ex)
            {
                LogException("Copy", ex.Message);
            }
           
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            try
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
            catch(Exception ex)
            {
                LogException("CopyAll", ex.Message);

            }
           
        }

        ////release path
        //public static String ExecutablePath()
        //{
        //    string appPath = Path.GetDirectoryName(Application.ExecutablePath);
        //    return appPath;
        //}

        //debug path
        public static String ExecutablePath()
        {
            try
            {
                string startupPath = System.IO.Directory.GetCurrentDirectory();
                string projectDirectory = Directory.GetParent(startupPath).Parent.FullName;
                return projectDirectory;
            }
            catch(Exception ex)
            {
                LogException("ExecutablePath", ex.Message);

            }
            return null;
          
        }

        public static void CacheHistory(History historyObj)
        {
            try
            {
                if (!File.Exists(ExecutablePath() + @"\csvFile.csv"))
                {
                    File.Create(ExecutablePath() + @"\csvFile.csv");
                    String headerRow = "No,Student Code,Practical Name,Point,Practical Date";
                    String nextRow = 1 + "," + historyObj.StudentCode + "," + historyObj.PracticalName + "," + historyObj.Point + "," + historyObj.PracticalDate;
                    List<String> toArray = new List<String>();
                    toArray.Add(headerRow);
                    toArray.Add(nextRow);
                    File.WriteAllLines(ExecutablePath() + @"\csvFile.csv", toArray.ToArray());
                }
                else
                {
                    String[] readAllLines = File.ReadAllLines(ExecutablePath() + @"\csvFile.csv", Encoding.UTF8);
                    List<String> toArray = readAllLines.ToList();
                    String nextRow = toArray.Count + "," + historyObj.StudentCode + "," + historyObj.PracticalName + "," + historyObj.Point + "," + historyObj.PracticalDate;
                    toArray.Add(nextRow);
                    File.WriteAllLines(ExecutablePath() + @"\csvFile.csv", toArray.ToArray());
                }

            }
            catch(Exception ex)
            {
                LogException("CacheHistory", ex.Message);
            }         
        }
    }
}
