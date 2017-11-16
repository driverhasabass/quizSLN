using System;
using OpenQA.Selenium.IE;
using OpenQA.Selenium;
using System.Configuration;
using System.IO;

namespace PaylocityQuiz
{
    public class QuizBaseWeb
    {
        public IWebDriver webdriver;
        public string urlToTest;
        public QuizBaseWeb()
        {
            var t = ConfigurationManager.AppSettings["BrowserType"];
            if (t == "IE") { webdriver = IEdriverInitialize(); }
            else if (t == "Chrome") { webdriver = ChromeInitialize(); }
            else throw new Exception("No browser selected in AppConfig");
            webdriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            urlToTest = "file:///" + Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + ConfigurationManager.AppSettings["UrlToTest"];
        }

        private IWebDriver IEdriverInitialize()
        {
            var driverLoc = Path.Combine(Environment.CurrentDirectory); //use AppSettings if driver is on network
            IWebDriver webdriver = new InternetExplorerDriver(driverLoc);
            return webdriver;
        }

        private IWebDriver ChromeInitialize()
        {
            var driverLoc = Path.Combine(Environment.CurrentDirectory); //use AppSettings if driver is on network
            IWebDriver webdriver = new OpenQA.Selenium.Chrome.ChromeDriver(driverLoc);
            return webdriver;
        }

        public void EndTest()
        {
            webdriver.Close();
        }
    }
}
