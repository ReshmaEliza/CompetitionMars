using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestMarsCompetition.Utilities;

namespace TestMarsCompetition.Utilities
{
     class WaitUtils: WebdriverManager
    {
       
        public static void WaitElementIsVisible(By ElementLocator,int p)
        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(p));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(ElementLocator));
            

        }


       

        public static void WaitToBeClickable(string locatorType, By locatorValue, int seconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, seconds));

            if (locatorType == "XPath")
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locatorValue));
            }

        }

       
    }
}


