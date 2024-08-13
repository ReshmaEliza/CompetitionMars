using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMarsCompetition.Utilities
{
     public class WebdriverManager
    {
        

        protected static IWebDriver driver;

        // Method to initialize the WebDriver
        public static void InitializeDriver()
        {
            if (driver == null)
            {
               driver = new ChromeDriver();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40);
                driver.Manage().Window.Maximize();
                driver.Url = "http://localhost:5000/";
            }
        }

        // Method to get the WebDriver instance
        public static IWebDriver GetDriver()
        {
            if (driver == null)
            {
                InitializeDriver();
            }
            return driver;
        }

        // Method to quit the WebDriver
        public static void QuitDriver()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                   driver = null;
            }
        }
    }
}
