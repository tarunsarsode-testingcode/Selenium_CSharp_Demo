using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.All_BaseFile
{
    class Login
    {
        string SysResultMessages = "";
        String MethodName = "";
        Login objLogin = null;
        TestCaseReportLog.WebUnitTestDL ObjWebUnitTestDL = new TestCaseReportLog.WebUnitTestDL();

        public void StartUnitTest(DataSet dsDtExecutionDataSet)
        {

            CommonConstant.pdLoginData = dsDtExecutionDataSet.Tables[0];
            BaseClass.driver.Navigate().GoToUrl(CommonID.APP_URL);
            //IJavaScriptExecutor js = (IJavaScriptExecutor)AutomationTestUI.driver;
            //js.ExecuteScript("return localStorage.setItem('devtools','true')");
            //js.ExecuteScript("return localStorage.setItem('import-map-override:@optiproerp/wms','http://localhost:4219/main.js');");
            Console.WriteLine("Browser Maximized");
            // AutomationTestUI.driver.Navigate().GoToUrl("http://172.16.6.36:1300/OptiProERPWMS/account");
            //DataTable pdLoginDataTable = new DataTable();
            //DataTable pdCreateWODataTable = new DataTable();
            //DataSet dsCovertExlToDataset = new DataSet();

            try
            {
                objLogin = new Login();
                //IJavaScriptExecutor js = (IJavaScriptExecutor)AutomationTestUI.driver;
                //js.ExecuteScript("return localStorage.setItem('devtools','true');");
                //js.ExecuteScript("return localStorage.setItem('import-map-override:@optiproerp/pwb','http://localhost:4224/main.js');");
                BaseClass.driver.Navigate().Refresh();


                for (int idx = 0; idx < CommonConstant.pdLoginData.Rows.Count; idx++)
                {

                    MethodName = BaseClass.objCommonFunction.GetStringValue(idx, FromConstant.MathodName, CommonConstant.pdLoginData);

                    if (MethodName != "")
                    {
                        try
                        {
                            typeof(Login).GetMethod(MethodName).Invoke(objLogin, new object[] { idx });

                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex);

                        }
                    }


                }
                // ObjWebUnitTestDL.Dispose();

            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

        }


        #region Admin login

        public void Admin_Login(int TestCaseRowIdx)
        {
            String TestCaseId = "", userID = "", password = "";

            TestCaseId = BaseClass.objCommonFunction.GetStringValue(TestCaseRowIdx, FromConstant.TestCaseId, CommonConstant.pdLoginData);

            userID = BaseClass.objCommonFunction.GetStringValue(TestCaseRowIdx, FromConstant.UserID, CommonConstant.pdLoginData);

            password = BaseClass.objCommonFunction.GetStringValue(TestCaseRowIdx, FromConstant.Password, CommonConstant.pdLoginData);


            try
            {
                BaseClass.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                BaseClass.objCommonFunction.SetValueByName(CommonID.UserID, userID);
                System.Threading.Thread.Sleep(500);
                BaseClass.objCommonFunction.SetValueByName(CommonID.Password,password);
                System.Threading.Thread.Sleep(500);

                BaseClass.objCommonFunction.Click_Xpath(CommonID.Submit);

               // ObjWebUnitTestDL.SaveLogForTestCase(CommonConstant.NoOfTestID, CommonConstant.SPALoginUserName, CommonConstant.Picklist_Delivery, TestCaseId, TestCaseName, "", CommonConstant.Pass, CommonConstant.NoExep, CommonConstant.WMS, isFuncInternalCall);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
              //  ObjWebUnitTestDL.SaveLogForTestCase(CommonConstant.NoOfTestID, CommonConstant.SPALoginUserName, CommonConstant.Picklist_Delivery, TestCaseId, TestCaseName, SysResultMessages, CommonConstant.Failed, ex.ToString(), CommonConstant.WMS, isFuncInternalCall);

            }


        }

        #endregion


    }
}
