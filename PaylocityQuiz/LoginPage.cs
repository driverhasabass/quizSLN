using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace PaylocityQuiz
{
    public class LoginPage 
    {
        private IWebDriver webdriver;
        public LoginPage(IWebDriver driver)
        {
            webdriver = driver;
            webdriver.FindElement(By.Id("btnLogin"));
        }

        private IWebElement btnLogin
        {
            get
            {
                return webdriver.FindElement(By.Id("btnLogin"));
            }
        }

        public IWebElement txtLogin
        {
            get
            {
                return webdriver.FindElement(By.Name("form-username"));
            }
        }

        public IWebElement txtPassword
        {
            get
            {
                return webdriver.FindElement(By.Name("form-password"));
            }
        }

        public void btnLoginClick()
        {
            btnLogin.Click();
        }
    }
}
