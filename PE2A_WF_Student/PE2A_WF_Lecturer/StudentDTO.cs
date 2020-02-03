using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PE2A_WF_Lecturer
{
    class StudentDTO
    {
        public string StudentID{ get; set; }
      public IPAddress IpAddress { get; set; }
      public int Port { get; set; }
   
        public double Point{ get; set; }

        public StudentDTO(string studentID, IPAddress ipAddress, int port)
        {
            StudentID = studentID;
            IpAddress = ipAddress;
            Port = port;
        }
    }
}
