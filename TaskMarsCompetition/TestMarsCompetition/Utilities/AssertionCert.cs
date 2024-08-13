using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestMarsCompetition.Page;

namespace TestMarsCompetition.Utilities
{
    class AssertionCert : WebdriverManager
    {
        protected string pattern = @"^(?:$|(?=.*[a-zA-Z0-9])[a-zA-Z0-9\s-#]+)$";

        String UpdatedElement;



        protected static List<string> table_Values = new List<string>();
        protected static List<string> table_Values_cert = new List<string>();
        protected static List<string> table_Values_certFrom = new List<string>();
        protected String notification;
        CertificatePage certificatePage = new CertificatePage();

        IList<IWebElement> rowsEdu;
        private static By row_Locator => By.XPath("//div[@data-tab='fourth']//tbody");
        private static By TableElementsCert_Locator => By.XPath($"//div[@data-tab='fourth']//td[1]");
        private static IList<IWebElement> TableElementsCert;
        


        private static By TableElementscertfrom_Locator => By.XPath($"//div[@data-tab='fourth']//td[2]");
        private static IList<IWebElement> TableElementsCertfrom;
        private static By TablecertLocator => By.XPath($"//div[@data-tab='fourth']");
        private static IWebElement Tablecert;
        private static By AddedCertNameLocator(string certificationName) => (By.XPath($"//div[@data-tab='fourth']//td[contains(text(),'{certificationName}')]"));
        String AddedcertName;

        private static By AddedCertLocator(string certificationFrom) => (By.XPath($"//div[@data-tab='fourth']//td[contains(text(),'{certificationFrom}')]"));
        String AddedCert;




        private static By CertLocator => (By.XPath("//div[@data-tab='fourth']//td[1]"));
        IList<IWebElement> CertName;

        private static By CertfromLocator => (By.XPath("//div[@data-tab='fourth']//td[2]"));
        IList<IWebElement> CertfromName;

        private static By AddedYearLocator(string year) => (By.XPath($"//div[@data-tab='fourth']//td[contains(text(),'{year}')]"));
        String AddedYear;


        private static readonly By NotificationElementLocator = By.ClassName("ns-box-inner");
        static IWebElement NotificationElement;


        private static By ColumnsCertLocator => By.XPath("//div[@data-tab='fourth']//td");

        private static IList<IWebElement> columnsCert;

        private static By rowsCertLocator => By.XPath("//div[@data-tab='fourth']//tbody/tr");
        private static IList<IWebElement> rowsCert;
        private static By TableCertLocator => By.XPath($"//div[@data-tab='fourth']");
        private static IWebElement TableCert;

        private static By tablecertLocator => By.XPath($"//div[@data-tab='fourth']//table");
        private static IWebElement tablecert;

        public void AddCertAssert(string certificationName, string certificationFrom, string certificationYear)
        {

            //Gets the notification message
            NotificationElement = driver.FindElement(NotificationElementLocator);
            WaitUtils.WaitElementIsVisible(NotificationElementLocator, 5);
            notification = NotificationElement.Text;
            TableElementsCert = driver.FindElements(TableElementsCert_Locator);


            if (Regex.IsMatch(certificationName, pattern) && Regex.IsMatch(certificationFrom, pattern))//Checks the existance of invalid characters
            { //The following statements analyses the value of the notification and determines the test results
                if (notification.Contains(" has been added to your certification"))
                {
                    
                    NotificationCertAddedAssert(notification, TableElementsCert, certificationName, certificationFrom, certificationYear);
                }

                else if (notification.Contains("This information is already exist"))
                {
                    certificatePage.cancelButton();
                    TestContext.Write($"Addition of Certificate - '{certificationName}' has not been done due to '{notification}'\n.The system stopped the addition of duplicate entries");
                }
                else if (notification.Contains("Duplicated"))
                {
                    certificatePage.cancelButton();
                    TestContext.Write($"Addition - {certificationName} has not been done due to '{notification}'\n");
                }
                else if (notification.Contains("Please enter"))
                {
                    certificatePage.cancelButton();
                    TestContext.Write($"Addition of certificate '{certificationName}' has not been done. Notification from system - '{notification}'\n");
                }
                else
                    Assert.Fail($"Failed Action due to :{notification}");
            }
            else
            {
                if (notification.Contains("certificate information was invalid"))
                {
                    certificatePage.cancelButton();
                    TestContext.Write($"Addition of '{certificationName}' has not been done due to {notification}\n");
                }
                else
                    Assert.Fail($"System Allowed addition of invalid characters! Notification from System :{notification}");
            }

        }

        public void DeleteCertAssert(string DeletedCert)
        {

            //Gets the notification message
            NotificationElement = driver.FindElement(NotificationElementLocator);
            WaitUtils.WaitElementIsVisible(NotificationElementLocator, 5);
            notification = NotificationElement.Text;
            TableElementsCert = driver.FindElements(TableElementsCert_Locator);

            //The following statements analyses the value of the notification and determines the test results

            if (notification.Contains("has been deleted from your certification"))
            {
                TestContext.WriteLine($"Notification from system: '{notification}'");
                NotificationcertificateDeleted(notification, TableElementsCert, DeletedCert);
            }


            else
                Assert.Fail($"Failed Action due to :{notification}");



        }


        public bool HasDuplicatescertificate()
        {
            // Locate the cert table

            rowsEdu = driver.FindElements(row_Locator);

            // HashSet to store unique rows
            HashSet<string> seenRows = new HashSet<string>();


            // Iterate through each row in the table
            foreach (var row in rowsEdu)
            {
                table_Values.Add(row.Text.ToLower());
            }


            // Check if the row data is already seen
            foreach (string Value in table_Values)
            {

                
                if (!seenRows.Add(Value))
                {
                    // Print a message indicating a duplicate was found
                    TestContext.WriteLine($"Duplicate row found: {Value}");
                    return true; // Duplicate found
                }

            }

            // Print the count of unique rows
            TestContext.WriteLine($"Number of unique rows in the HashSet: {seenRows.Count}");

            return false; // No duplicates found  

        }

        public void NotificationCertAddedAssert(string notification, IList<IWebElement> TableElements, string certificationName, string certificationFrom, string certificationYear)
        {
            Thread.Sleep(1000);
            bool hasDuplicates = HasDuplicatescertificate();

            if (hasDuplicates == false)
            {
                AddedcertName = driver.FindElement(AddedCertNameLocator(certificationName)).Text;
                AddedCert = driver.FindElement(AddedCertLocator(certificationFrom)).Text;
                AddedYear = driver.FindElement(AddedYearLocator(certificationYear)).Text;


                if (AddedcertName.Equals(certificationName) && AddedCert.Equals(certificationFrom) && AddedYear.Equals(certificationYear))
                {
                    TestContext.WriteLine($"TEST PASSED- Notification - {notification}");
                }
                else
                    Assert.Fail($"Element {certificationName} is not added");
            }
            else
                Assert.Fail($"The system allowed the addition of duplicate entires Notification - {notification} for certificate - '{certificationName}'");


        }
        //This method checks if the element has been deleted
        public void NotificationcertificateDeleted(string notification, IList<IWebElement> TableElements, string cert)
        {


            // Iterate through each element and add its text to the list
            foreach (IWebElement element in TableElements)
            {
                table_Values.Add(element.Text);

            }
            TestContext.WriteLine("certificate details Present Currently:");
            foreach (string Value in table_Values)
            {
                TestContext.WriteLine(Value);
                Assert.That(!Value.Equals(cert), "Deletion Failed");

            }


        }

        public void UpdateAssertions(String oldCertValue, string certificationName, string certificationFrom, string certificationYear)
        {
            //gets the notification
            NotificationElement = driver.FindElement(NotificationElementLocator);
            WaitUtils.WaitElementIsVisible(NotificationElementLocator, 3);
            notification = NotificationElement.Text;


            TableCert = driver.FindElement(TablecertLocator);

            if (Regex.IsMatch(certificationName, pattern) && Regex.IsMatch(certificationFrom, pattern))//Checks the existance of invalid characters
            {//The following statements analyses the value of the notification and determines the test results
                if (notification.Contains("has been updated to your certification"))
                {
                    TableElementsCert = driver.FindElements(TableElementsCert_Locator); ;

                    WindowHandlers.ScrollToView(TableCert);
                    NotificationCertUpdate(notification, TableElementsCert, certificationName, certificationFrom, certificationYear);
                }
                else if (notification.Contains("This information is already exist."))
                {
                    certificatePage.cancelButton();
                    TestContext.Write($"Updation of certificate '{oldCertValue}' has not been done. Notification from system-{notification}\n");
                }
                else if (notification.Contains("Duplicated"))
                {
                    certificatePage.cancelButton();
                    TestContext.Write($"Updation of certificate '{oldCertValue}' has not been done. Notification from system-{notification}\n");
                }
                else if (notification.Contains("Please enter language and level"))
                {
                    certificatePage.cancelButton();
                    TestContext.Write($"Updation of language '{oldCertValue}' has not been done. Notification from system-{notification}\n");
                }

                else

                    Assert.Fail($"Failed Action due to :{notification}");


            }
            else

            {
                if (notification.Contains("certificate information was invalid"))
                {
                    certificatePage.cancelButton();
                    TestContext.Write($"Addition of '{oldCertValue}' has not been done due to {notification}\n");
                }
                else
                    Assert.Fail($"System Allowed addition of invalid characters! Notification from System :{notification}");
            }


        }


        public void NotificationCertUpdate(string notification, IList<IWebElement> TableElements, string certificationName, string certificationFrom, string certificationYear)
        {


            bool hasDuplicates = HasDuplicatescertificate();

            if (hasDuplicates == false)
            {
                try
                {
                    AddedcertName = driver.FindElement(AddedCertNameLocator(certificationName)).Text;
                    AddedCert = driver.FindElement(AddedCertLocator(certificationFrom)).Text;
                    AddedYear = driver.FindElement(AddedYearLocator(certificationYear)).Text;


                    if (AddedcertName.Equals(certificationName) && AddedCert.Equals(certificationFrom) && AddedYear.Equals(certificationYear))
                    {
                        TestContext.WriteLine($"TEST PASSED- Notification - {notification}");
                    }
                }

                catch
                {
                    Assert.Fail("Element not added in the table");
                }
            }

            else
                Assert.Fail($"The system allowed the addition of duplicate entires.Notification from system - {notification}");

        }

        public void StringLengthAssertion_certificate()
        {
            // Retrieves the notification
            NotificationElement = driver.FindElement(NotificationElementLocator);
            WaitUtils.WaitElementIsVisible(NotificationElementLocator, 5);
            notification = NotificationElement.Text;

            CertName = driver.FindElements(CertLocator);
            CertfromName = driver.FindElements(CertfromLocator);

            TestContext.WriteLine($"Notification from System: {notification}");

            foreach (IWebElement element in CertName)
            {
                table_Values_cert.Add(element.Text);
            }

            foreach (IWebElement element in CertfromName)
            {
                table_Values_certFrom.Add(element.Text);
            }

            Assert.Multiple(() =>
            {
                foreach (string value in table_Values_cert)
                {
                    int l = value.Length;
                    // Checks if the length of characters is greater than 50
                    if (l > 50)
                    {
                        Assert.Fail("System allowed the addition of characters with length > 50 for College Name TextBox");
                    }
                }

                foreach (string value in table_Values_certFrom)
                {
                    int l = value.Length;
                    // Checks if the length of characters is greater than 50
                    if (l > 50)
                    {
                        Assert.Fail("System allowed the addition of characters with length > 50 for textbox certificate");
                    }
                }
            });

            Thread.Sleep(1000);
        }

        public void Stability(int OGElementCount)
        {
            TableElementsCert = driver.FindElements(TableElementsCert_Locator);
            int ActualCount = TableElementsCert.Count();
            //checks if all the requested elements are added
            if (ActualCount == OGElementCount)
            {
                TestContext.WriteLine("All Elements are added");

            }
            else
            {

                Assert.Fail($"Expected Count of Elements " + OGElementCount + " .Actual Count " + ActualCount + "");
            }
        }

    }
}
