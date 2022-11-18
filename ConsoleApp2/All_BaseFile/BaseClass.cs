using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using OpenQA.Selenium;

using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit;

namespace ConsoleApp2.All_BaseFile
{
    [TestFixture]
    class BaseClass
    {

        public static IWebDriver driver = new ChromeDriver();
        // public static GetParameterValue objGetParamValue = new GetParameterValue();
        //public static SetParameterValue objSetValue = new SetParameterValue();
        public static CommonFunction objCommonFunction = new CommonFunction();
        // public static OptWebUnitTestSaveLog.WebUnitTestDL ObjWebUnitTestDL = new OptWebUnitTestSaveLog.WebUnitTestDL();
        //  CommonConstant.NoOfTestID = OptWebUnitTestSaveLog.GetTestIdForDB() + 1;
        

        [SetUp]
        static void Main(string[] args)
        {
            BaseClass bClass = new BaseClass();
            //loaded chrome driver
           // CommonConstant.NoOfTestID = ObjWebUnitTestDL.GetTestIdForDB() + 1;
             //IWebDriver driver = new ChromeDriver();
            DataTable pdDataTable = new DataTable();
            

        string psExlFileName = "Automation-FormList.xlsx";
            string psExlFilePath = bClass.GetAutomationFromListPath(psExlFileName);
            pdDataTable = bClass.LoadDataForAutomationList(psExlFilePath);

           // pdDataTable = ObjWebUnitTestDL.CheckTableisExistFormList();


            try
            {

                for (int idx = 0; idx < pdDataTable.Rows.Count; idx++)
                {

                    switch (pdDataTable.Rows[idx].Field<String>("SCREEN_NAME").ToString())
                    {
                        case CommonConstant.Login:

                            try
                            {

                                Login login = new Login();
                                DataSet pdExcelDataSetLogin = new DataSet();
                                string ExecutionExlPath = CommonConstant.AppPathFolder + pdDataTable.Rows[idx].Field<String>("PATH").ToString();

                                 pdExcelDataSetLogin = bClass.LoadDataForExecution(ExecutionExlPath);

                                login.StartUnitTest(pdExcelDataSetLogin);

                            }
                            catch (Exception ex)
                            {
                                Console.Write(ex);

                            }
                            break;

                        case CommonConstant.Spot_Count:

                            //try
                            //{
                            //    Spot_Count SPC = new Spot_Count();
                            //    DataSet pdExcelDataSetLogin = new DataSet();
                            //    string ExecutionExlPath = CommonConstant.AppPathFolder + pdDataTable.Rows[idx].Field<String>("PATH").ToString();

                            //    // string ExecutionExlPath = CommonConstant.AppPathFolder + pdDataTable.Rows[idx].Field<String>("PATH").ToString();
                            //    // string ExecutionExlPath=  cSPA.GetExlFilePathForScreen(CommonConstant.SPALogin);
                            //    // pdExcelDataSetLogin = cSPA.LoadDataForExecution(cSPA.GetExlFilePathForScreen(CommonConstant.SPALogin));
                            //    pdExcelDataSetLogin = cSPA.LoadDataForExecution(ExecutionExlPath);
                            //    SPC.StartUnitTest(pdExcelDataSetLogin);

                            //}
                            //catch (Exception ex)
                            //{
                            //    Console.Write(ex);

                            //}
                            break;

                        default:

                            Console.WriteLine("DEFAULT");
                            break;
                    }


                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

        }


        public DataTable LoadDataForAutomationList(string xlsFilePath)
        {

            string[] psArray = { };
            //  con.Open();           
            DataSet dsDataSetToSend = new DataSet();
            string connString = string.Empty;

            if (System.Environment.Is64BitOperatingSystem)
            {
                //  connString = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                //  "Data Source=" + sxlFilePath + ";Extended Properties='Excel 8.0;HDR=Yes;MaxScanRows=1;IMEX=1';"
                connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + xlsFilePath + ";Extended Properties='Excel 8.0;HDR=YES';"; //for above excel 2007  

            }
            else
            {
                connString = @"provider=provider=Microsoft.Jet.OLEDB.4.0; Data Source='" + xlsFilePath + "';Extended Properties='Excel 8.0;HDR=Yes;MaxScanRows=1;IMEX=1'";

            }

            using (OleDbConnection con = new OleDbConnection(connString))
            {
                con.Open();
                try
                {

                    DataTable dtDataTable = new DataTable();
                    dtDataTable = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, psArray);
                    if (dtDataTable != null || dtDataTable.Rows.Count > 0)
                    {
                        int piTableIndex = 0;

                        DataSet dssDataSet = new System.Data.DataSet();

                        for (int sheetCount = 0; sheetCount < dtDataTable.Rows.Count; sheetCount++)
                        {
                            //' Create Query to get Data from sheet. '
                            string psSheetname = dtDataTable.Rows[sheetCount]["table_name"].ToString();
                            string dstblName = dtDataTable.Rows[sheetCount]["table_name"].ToString().Replace("$", "");

                            // String str = "abcdefgh";
                            Char value = '$';
                            Boolean result = psSheetname.Contains(value);

                            if (result == true)
                            {
                                OleDbDataAdapter oleAdpt = new OleDbDataAdapter("SELECT * FROM [" + psSheetname + "]", con);
                                oleAdpt.Fill(dsDataSetToSend, dstblName);



                                DeleteBlankRowsfromDataset(dsDataSetToSend, piTableIndex);

                                //pdDataSet.Tables[psTblName[piTableIndex]].AcceptChanges();

                                //  dsDataSetToSend = pdDataSet;
                                piTableIndex = piTableIndex + 1;
                            }
                        }


                    }

                }
                catch (Exception ex)
                {
                    Console.Write(ex);

                }
                con.Close();
                DataTable results = dsDataSetToSend.Tables[0].Select("EXECUTE='Y'").CopyToDataTable();
                CommonConstant.ScreenName = results;
                return results;

            }


        }

        public string GetExlFilePathForScreen(string psScreenName)
        {
            string psExlFilePathName = "";
            DataView dvScreenName = new DataView(CommonConstant.ScreenName);

            dvScreenName.RowFilter = "SCREEN_NAME='" + psScreenName + "'";
            psExlFilePathName = (string)dvScreenName[0]["PATH"];
            psExlFilePathName = CommonConstant.AppPathFolder + psExlFilePathName;
            return psExlFilePathName;

        }

        public DataSet LoadDataForExecution(string xlsFilePath)
        {

            string[] psArray = { };
            //  con.Open();           
            DataSet dsDataSetToSend = new DataSet();
            string connString = string.Empty;

            if (System.Environment.Is64BitOperatingSystem)
            {
                //  connString = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                //  "Data Source=" + sxlFilePath + ";Extended Properties='Excel 8.0;HDR=Yes;MaxScanRows=1;IMEX=1';"
                connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + xlsFilePath + ";Extended Properties='Excel 8.0;HDR=YES';"; //for above excel 2007  

            }
            else
            {
                connString = @"provider=provider=Microsoft.Jet.OLEDB.4.0; Data Source='" + xlsFilePath + "';Extended Properties='Excel 8.0;HDR=Yes;MaxScanRows=1;IMEX=1'";

            }

            using (OleDbConnection con = new OleDbConnection(connString))
            {
                con.Open();
                try
                {

                    DataTable dtDataTable = new DataTable();
                    dtDataTable = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, psArray);
                    if (dtDataTable != null || dtDataTable.Rows.Count > 0)
                    {
                        int piTableIndex = 0;

                        DataSet dssDataSet = new System.Data.DataSet();

                        for (int sheetCount = 0; sheetCount < dtDataTable.Rows.Count; sheetCount++)
                        {
                            //' Create Query to get Data from sheet. '
                            string psSheetname = dtDataTable.Rows[sheetCount]["table_name"].ToString();
                            string dstblName = dtDataTable.Rows[sheetCount]["table_name"].ToString().Replace("$", "");

                            // String str = "abcdefgh";
                            Char value = '$';
                            Boolean result = psSheetname.Contains(value);

                            if (result == true)
                            {
                                OleDbDataAdapter oleAdpt = new OleDbDataAdapter("SELECT * FROM [" + psSheetname + "] WHERE EXECUTE = 'Y'", con);
                                oleAdpt.Fill(dsDataSetToSend, dstblName);

                                DeleteBlankRowsfromDataset(dsDataSetToSend, piTableIndex);

                                //pdDataSet.Tables[psTblName[piTableIndex]].AcceptChanges();

                                //  dsDataSetToSend = pdDataSet;
                                piTableIndex = piTableIndex + 1;
                            }
                        }


                    }

                }
                catch (Exception ex)
                {
                    Console.Write(ex);

                }
                con.Close();

                return dsDataSetToSend;

            }


        }

        public string GetAutomationFromListPath(string psxlFileName)
        {
            string psReturn = "";

            String psDirectory = AppDomain.CurrentDomain.BaseDirectory;
            psDirectory = psDirectory.Remove(psDirectory.IndexOf("\\bin\\Debug"));
            CommonConstant.AppPath = psDirectory;
            CommonConstant.AppPathFolder = psDirectory + "\\Screen_Sheets\\";
            psReturn = psDirectory + "\\" + psxlFileName;
            return psReturn;
        }

        public DataSet GetExlData()
        {
            DataSet pdReturnDataset = new DataSet();


            return pdReturnDataset;
        }
        public bool DeleteBlankRowsfromDataset(DataSet pdDataset, int piTableIndex)
        {
            bool pbReturn = true;
            try
            {

                pdDataset.Tables[piTableIndex].Rows.Cast<DataRow>().ToList().FindAll(Row =>
                { return String.IsNullOrEmpty(String.Join("", Row.ItemArray)); }).ForEach(Row =>
                { pdDataset.Tables[piTableIndex].Rows.Remove(Row); });

                pdDataset.Tables[piTableIndex].AcceptChanges();

                pbReturn = true;
            }
            catch (Exception ex)
            {
                pbReturn = false;
                throw;
            }

            return pbReturn;

        }

    }
}
