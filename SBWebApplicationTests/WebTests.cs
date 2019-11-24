using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SBWebApplicationTests
{
    public class WebTests
    {
        private string appURL;
        [SetUp]
        public void Initialize()
        {
            appURL = "http://212.73.150.14/TomaPelican/login.aspx?URL=/default.aspx";
           
            
        }

        private IWebDriver GetDriver(string browser)
        {
            switch (browser)
            {
                case "Chrome":
                   return new ChromeDriver();
                case "Firefox":
                   return new FirefoxDriver();
                    break;
                default:
                   return new ChromeDriver();
            }
        }
        [Test]
        public void OpenAppTest()
        {
            var browser = "Chrome";
            using (var driver = GetDriver(browser))
            {
                driver.Navigate().GoToUrl(appURL + "/");
                driver.FindElement(By.XPath("//input[@id='loginBtn']")).Click();
                Assert.IsTrue(driver.Title.Contains("Pelican"), "Verified title of the page");
            }
        }

        [Test]
        public void LoginTest()
        {
            var browser = "Chrome";
            using (var driver = GetDriver(browser))
            {
                driver.Navigate().GoToUrl(appURL + "/");

                var userNameBox = driver.FindElement(By.Id("userNameBox"));
                userNameBox.SendKeys("Admin");
                Console.WriteLine("User name was send.");

                var passwordBox = driver.FindElement(By.Id("passWordBox"));
                passwordBox.SendKeys("Pelican");
                Console.WriteLine("Password was send.");

                var loginButton = driver.FindElement(By.Id("loginBtn"));
                loginButton.Click();
                Console.WriteLine("Button was submitted.");
                new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists((By.ClassName("loginControl"))));
                var loginControls = driver.FindElements(By.ClassName("loginControl"));
                Assert.AreEqual(1, loginControls.Count);

            }
        }

        [TearDown]
        public void Close()
        {
        }
    }
}