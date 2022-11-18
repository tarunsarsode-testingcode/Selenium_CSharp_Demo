using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.All_BaseFile
{
    class CommonConstant
    {
        
        public const int NoOfTest = 0;
        public const string Failed = "Failed";
        public const string NoExep = "NoExep";
        public const string Pass = "Pass";
       

        public const string Login = "Login";
        public const string Spot_Count = "Spot_Count";
       

        private static string psAppPath; // field

        public static string AppPath   // property
        {
            get { return psAppPath; }   // get method
            set { psAppPath = value; }  // set method
        }

        private static string psAppPathFolder; // field
        public static string AppPathFolder   // property
        {
            get { return psAppPathFolder; }   // get method
            set { psAppPathFolder = value; }  // set method
        }

        private static DataTable pdScreenName; // field
        public static DataTable ScreenName   // property
        {
            get { return pdScreenName; }   // get method
            set { pdScreenName = value; }  // set method
        }

        private static DataTable LoginData; // field
        public static DataTable pdLoginData   // property
        {
            get { return LoginData; }   // get method
            set { LoginData = value; }  // set method
        }

        private static int pdNoOfTestID;
        public static int NoOfTestID // property
        {
            get { return pdNoOfTestID; } // get method
            set { pdNoOfTestID = value; } // set method
        }

        private static string psSPAUserName; // field
        public static string SPALoginUserName   // property
        {
            get { return psSPAUserName; }   // get method
            set { psSPAUserName = value; }  // set method
        }
    }
}
