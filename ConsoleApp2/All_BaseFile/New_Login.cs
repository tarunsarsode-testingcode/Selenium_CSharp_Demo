//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using OpenQA.Selenium;
//using OpenQA.Selenium.Support.UI;
//using OpenQA.Selenium.Chrome;

//using NUnit.Framework;
//using NUnit.Framework.Interfaces;
//using NUnit;

//using AventStack.ExtentReports.Reporter;
//using AventStack.ExtentReports;
//using System.IO;

//namespace ConsoleApp2.All_BaseFile
//{
//    class New_Login
//    {
//        public IWebDriver driver;

//        public static ExtentTest test;
//        public static ExtentReports extent;

//        [SetUp]
//        public void Initialize()
//        {
//            driver = new ChromeDriver();
//        }


//        [OneTimeSetUp]
//        public void ExtentStart()
//        {

//            extent = new ExtentReports();
//            var htmlreporter = new ExtentHtmlReporter(@"D:\ReportResults\Report" + DateTime.Now.ToString("_MMddyyyy_hhmmtt") + ".html");
//            extent.AttachReporter(htmlreporter);

//        }



//        [Test]
//        public void BrowserTest()
//        {
//            test = null;
//            test = extent.CreateTest("T001").Info("Login Test");

//            driver.Manage().Window.Maximize();
//            driver.Navigate().GoToUrl("http://testing-ground.scraping.pro/login");
//            test.Log(Status.Info, "Go to URL");

//            //provide username
//            driver.FindElement(By.Id("usr")).SendKeys("admin");
//            //provide password
//            driver.FindElement(By.Id("pwd")).SendKeys("12345");

//            try
//            {
//                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
//                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h3[contains(.,'WELCOME :)')]")));
//                //Test Result
//                test.Log(Status.Pass, "Test Pass");

//            }

//            catch (Exception e)

//            {
//                test.Log(Status.Fail, "Test Fail");
//                throw;

//            }
//        }

//        [TearDown]
//        public void closeBrowser()
//        {
//            driver.Close();
//        }

//        [OneTimeTearDown]
//        public void ExtentClose()
//        {
//            extent.Flush();
//        }
//    }
//}
            

