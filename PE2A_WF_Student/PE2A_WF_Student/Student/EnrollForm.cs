using PE2A_WF_Student;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PE2A_WF_Student
{
    public partial class EnrollForm : Form
    {
        public EnrollForm()
        {
            InitializeComponent();
        }
        string studentID;
        StudentForm studentSubmit = new StudentForm();
        Thread sendingThread;
        Thread listeningThread;
        private void btnEnroll_Click(object sender, EventArgs e)
        {
            studentID = txtStudentID.Text.ToUpper().Trim();

            if (studentID.Trim().ToLower().Equals("admin"))
            {
                MessageBox.Show(Constant.CANNOT_LOGIN_ADMIN_MESSAGE);
            }
            else
            {
                String workFolder = Util.ExecutablePath() + @"\Submission\" + Constant.PRACTICAL_EXAM_JAVA_WEB + @"\work";
                String webappFolder = Util.ExecutablePath() + @"\Submission\" + Constant.PRACTICAL_EXAM_JAVA_WEB + @"\webapp"; 
                if (Directory.Exists(workFolder))
                {
                    Directory.Delete(workFolder, true);
                }
                if (Directory.Exists(webappFolder))
                {
                    Directory.Delete(webappFolder, true);
                }
                Directory.CreateDirectory(workFolder);
                Directory.CreateDirectory(webappFolder);
                Random rd = new Random();
                int randomSleep = rd.Next(500, 5000);
                Thread.Sleep(randomSleep);
                studentSubmit.StudentID = studentID;
                // send broadcast to router
                string message = Util.GetLocalIPAddress() + "-" + Constant.STUDENT_LISTENING_PORT + "-" + studentID;
                SendBroadCastToRouter(message);
                studentSubmit.Show();
                this.Visible = false;
            }
        }

        private void SendBroadCastToRouter(string message)
        {
            Util.SendBroadCast(message, Constant.LECTURER_LISTENING_PORT);
            Console.WriteLine("====SENT====");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ListenToLecturer()
        {
            StudentForm studentSubmit = new StudentForm();
            studentSubmit.StudentID = studentID;
            // get message return from lecturer
            string returnMessage = Util.GetMessageFromTCPConnection(Constant.STUDENT_LISTENING_PORT, Constant.MAXIMUM_REQUEST);
            sendingThread.Abort();
            if (returnMessage.Equals(Constant.EXISTED_IP_MESSAGE))
            {
                MessageBox.Show(returnMessage);
            }
            else
            {
                string[] msgArr = returnMessage.Split('=');
                studentSubmit.SubmitAPIUrl = msgArr[1];
                studentSubmit.ScriptCode = msgArr[2];
                this.InvokeEx(f => studentSubmit.Show());
            }
            Console.WriteLine("Lecturer: " + returnMessage);
        }



        private void EnrollForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            HistoryForm historyForm = new HistoryForm();
            historyForm.Show();
        }
    }
}
