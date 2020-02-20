using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE2A_WF_Student
{
    public class Constant
    {
        public static int LECTURER_LISTENING_PORT = 9999;
        public static int STUDENT_LISTENING_PORT = 9998;
        public static int MAXIMUM_REQUEST = 100;
        public static string EXISTED_IP_MESSAGE = "You Have Connected To Server";
        public static string[] STATUSLIST = { "connected","submitted","checked","returned"};
        public static string CANNOT_LOGIN_ADMIN_MESSAGE = "You are not permitted to log in with this role";
        public static string SUBMMIT_SUCCESS_MESSAGE = "Submit successfully";
    }
}
