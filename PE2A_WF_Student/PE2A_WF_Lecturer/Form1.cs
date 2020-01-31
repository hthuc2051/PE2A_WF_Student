using PE2A_WF_Student;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PE2A_WF_Lecturer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEnroll_Click(object sender, EventArgs e)
        {
            string studentID = txtStudentID.Text;

            if (studentID.Equals("admin"))
            {
                LecturerForm lecturerForm = new LecturerForm();
                lecturerForm.Show();
            }

            else
            {
                StudentForm studentSubmit = new StudentForm();
                studentSubmit.studentID = studentID;
           
                // send broadcast to router
                string message = Util.GetLocalIPAddress() + "-" + Constain.STUDENT_LISTENING_PORT+ "-" + studentID;
                Thread t = new Thread(() => SendBroadCastToRouter(message));
                t.Start();
                // get message return from lecturer
                string returnMessage = Util.GetMessageFromTCPConnection(Constain.STUDENT_LISTENING_PORT, Constain.MAXIMUM_REQUEST);
                t.Abort();
                Console.WriteLine("Lecturer: " + returnMessage);
                string[] msgArr = returnMessage.Split('=');
                studentSubmit.submitAPIUrl = msgArr[1];
                studentSubmit.Show();
              
            }
        
        }

        private void SendBroadCastToRouter(string message)
        {
            Util.SendBroadCast(message, Constain.LECTURER_LISTENING_PORT);
            Console.WriteLine("====SENT====");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
