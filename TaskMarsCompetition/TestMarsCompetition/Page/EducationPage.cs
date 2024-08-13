using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using TestMarsCompetition.Page;
using TestMarsCompetition.Utilities;

using System.Runtime.InteropServices;


namespace TestMarsCompetition.Page
{
    class EducationPage
    {
        private IWebElement Institute_Name;
        private IWebElement Degree;
        private IWebElement Education_Tab;
        private IWebElement deleteButton;
        protected IWebDriver driver;
        IWebElement closenotification;

        protected static List<string> table_Values = new List<string>();

        private By AddButtonLanguageLocator = By.XPath("//input[@value='Add']");
        private IWebElement AddButton;

        private static By UpdateButtonLocator = By.XPath("//input[@value='Update']");
        private IWebElement updateButton;
        private By CancelButtonLocator => By.XPath("//input[@value='Cancel']");
        private IWebElement Cancel_Button;
        private By DropDownLocator(String Name) => By.XPath($"//div[@class='row']//select[@name='{Name}']");
        IWebElement dropDown;
        private static By TabLocator => By.XPath($"//a[text() = 'Education']");
        private IWebElement TabSelected;
        private static By addLocator = By.XPath($"//div[@data-tab='third']//th[@class='right aligned']/div[contains(text(),'Add New')]");
        IWebElement AddNew;

        private static By deleteAllbuttonLocator => By.XPath($"//div[@data-tab='third']//td/parent::tr//span[@class='button'][2]");
        private static IWebElement deleteAllButton;
        private static By deleteElementButtonLocator(string ElementtobeDelete) => By.XPath($"//div[@data-tab='third']//td[(text()='{ElementtobeDelete}')]/parent::tr//span[@class='button'][2]");
        private IWebElement deleteElement;


        private static By AddTextBoxLocatorCollege => By.XPath("//div[@class='row']//input[@placeholder='College/University Name']");
        private IWebElement AddTextBoxCollege;

        private static By AddTextBoxLocatorDegree => By.XPath($"//div[@class='row']//input[@placeholder='Degree']");
        private IWebElement AddTextBoxDegree;
        private static By tableLocator => By.XPath($"//div[@data-tab='third']");
        private static IWebElement TableChoice;

        private static By rowLocator(string EducationAdded) => By.XPath($"//td[contains(text(),'{EducationAdded}')]/parent::tr//span[@class='button'][1]");
        private IWebElement RowtobeUpdated;
        private static By TableElementsColoumn1_Locator => By.XPath($"//div[@data-tab='third']//td[1]");
        private static IList<IWebElement> TableElements;

        private static By EditCollegeLocator => By.XPath("//div[@class='fields']//input[@placeholder='College/University Name']");
        private IWebElement EditCollege;
        private static By EditDegreeLocator => By.XPath("//div[@class='fields']//input[@placeholder='Degree']");
        private IWebElement EditDegree;

        private static By ValueLocator(string value) => (By.XPath($"//div[@data-tab='third']//td[contains(text(),'{value}')]"));
        String AddedValue;


        public EducationPage()
        {

            driver = WebdriverManager.GetDriver();
            driver = WebdriverManager.GetDriver();
        }
        public void GoToTab()
        {



            driver.Navigate().Refresh();
            driver.Navigate().Refresh();
            Education_Tab = driver.FindElement(TabLocator);
            Education_Tab.Click();

        }

        public void DeleteAllElements()
        {

            try
            {


                TableChoice = driver.FindElement(tableLocator);
                TableElements = driver.FindElements(TableElementsColoumn1_Locator);
                int count = TableElements.Count();

                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        deleteAllButton = driver.FindElement(deleteAllbuttonLocator);

                        WindowHandlers.ScrollToView(TableChoice);
                        Thread.Sleep(2000);

                        deleteAllButton.Click();

                    }

                }
            }
            catch
            {
                TestContext.WriteLine("No elements found for deletion");
            }
        }

        public void cancelButton()
        {
            Cancel_Button = driver.FindElement(CancelButtonLocator);
            Cancel_Button.Click();
        }
        public void AddEducation(string instituteName, string country, string title, string degree, string yearOfGraduation)



        {
            Thread.Sleep(3000);
            //Clicking Add Button
            
            AddNew = driver.FindElement(addLocator);
            WaitUtils.WaitToBeClickable("Xpath", addLocator, 20);
            
            AddNew.Click();

            //Entering Institution Name
            AddTextBoxCollege = driver.FindElement(AddTextBoxLocatorCollege);
            AddTextBoxCollege.SendKeys(instituteName);

            //Choose the country dropdown and click on the value
            DropDown("country", country);


            //Choose the title 
            DropDown("title", title);

            //Entering Degree Name
            Degree = driver.FindElement(AddTextBoxLocatorDegree);
            
            Degree.SendKeys(degree);

            //Choose the graduation year
            DropDown("yearOfGraduation", yearOfGraduation);

            //click on add to confirm
            AddButton = driver.FindElement(AddButtonLanguageLocator);
            //SafeClick(AddButtonLanguageLocator);
            AddButton.Click();

        }


        public void delete(String ElementtobeDelete)
        {

            try
            {
                //Finding Deletebutton for requested element
                deleteElement = driver.FindElement(deleteElementButtonLocator(ElementtobeDelete));
                WindowHandlers.ScrollToView(deleteElement);
                Thread.Sleep(2000);
                WaitUtils.WaitToBeClickable("Xpath", deleteElementButtonLocator(ElementtobeDelete), 15);

                deleteElement.Click();
                Thread.Sleep(3000);

            }
            catch
            {

                TestContext.WriteLine($"Degree to be be deleted '{ElementtobeDelete}' was not found in the table");
            }

        }
        public void DropDown(String NameofDropdown, String selectedValue)
        {
            //SElect the drop down
            dropDown = driver.FindElement(DropDownLocator(NameofDropdown));
            SelectElement s = new SelectElement(dropDown);
            s.SelectByText(selectedValue);


        }

        public void Update(String oldDegreeValue, string instituteName, string country, string title, string Newdegree, string yearOfGraduation)
        {

            try //check if the element to be updated is present
            {
                //move to the the row to be updated
                RowtobeUpdated = driver.FindElement(rowLocator(oldDegreeValue)); ;
                WindowHandlers.ScrollToView(RowtobeUpdated);
                Thread.Sleep(3000);

                //Click edit on the requested element
                RowtobeUpdated.Click();

                Thread.Sleep(2000);
                //Enter the new values
                EditCollege = driver.FindElement(EditCollegeLocator);
                EditCollege.Clear();
                EditCollege.SendKeys(instituteName);
                //Choose the country dropdown and click on the value
                DropDown("country", country);


                //Choose the title 
                DropDown("title", title);


                //Entering Degree Name
                EditDegree = driver.FindElement(EditDegreeLocator);
                EditDegree.Clear();
                EditDegree.SendKeys(Newdegree);

                //Choose the graduation year
                DropDown("yearOfGraduation", yearOfGraduation);


                updateButton = driver.FindElement(UpdateButtonLocator);
                Thread.Sleep(1000);
                //CLick on update
                updateButton.Click();


            }
            catch // When the element to be updated is not present
            {
                TestContext.WriteLine($"Education -  '{oldDegreeValue}' which was requested to be updated is not present in the table");

            }

        }

        public void CloseNotification()
        {
            closenotification = driver.FindElement(By.XPath("//a[@class='ns-close']"));
            closenotification.Click();
        }

        public IWebElement SafeFindElement(By locator, int timeoutInSeconds = 10)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(locator));
            }
            catch (NoSuchElementException)
            {
                TestContext.WriteLine($"Element with locator: {locator.ToString()} not found.");
                return null;
            }
        }

        public void SafeSendKeys(By locator, string text, int timeoutInSeconds = 10)
        {
            var element = SafeFindElement(locator, timeoutInSeconds);
            if (element != null)
            {
                element.SendKeys(text);
            }
        }

        public void SafeClick(By locator, int timeoutInSeconds = 10)
        {
            var element = SafeFindElement(locator, timeoutInSeconds);
            if (element != null)
            {
                element.Click();
            }
        }


    }
}
