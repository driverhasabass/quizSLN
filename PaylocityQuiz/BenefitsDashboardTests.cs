using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Collections.Generic;
using System.IO;

namespace PaylocityQuiz
{
    [TestClass]
    public class BenefitsDashboardTests : QuizBaseWeb
    {
        [TestMethod]
        public void AddEmployeeWithoutDiscount()
        {
            try
            {
                var userName = ConfigurationManager.AppSettings["UserName"];
                var password = ConfigurationManager.AppSettings["Password"];
                webdriver.Navigate().GoToUrl(urlToTest);
                LoginPage loginPage = new LoginPage(webdriver);             
                loginPage.txtLogin.SendKeys(userName);
                loginPage.txtPassword.SendKeys(password);
                loginPage.btnLoginClick();

                BenefitsDashboardPage bdPage = new BenefitsDashboardPage(webdriver);
                BenefitsDashboardPage.EditAddModal addEmpModal = bdPage.btnAddEmployeeClick();

                //first name does not begin with 'A'
                var empFName = "Bucky";
                var empLName = "O'Hare";
                var numDep = 2;
                addEmpModal.txtEditFirstName(empFName);
                addEmpModal.txtEditLastName(empLName);
                addEmpModal.txtEditDependents(numDep.ToString());
                addEmpModal.btnSubmitChangesClick();

                List<string> tableData = bdPage.BuildFullTable();

                int index = tableData.FindIndex(f => f.Contains("Bucky"));
                Assert.IsTrue(index > 0, "The new employee was not found in the table.");
                var expBenefitCost = Math.Round(((500 * numDep) + 1000) / 26.0, 2, MidpointRounding.AwayFromZero).ToString();
                var actualBenefitCost = bdPage.txtCellValue(index, 6);
                Assert.AreEqual(expBenefitCost, actualBenefitCost, "The expected benefit cost was expected to be " + expBenefitCost + " but is actually " + actualBenefitCost);
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                EndTest();
            }
        }

        [TestMethod]
        public void AddEmployeeWithDiscount()
        {
            try
            {
                var userName = ConfigurationManager.AppSettings["UserName"];
                var password = ConfigurationManager.AppSettings["Password"];
                webdriver.Navigate().GoToUrl(urlToTest);
                LoginPage loginPage = new LoginPage(webdriver);
                loginPage.txtLogin.SendKeys(userName);
                loginPage.txtPassword.SendKeys(password);
                loginPage.btnLoginClick();

                BenefitsDashboardPage bdPage = new BenefitsDashboardPage(webdriver);
                BenefitsDashboardPage.EditAddModal addEmpModal = bdPage.btnAddEmployeeClick();

                //first name does begin with 'A'
                var empFName = "Adrian";
                var empLName = "Balboa";
                var numDep = 2;
                addEmpModal.txtEditFirstName(empFName);
                addEmpModal.txtEditLastName(empLName);
                addEmpModal.txtEditDependents(numDep.ToString());
                addEmpModal.btnSubmitChangesClick();

                List<string> tableData = bdPage.BuildFullTable();

                int index = tableData.FindIndex(f => f.Contains("Adrian"));
                Assert.IsTrue(index > 0, "The new employee was not found in the table.");
                var expBenefitCost = Math.Round((((500 * numDep) + 1000) *.9) / 26.0, 2, MidpointRounding.AwayFromZero).ToString();
                var actualBenefitCost = bdPage.txtCellValue(index, 6);
                Assert.AreEqual(expBenefitCost, actualBenefitCost, "The expected benefit cost was expected to be " + expBenefitCost + " but is actually " + actualBenefitCost);
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                EndTest();
            }
        }
    }
}
