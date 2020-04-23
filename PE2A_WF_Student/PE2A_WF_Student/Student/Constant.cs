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

        // TCP code
        public static string RETURN_URL_CODE = "[RETURN_URL]";
        public static string RETURN_EXAM_SCIPT = "[RETURN_EXAM_SCRIPT]";
        public static string RETURN_POINT = "[RETURN_POINT]";

        // File Size
        public static int FILE_SIZE = 1024 * 11;

        // Practical Exam type 
        public static string PRACTICAL_EXAM_JAVA = "JAVA";
        public static string PRACTICAL_EXAM_JAVA_WEB = "JAVAWEB";
        public static string PRACTICAL_EXAM_C_SHARP = "CSHARP";
        public static string PRACTICAL_EXAM_C = "C";

        public static string ZIP_EXTENSION = ".zip";
        public static string RAR_EXTENSION = ".rar";

        //Log file
        public static string LOG_FILE = "Log_file.txt";
        //Practical path git
        public static string JAVA_WEB_PATH_GIT = @"\Student\JavaWebTemplate";
        public static string JAVA_PATH_GIT = @"\Student\JavaTemplate\src\com";
        public static string CS_PATH_GIT = @"\Student\PRP192PracticalExam\PRP192PracticalExam";
        public static string C_PATH_GIT = @"\Student\CTemplate";
        //Practical path student 
        public static string JAVA_WEB_PATH_SAVE = "";
        public static string JAVA_PATH_SAVE = @"\practicalexam";
        public static string CS_PATH_SAVE = @"\Practical";
        public static string C_PATH_SAVE = @"\";


    }
}
