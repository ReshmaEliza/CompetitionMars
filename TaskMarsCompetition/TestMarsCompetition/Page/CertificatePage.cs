using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMarsCompetition.Utilities;

namespace TestMarsCompetition.Page
{
    class CertificatePage
    {

        private IWebElement Certificate_Name;
        private IWebElement Cert_From;
        private IWebElement Cert_Tab;
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
        private By DropDownLocator => By.XPath($"//div[@class='row']//select[@name='certificationYear']");
        IWebElement dropDown;
        private static By TabLocator => By.XPath($"//a[text() = 'Certifications']");
        private IWebElement TabSelected;
        private static By addLocator = By.XPath($"//div[@data-tab='fourth']//th[@class='right aligned']/div[contains(text(),'Add New')]");
        IWebElement AddNew;

        private static By deleteAllbuttonLocator => By.XPath($"//div[@data-tab='fourth']//td/parent::tr//span[@class='button'][2]");
        private static IWebElement deleteAllButton;
        private static By deleteElementButtonLocator(string ElementtobeDelete) => By.XPath($"//div[@data-tab='fourth']//td[text() = '{ElementtobeDelete}']/parent::tr//span[@class='button'][2]");
        private IWebElement deleteElement;


        private static By AddTextBoxCertLocator => By.XPath($"//div[@class='row']//input[@placeholder='Certificate or Award']");
        private IWebElement AddTextBoxCert;

        private static By AddTextBoxLocatorCertfrom => By.XPath($"//div[@class='row']//input[@placeholder='Certified From (e.g. Adobe)']");
        private IWebElement AddTextBoxCertFrom;
        private static By tableLocator => By.XPath($"//div[@data-tab='fourth']");
        private static IWebElement TableChoice;

        private static By rowLocator(string EducationAdded) => By.XPath($"//td[contains(text(),'{EducationAdded}')]/parent::tr//span[@class='button'][1]");
        private IWebElement RowtobeUpdated;
        private static By TableElementsColoumn1_Locator => By.XPath($"//div[@data-tab='fourth']//td[1]");
        private static IList<IWebElement> TableElements;

        private static By EditCertLocator => By.XPath("//div[@class='fields']//input[@placeholder='Certificate or Award']");
        private IWebElement EditCert;
        private static By EditCertFromLocator => By.XPath("//div[@class='fields']//input[@placeholder='Certified From (e.g. Adobe)']");
        private IWebElement EditCertFrom;

        private static By ValueLocator(string value) => (By.XPath($"//div[@data-tab='fourth']//td[contains(text(),'{value}')]"));
        String AddedValue;



        public CertificatePage()
        {

            driver = WebdriverManager.GetDriver();
        }
        public void GoToTab()
        {



            driver.Navigate().Refresh();
            driver.Navigate().Refresh();
            Cert_Tab = driver.FindElement(TabLocator);
            Cert_Tab.Click();

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
                        Thread.Sleep(3000);

                        deleteAllButton.Click();

                    }

                }
            }
            catch
            {
                TestContext.WriteLine("No elements found for deletion");
            }
        }

        public void AddCert(string certificationName, string certificationFrom, string certificationYear)



        {
            Thread.Sleep(3000);
            //Clicking Add Button

            AddNew = driver.FindElement(addLocator);
            WaitUtils.WaitToBeClickable("Xpath", addLocator, 30);
            Thread.Sleep(3000);
            AddNew.Click();

            //Entering cert Name
            AddTextBoxCert = driver.FindElement(AddTextBoxCertLocator);
            AddTextBoxCert.SendKeys(certificationName);

            //Entering cert from
            AddTextBoxCertFrom = driver.FindElement(AddTextBoxLocatorCertfrom);
            AddTextBoxCertFrom.SendKeys(certificationFrom);

            //Choose the year dropdown and click on the value
            DropDown(certificationYear);

            //click on add to confirm
            AddButton = driver.FindElement(AddButtonLanguageLocator);
            AddButton.Click();

        }



        public void delete(string ElementtobeDelete)
        {


            if (IsElementPresent(deleteElementButtonLocator(ElementtobeDelete)))
            {

                // Perform the delete operation
                deleteElement = driver.FindElement(deleteElementButtonLocator(ElementtobeDelete));
                WindowHandlers.ScrollToView(deleteElement);
                Thread.Sleep(2000);


                deleteElement.Click();
                Thread.Sleep(3000);

            }
            else
            {
                TestContext.WriteLine($"The element to be deleted '{ElementtobeDelete}' was not found in the table.");
            }
        }

        private bool IsElementPresent(By locator)
        {
            try
            {
                driver.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void DropDown(String selectedValue)
        {
            //SElect the drop down
            dropDown = driver.FindElement(DropDownLocator);
            SelectElement s = new SelectElement(dropDown);
            s.SelectByText(selectedValue);


        }

        public void Update(string oldcertificatevalue, string certificationName, string certificationFrom, string certificationYear)
        {

            try //check if the element to be updated is present
            {
                //move to the the row to be updated
                RowtobeUpdated = driver.FindElement(rowLocator(oldcertificatevalue)); ;
                WindowHandlers.ScrollToView(RowtobeUpdated);
                Thread.Sleep(3000);

                //Click edit on the requested element
                RowtobeUpdated.Click();

                Thread.Sleep(2000);
                //Enter the new values
                EditCert = driver.FindElement(EditCertLocator);
                EditCert.Clear();
                EditCert.SendKeys(certificationName);

                //Entering cert from value
                EditCertFrom = driver.FindElement(EditCertFromLocator);
                EditCertFrom.Clear();
                EditCertFrom.SendKeys(certificationFrom);
                //Choose the year dropdown and click on the value
                DropDown(certificationYear);

                updateButton = driver.FindElement(UpdateButtonLocator);
                Thread.Sleep(1000);
                //CLick on update
                updateButton.Click();


            }
            catch // When the element to be updated is not present
            {
                TestContext.WriteLine($"Education -  '{oldcertificatevalue}' which was requested to be updated is not present in the table");

            }

        }

        public void CloseNotification()
        {
            closenotification = driver.FindElement(By.XPath("//a[@class='ns-close']"));
            closenotification.Click();
        }



        public void cancelButton()
        {
            Cancel_Button = driver.FindElement(CancelButtonLocator);
            Cancel_Button.Click();
        }



    }
}

