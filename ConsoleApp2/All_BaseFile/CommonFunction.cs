using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;


namespace ConsoleApp2.All_BaseFile
{
    class CommonFunction
    {
        public void SetValue(string psIdName, string psvalue)
        {
            BaseClass.driver.FindElement(By.Id(psIdName)).SendKeys(psvalue);
        }

        public void WindowMaximize()
        {
            BaseClass.driver.Manage().Window.Maximize();
        }

        public void SetValueByName(string psIdName, string psvalue)
        {
            BaseClass.driver.FindElement(By.Name(psIdName)).SendKeys(psvalue);
        }

        public void SetValueByXpath(string psIdName, string psvalue)
        {
            BaseClass.driver.FindElement(By.XPath(psIdName)).SendKeys(psvalue);
        }

        public void Keypress(string psIdName, string KeyName)
        {
            BaseClass.driver.FindElement(By.XPath(psIdName)).SendKeys(KeyName);
        }


        public void ClearValue(string psIdName)
        {
            BaseClass.driver.FindElement(By.Id(psIdName)).Clear();
        }
        public void ClearValueByName(string psIdName)
        {
            BaseClass.driver.FindElement(By.Name(psIdName)).Clear();
        }

        public void ClearValueByXpath(string psIdName)
        {
            BaseClass.driver.FindElement(By.XPath(psIdName)).Clear();
        }


        public String GetStringValue(int piRowId, string psColumnName, DataTable ObjDataTable)
        {
            string psReturn = "";
            psReturn = Convert.ToString(ObjDataTable.Rows[piRowId].Field<String>("" + psColumnName + ""));
            return psReturn;
        }
        public int GetIntValue()
        {
            int piReturn = 0;
            return piReturn;
        }

        public DateTime GetDateValue()
        {
            DateTime pdReturn = DateTime.Today;
            return pdReturn;
        }

        public string GetPlaceholderValueUI(string psIdName)
        {
            string psReturn = "";
            psReturn = BaseClass.driver.FindElement(By.Id(psIdName)).GetAttribute("placeholder");
            return psReturn;
        }
        public void Click(string psIdName)
        {
            BaseClass.driver.FindElement(By.Id(psIdName)).Click();
        }

        public void Click_Xpath(string psIdName)
        {
            BaseClass.driver.FindElement(By.XPath(psIdName)).Click();
        }

        public void Click_Name(string psIdName)
        {
            BaseClass.driver.FindElement(By.Name(psIdName)).Click();
        }

        public string GetTextBoxValueUI(string psIdName)
        {
            string psReturn = "";
            psReturn = BaseClass.driver.FindElement(By.Id(psIdName)).GetAttribute("value");
            return psReturn;
        }

        public string GetErrorMsgForBlurUI(string psIdName)
        {
            string psReturn = "";
            psReturn = BaseClass.driver.FindElement(By.Id(psIdName)).Text;
            return psReturn;
        }

        public OpenQA.Selenium.IWebElement GetLookupDataUI(string psIdName)
        {
            IWebElement psReturn;
            psReturn = BaseClass.driver.FindElement(By.Id(psIdName));

            return psReturn;
        }

        public OpenQA.Selenium.IWebElement GetLookupDataUI_xpath(string psIdName)
        {
            IWebElement psReturn;
            psReturn = BaseClass.driver.FindElement(By.XPath(psIdName));

            return psReturn;
        }


        public bool SelectValueFromLookupWithPager(string psValue, IWebElement LookupTable)
        {
            bool pbReturn = true;
            bool pbPagerNextBtnDisabled = false;
            bool pbExist = false;
            try
            {
                IList<IWebElement> elements = LookupTable.FindElements(By.XPath("//tr/td[1]"));
                IWebElement PagerNextBtn = LookupTable.FindElement(By.TagName("kendo-pager-next-buttons")).FindElement(By.ClassName("k-pager-nav"));
                //FindElement(By.TagName("span"))
                pbPagerNextBtnDisabled = PagerNextBtn.GetAttribute("class").Contains("k-state-disabled");

                foreach (IWebElement e in elements)
                {
                    System.Console.WriteLine(e.Text);
                    if (psValue == e.Text)
                    {
                        e.Click();
                        pbExist = true;
                        pbReturn = true;
                        break;
                    }

                }

                if (pbExist != true)
                {
                    if (!pbPagerNextBtnDisabled)
                    {
                        PagerNextBtn.Click();
                        pbReturn = SelectValueFromLookupWithPager(psValue, LookupTable);
                    }
                    else
                    {
                        pbReturn = false;

                    }
                }

            }
            catch (Exception ex)
            {
                pbReturn = false;
                Console.Write(ex);
            }

            return pbReturn;
        }


        public bool SelectValueFromLookupWithOutPager(string psValue, IWebElement LookupTable)
        {
            bool pbReturn = true;
            bool pbExist = false;
            try
            {
                IList<IWebElement> elements = LookupTable.FindElements(By.XPath("//tr/td[1]"));

                foreach (IWebElement e in elements)
                {
                    System.Console.WriteLine(e.Text);
                    if (psValue == e.Text)
                    {
                        e.Click();

                        pbExist = true;
                        pbReturn = true;
                        break;
                    }

                }

                if (pbExist != true)
                {
                    pbReturn = false;
                }

            }
            catch (Exception ex)
            {
                pbReturn = false;
                Console.Write(ex);
            }

            return pbReturn;
        }

        // 
        public bool SelectValueFromLookupWithPager(string psValue, OpenQA.Selenium.IWebElement LookupTable, int ColumID)
        {
            bool pbReturn = true;
            bool pbPagerNextBtnDisabled = false;
            bool pbExist = false;
            try
            {
                IList<IWebElement> elements = LookupTable.FindElements(By.XPath("//tr/td[" + ColumID + "]"));

                IWebElement PagerNextBtn = LookupTable.FindElement(By.TagName("kendo-pager-next-buttons")).FindElement(By.TagName("span"));

                pbPagerNextBtnDisabled = PagerNextBtn.GetAttribute("class").Contains("k-state-disabled");

                foreach (IWebElement e in elements)
                {
                    Console.Write(e.Text);
                    if (psValue == e.Text)
                    {
                        e.Click();
                        pbExist = true;
                        pbReturn = true;
                        break;
                    }

                }

                if (pbExist != true)
                {
                    if (!pbPagerNextBtnDisabled)
                    {
                        PagerNextBtn.Click();
                        pbReturn = SelectValueFromLookupWithPager(psValue, LookupTable);
                    }
                    else
                    {
                        pbReturn = false;
                        Console.Write("Value is not present in Lookup");
                    }
                }

            }
            catch (Exception ex)
            {
                pbReturn = false;
                Console.Write(ex);
            }

            return pbReturn;
        }

        public bool SelectValueFromPopupWithoutpager(string psValue, int ColumID)
        {
            bool pbReturn = true;
            bool pbExist = false;
            try
            {
                IList<IWebElement> elements = BaseClass.driver.FindElements(By.XPath("//tr/td[" + ColumID + "]"));

                foreach (IWebElement e in elements)
                {
                    Console.Write(e.Text);
                    if (psValue == e.Text)
                    {
                        e.Click();
                        pbExist = true;
                        pbReturn = true;
                        break;
                    }

                }

                if (pbExist != true)
                {

                    pbReturn = false;
                }

            }
            catch (Exception ex)
            {
                pbReturn = false;
                Console.Write(ex);
            }

            return pbReturn;
        }


        //public bool SelectCheckBoxFromGridWithoutPager(string psValue, IWebElement LookupTable)
        //{
        //    bool pbReturn = true;
        //    //int psColumID = 4;//Item Code ColumnID
        //    //int piCheckboxColumID = 1;
        //    //bool pbRowSelect = false;
        //    try
        //    {
        //        IList<IWebElement> elements = LookupTable.FindElements(By.XPath("//tr/td[" + CommonID.picklistResultBatchSerialId + "]"));
        //        //IWebElement PagerNextBtn = LookupTable.FindElement(By.TagName("kendo-pager-next-buttons")).FindElement(By.TagName("span"));

        //        IList<IWebElement> element = LookupTable.FindElements(By.XPath("//tr/td[" + CommonID.picklistResultCheckboxId + "]"));
        //        // pbPagerNextBtnDisabled = PagerNextBtn.GetAttribute("class").Contains("k-state-disabled");


        //        for (int i = 0; i < elements.Count; i++)
        //        {
        //            if (psValue == elements[i].Text)
        //            {
        //                System.Console.WriteLine(elements[i].Text);
        //                element[i].Click();
        //                string rowID = element[i].FindElement(By.ClassName("ng-reflect-logical-row-index")).Text;
        //                System.Console.WriteLine(rowID);
        //                //element[i].FindElement(By.XPath("//tr/td[" + CommonID.picklistResultCheckboxId + "]")).Click();
        //                pbReturn = true;
        //                break;

        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        pbReturn = false;
        //        Console.Write(ex);
        //    }

        //    return pbReturn;
        //}

        public String GetValueFromLookupWithoutPager(string psValue, IWebElement LookupTable, int ColumID)
        {
            String ResultText = "";
            try
            {
                IList<IWebElement> elements = LookupTable.FindElements(By.XPath("//tr/td[" + ColumID + "]"));

                foreach (IWebElement e in elements)
                {
                    Console.Write(e.Text);
                    if (psValue == e.Text)
                    {
                        ResultText = e.Text;
                        break;
                    }

                }

            }
            catch (Exception ex)
            {

                Console.Write(ex);
            }

            return ResultText;
        }

        //public void SetValueIntoGridWithoutPager(IWebElement LookupTable, string FName, string Remark, string Taxcode, string DistribMethod, string NetAmount)
        //{

        //    IList<IWebElement> Name = LookupTable.FindElements(By.XPath("//tr/td[" + CommonID.FreightNameColId + "]"));

        //    IList<IWebElement> remark = LookupTable.FindElements(By.XPath("//tr/td[" + CommonID.FreightRemarkColId + "]"));

        //    IList<IWebElement> taxcode = LookupTable.FindElements(By.XPath("//tr/td[" + CommonID.FreightTaxCodeColId + "]"));

        //    IList<IWebElement> DistribMethodDropdown = LookupTable.FindElements(By.XPath("//tr/td[" + CommonID.FreightDistribMethodDropdownColId + "]"));

        //    IList<IWebElement> NAmount = LookupTable.FindElements(By.XPath("//tr/td[" + CommonID.FreightNetAmountColId + "]"));

        //    for (int i = 0; i < remark.Count; i++)
        //    {
        //        if (FName == Name[i].Text)
        //        {
        //            remark[i].FindElement(By.Id(CommonID.FreightRemarkinput)).SendKeys(Remark);
        //            System.Threading.Thread.Sleep(500);
        //            //BaseClass.driver.FindElement(By.Id(CommonID.PicklistInput)).SendKeys(Keys.Tab);
        //            System.Threading.Thread.Sleep(500);

        //            taxcode[i].FindElement(By.Id(CommonID.FreightTaxCodeinput)).SendKeys(Taxcode);
        //            System.Threading.Thread.Sleep(500);
        //            //BaseClass.driver.FindElement(By.Id(CommonID.PicklistInput)).SendKeys(Keys.Tab);
        //            System.Threading.Thread.Sleep(500);

        //            //DistribMethodDropdown.FindElement(By.Id(CommonID.FreightDistribMethodDropdown)).SendKeys(Dropvalue);
        //            BaseClass.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

        //            IWebElement selectElement = DistribMethodDropdown[i].FindElement(By.TagName("select"));
        //            selectElement.SendKeys(DistribMethod);
        //            System.Threading.Thread.Sleep(500);

        //            //NAmount[i].FindElement(By.Id(CommonID.FreightNetAmount)).SendKeys(NetAmount);
        //            //System.Threading.Thread.Sleep(500);
        //            ////BaseClass.driver.FindElement(By.Id(CommonID.PicklistInput)).SendKeys(Keys.Tab);
        //            //System.Threading.Thread.Sleep(500);

        //        }

        //    }
        //    //try
        //    //{
        //    //    IList<IWebElement> TableRow = LookupTable.FindElements(By.TagName("tr"));

        //    //    for (int i = 0; i < TableRow.Count; i++)
        //    //    {


        //    //            IList<IWebElement> TableCol = TableRow[i].FindElements(By.TagName("td"));
        //    //        System.Console.WriteLine(TableRow[i].Text);

        //    //        IWebElement Name = TableRow[i].FindElement(By.XPath("//tr/td[" + CommonID.FreightNameColId + "]"));
        //    //        IWebElement remark = TableRow[i].FindElement(By.XPath("//tr/td[" + CommonID.FreightRemarkColId + "]"));
        //    //        IWebElement taxcode = TableRow[i].FindElement(By.XPath("//tr/td[" + CommonID.FreightTaxCodeColId + "]"));
        //    //        IWebElement DistribMethodDropdown = TableRow[i].FindElement(By.XPath("//tr/td[" + CommonID.FreightDistribMethodDropdownColId + "]"));


        //    //        remark.FindElement(By.Id(CommonID.FreightRemarkinput)).SendKeys(Remark);
        //    //        System.Threading.Thread.Sleep(500);
        //    //        //BaseClass.driver.FindElement(By.Id(CommonID.PicklistInput)).SendKeys(Keys.Tab);
        //    //        System.Threading.Thread.Sleep(500);

        //    //        taxcode.FindElement(By.Id(CommonID.FreightTaxCodeinput)).SendKeys(Taxcode);
        //    //        System.Threading.Thread.Sleep(500);
        //    //        //BaseClass.driver.FindElement(By.Id(CommonID.PicklistInput)).SendKeys(Keys.Tab);
        //    //        System.Threading.Thread.Sleep(500);

        //    //        //DistribMethodDropdown.FindElement(By.Id(CommonID.FreightDistribMethodDropdown)).SendKeys(Dropvalue);
        //    //        BaseClass.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

        //    //        IWebElement selectElement = BaseClass.driver.FindElement(By.TagName("select"));
        //    //        selectElement.SendKeys(DistribMethod);


        //    //    }

        //    //}
        //    //catch (Exception ex)
        //    //{

        //    //    Console.Write(ex);
        //    //}


        //}

        //public void SetValueIntoGridWithoutPager_TaxCodeLookup(IWebElement LookupTable, string FName, string Remark, string Taxcode, string DistribMethod, string NetAmount)
        //{
        //    bool ItemcodeLookupExist = false;

        //    IList<IWebElement> Name = LookupTable.FindElements(By.XPath("//tr/td[" + CommonID.FreightNameColId + "]"));

        //    IList<IWebElement> remark = LookupTable.FindElements(By.XPath("//tr/td[" + CommonID.FreightRemarkColId + "]"));

        //    IList<IWebElement> taxcode = LookupTable.FindElements(By.XPath("//tr/td[" + CommonID.FreightTaxCodeColId + "]"));

        //    IList<IWebElement> DistribMethodDropdown = LookupTable.FindElements(By.XPath("//tr/td[" + CommonID.FreightDistribMethodDropdownColId + "]"));

        //    IList<IWebElement> NAmount = LookupTable.FindElements(By.XPath("//tr/td[" + CommonID.FreightNetAmountColId + "]"));

        //    for (int i = 0; i < remark.Count; i++)
        //    {
        //        if (FName == Name[i].Text)
        //        {
        //            remark[i].FindElement(By.Id(CommonID.FreightRemarkinput)).SendKeys(Remark);
        //            System.Threading.Thread.Sleep(500);
        //            //BaseClass.driver.FindElement(By.Id(CommonID.PicklistInput)).SendKeys(Keys.Tab);
        //            System.Threading.Thread.Sleep(500);

        //            //Select TaxCode via Lookup
        //            BaseClass.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        //            BaseClass.objCommonFunction.Click(CommonID.FreightTaxCodeLookupBtn);

        //            BaseClass.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        //            IWebElement TaxGridTable = BaseClass.objCommonFunction.GetLookupDataUI(CommonID.TaxCode_grid);
        //            BaseClass.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        //            System.Threading.Thread.Sleep(500);

        //            ItemcodeLookupExist = BaseClass.objCommonFunction.SelectValueFromLookupWithOutPager(Taxcode, TaxGridTable);
        //            if (ItemcodeLookupExist == false) { BaseClass.objCommonFunction.Click(CommonID.TaxCode_grid_closebtn); }

        //            //DistribMethodDropdown.FindElement(By.Id(CommonID.FreightDistribMethodDropdown)).SendKeys(Dropvalue);
        //            BaseClass.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

        //            IWebElement selectElement = DistribMethodDropdown[i].FindElement(By.TagName("select"));
        //            selectElement.SendKeys(DistribMethod);
        //            System.Threading.Thread.Sleep(500);

        //            //NAmount[i].FindElement(By.Id(CommonID.FreightNetAmount)).SendKeys(NetAmount);
        //            //System.Threading.Thread.Sleep(500);
        //            ////BaseClass.driver.FindElement(By.Id(CommonID.PicklistInput)).SendKeys(Keys.Tab);
        //            //System.Threading.Thread.Sleep(500);

        //        }

        //    }


        //}


        //For So deluivery

        public bool SO_SelectItemFromGridWithOutPager(string psValue, IWebElement LookupTable)
        {
            bool pbReturn = true;
            bool pbExist = false;
            try
            {
                IList<IWebElement> elements = LookupTable.FindElements(By.XPath("//tr/td[2]"));

                foreach (IWebElement e in elements)
                {
                    System.Console.WriteLine(e.Text);
                    if (psValue == e.Text)
                    {
                        e.Click();

                        pbExist = true;
                        pbReturn = true;
                        break;
                    }

                }

                if (pbExist != true)
                {
                    pbReturn = false;
                }

            }
            catch (Exception ex)
            {
                pbReturn = false;
                Console.Write(ex);
            }

            return pbReturn;
        }


        //public bool SO_SelectCheckBoxFromGridWithPager(string psValue, IWebElement LookupTable)
        //{
        //    bool pbReturn = true;
        //    bool pbPagerNextBtnDisabled = false;
        //    bool pbExist = false;
        //    try
        //    {
        //        IList<IWebElement> elements = LookupTable.FindElements(By.XPath("//tr/td[" + CommonID.SO_DFS_BatchSerialColID + "]"));
        //        IWebElement PagerNextBtn = LookupTable.FindElement(By.TagName("kendo-pager-next-buttons")).FindElement(By.TagName("span"));

        //        IList<IWebElement> element = LookupTable.FindElements(By.XPath("//tr/td[" + CommonID.SO_DFS_CheckboxID + "]"));
        //        pbPagerNextBtnDisabled = PagerNextBtn.GetAttribute("class").Contains("k-state-disabled");

        //        for (int i = 0; i < elements.Count; i++)
        //        {
        //            //System.Threading.Thread.Sleep(500);
        //            if (psValue == elements[i].Text)
        //            {
        //                System.Console.WriteLine(elements[i].Text);
        //                element[i + 1].Click();
        //                string rowID = element[i + 1].FindElement(By.ClassName("ng-reflect-logical-row-index")).Text;
        //                System.Console.WriteLine(rowID);
        //                //  element[i].FindElement(By.XPath("//tr/td[" + CommonID.picklistResultCheckboxId + "]")).Click();
        //                pbReturn = true;
        //                pbExist = true;
        //                break;

        //            }

        //        }

        //        if (pbExist != true)
        //        {
        //            if (!pbPagerNextBtnDisabled)
        //            {
        //                PagerNextBtn.Click();
        //                pbReturn = SO_SelectCheckBoxFromGridWithPager(psValue, LookupTable);
        //            }
        //            else
        //            {
        //                pbReturn = false;
        //                Console.Write("Value is not present in Lookup");
        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        pbReturn = false;
        //        Console.Write(ex);
        //    }

        //    return pbReturn;
        //}

    }
}
