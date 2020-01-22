using PE2A_WF_Student;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            string serverIP = txtServerIP.Text;

            if (studentID.Equals("admin"))
            {
                LecturerForm lecturerForm = new LecturerForm();
                lecturerForm.Show();
            }

            else
            {
                StudentForm studentSubmit = new StudentForm();
                studentSubmit.studentID = studentID;
                studentSubmit.serverIP = serverIP;
           
                // send broadcast to router
                string message = Util.GetLocalIPAddress() + "-" + Constain.STUDENT_LISTENING_PORT;
                Util.SendBroadCast(message, Constain.LECTURER_LISTENING_PORT);
                Console.WriteLine("====SENT====");
                // get message return from lecturer
                string returnMessage = Util.GetMessageFromTCPConnection(Constain.STUDENT_LISTENING_PORT, Constain.MAXIMUM_REQUEST);
                Console.WriteLine("Lecturer: " + returnMessage);

                studentSubmit.Show();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
