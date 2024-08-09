using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V125.Network;
using TestMarsCompetition.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;



namespace TestMarsCompetition.Page
{
    public class Login
    {
        protected IWebDriver driver;
        public Login()  {
            driver = WebdriverManager.GetDriver();
        }
        //driver.FindElement(
        private IWebElement  SignIn => driver.FindElement(By.XPath("//div/a[@class='item']"));
          
        private IWebElement Email => driver.FindElement(By.XPath("//input[@name='email']"));
      
       private IWebElement PasswordElement => driver.FindElement(By.XPath("//input[@name='password']"));
   

       private IWebElement loginButton => driver.FindElement(By.XPath("//button[contains(text(),'Login')]"));
      







        public void loginPage(String UserName, String Password)


        {
            //Click on signin button
            SignIn.Click();
         

            //Enter username and password
            
            Email.SendKeys(UserName);
            
            PasswordElement.SendKeys(Password);
            
            loginButton.Click();
            Thread.Sleep(3000);

            //checks if login is successful
            String loginverification_strUrl = driver.Url;
            Assert.That(loginverification_strUrl == "http://localhost:5000/Account/Profile", "Login failed");
        }


    }
}

