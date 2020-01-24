using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

        public static string GetMessageFromTCPConnection(int listeningPort, int maximumRequest)
        {
            Socket listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
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

        static Socket s;
        static Byte[] buffer;
        public static void ListeningToBroadcastUDPConnection(int listeningPort)
        {
            s = new Socket(AddressFamily.InterNetwork,
                          SocketType.Dgram,
                                ProtocolType.Udp);

            IPEndPoint senders = new IPEndPoint(IPAddress.Any, listeningPort);
            EndPoint tempRemoteEP = (EndPoint)senders;
            s.Bind(senders);
            buffer = new byte[1024];
            s.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref tempRemoteEP,
                                            new AsyncCallback(DoReceiveFrom), buffer);
        }

        private static void DoReceiveFrom(IAsyncResult iar)
        {
            EndPoint clientEP = new IPEndPoint(IPAddress.Any, 5656);
            int size = s.EndReceiveFrom(iar, ref clientEP);
            if (size > 0)
            {
                byte[] receivedData = new Byte[size];
                receivedData = (byte[])iar.AsyncState;

                ASCIIEncoding eEncpding = new ASCIIEncoding();
                string receivedMessage = eEncpding.GetString(receivedData);
                receivedMessage = receivedMessage.Substring(0, size);
                Console.WriteLine("received message:" + receivedMessage);
                string[] data = receivedMessage.Split('-');
                Thread t = new Thread(() => returnWebserviceURL(data[0], int.Parse(data[1])));
                t.Start();
            }
            s.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None,
                ref clientEP, new AsyncCallback(DoReceiveFrom), buffer);
        }
        private static void returnWebserviceURL(string ipAddess, int port)
        {
            bool isSent = false;
            string submissionURL = "http://" + GetLocalIPAddress() + ":8080/api/submission";
            do
            {
                try
                {
                    IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse(ipAddess), port);
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(iPEnd);
                    socket.Send(Encoding.UTF8.GetBytes("here is your submission url =" + submissionURL));
                    socket.Close();
                    isSent = true;
                }
                catch (Exception e)
                {

                }
            } while (!isSent);
           
        }

        public static IPAddress GetLocalIPAddress()
        {
            string hostName = Dns.GetHostName();
            string ip = Dns.GetHostByName(hostName).AddressList[0].ToString();
            return IPAddress.Parse(ip);
        }

    }
}
