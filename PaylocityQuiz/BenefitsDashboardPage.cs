using System.Collections.Generic;
using OpenQA.Selenium;

namespace PaylocityQuiz
{
    public class BenefitsDashboardPage 
    {
        private IWebDriver webdriver;
        public BenefitsDashboardPage(IWebDriver driver)
        {
            webdriver = driver;
            webdriver.FindElement(By.Id("btnAddEmployee"));
        }

        public void btnDeleteEmployeeClick(int row)
        {
            webdriver.FindElements(By.Id("btnDelete"))[row].Click();
        }

        public EditAddModal btnAddEmployeeClick()
        {
            webdriver.FindElement(By.Id("btnAddEmployee")).Click();
            return new EditAddModal(webdriver);
        }

        public EditAddModal btnEditEmployeeClick(int row)
        {
            var target = webdriver.FindElements(By.Id("btnEdit"))[row - 1];
            target.Click();
            return new EditAddModal(webdriver);
        }

        public List<string> BuildFullTable()
        {
            IWebElement table = webdriver.FindElement(By.Id("employee-table"));
            IList<IWebElement> rows = table.FindElements(By.TagName("tr"));
            List<string> rowVals = new List<string>();       
            foreach (IWebElement row in rows)
            {
                rowVals.Add(row.Text);
            }
            return rowVals;
        }

        public string txtCellValue(int row, int col)
        {
            var cellValue = "";
            cellValue = webdriver.FindElement(By.Id("employee-table")).FindElements(By.TagName("tr"))[row].FindElements(By.TagName("td"))[col].Text;
            return cellValue;
        }

        public class EditAddModal
        {
            private IWebDriver webdriver;
            public EditAddModal(IWebDriver driver)
            {
                webdriver = driver;
                webdriver.FindElement(By.ClassName("modal-title"));
            }

            public void btnSubmitChangesClick()
            {
                webdriver.FindElement(By.XPath("//*[@id=\"employees-form\"]/div[4]/div/button[1]")).Click();
            }

            public void btnCancelChangesClick()
            {
                webdriver.FindElement(By.XPath("//*[@id=\"employees-form\"]/div[4]/div/button[2]")).Click();
            }

            public void txtEditFirstName(string name)
            {
                var target = webdriver.FindElement(By.XPath("//*[@id=\"employees-form\"]/div[1]/div/input"));
                var tText = target.Text;
                if (tText != "") { target.Clear(); }
                target.SendKeys(name);
            }

            public void txtEditLastName(string name)
            {
                var target = webdriver.FindElement(By.XPath("//*[@id=\"employees-form\"]/div[2]/div/input"));
                if (target.Text != "") { target.Clear(); }    
                target.SendKeys(name);
            }

            public void txtEditDependents(string num)
            {
                var target = webdriver.FindElement(By.XPath("//*[@id=\"employees-form\"]/div[3]/div/input"));
                if (target.Text != "") { target.Clear(); }
                target.SendKeys(num);
            }
        }
    }
}
