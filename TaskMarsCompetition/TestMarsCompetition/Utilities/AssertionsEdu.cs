using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Joins;
using System.Reactive;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using TestMarsCompetition.Page;

namespace TestMarsCompetition.Utilities
{
    class AssertionsEdu : WebdriverManager
    {

        protected string pattern = @"^(?:$|(?=.*[a-zA-Z0-9])[a-zA-Z0-9\s-#]+)$";

        String UpdatedElement;
        private static By row_Locator => By.XPath("//div[@data-tab='third']//tbody");
        protected static List<string> table_Values = new List<string>();
        protected static List<string> table_Values_College = new List<string>();
        protected static List<string> table_Values_Degree = new List<string>();
        protected String notification;


        private static By TableElementsEducation_Locator => By.XPath($"//div[@data-tab='third']//td[1]");
        private static IList<IWebElement> TableElementsEducation;

        private static By TableElementscert_Locator => By.XPath($"//div[@data-tab='fourth']//td[4]");
        private static IList<IWebElement> TableElementsCert;
        private static By TableEducationLocator => By.XPath($"//div[@data-tab='third']");
        private static IWebElement TableEducation;
        private static By AddedinstituteNameLocator(string instituteName) => (By.XPath($"//div[@data-tab='third']//td[contains(text(),'{instituteName}')]"));
        String AddedinstituteName;

        private static By AddedCountryLocator(string country) => (By.XPath($"//div[@data-tab='third']//td[contains(text(),'{country}')]"));
        String AddedCountry;
        EducationPage EducationPage = new EducationPage();
        private static By AddedtitleLocator(string title) => (By.XPath($"//div[@data-tab='third']//td[contains(text(),'{title}')]"));
        String Addedtitle;

        private static By AddeddegreeLocator(string degree) => (By.XPath($"//div[@data-tab='third']//td[contains(text(),'{degree}')]"));
        String AddedDegree;


        private static By CollegeNameLocator => (By.XPath("//div[@data-tab='third']//td[2]"));
        IList<IWebElement> CollegeName;

        private static By DegreeNameLocator => (By.XPath("//div[@data-tab='third']//td[4]"));
        IList<IWebElement> DegreeName;

        private static By AddedYearLocator(string yearOfGraduation) => (By.XPath($"//div[@data-tab='third']//td[contains(text(),'{yearOfGraduation}')]"));
        String AddedYear;



        private static By AddedSkillLocator(string value) => (By.XPath($"//div[@data-tab='fourth']//td[contains(text(),'{value}')]"));
        String AddedCert;


        private static readonly By NotificationElementLocator = By.ClassName("ns-box-inner");
        static IWebElement NotificationElement;


        private static By ColumnsEducationLocator => By.XPath("//div[@data-tab='third']//td");

        private static IList<IWebElement> columnsEducation;

        private static By rowsEducationLocator => By.XPath("//div[@data-tab='third']//tbody/tr");
        private static IList<IWebElement> rowsEducation;
        private static By TableSkillLocator => By.XPath($"//div[@data-tab='third']");
        private static IWebElement TableSkill;

        private static By tableEducationLocator => By.XPath($"//div[@data-tab='third']//table");
        private static IWebElement tableEducation;


        public void AddEducationAssert(string instituteName, string country, string title, string degree, string yearOfGraduation)
        {

            //Gets the notification message
            NotificationElement = driver.FindElement(NotificationElementLocator);
            WaitUtils.WaitElementIsVisible(NotificationElementLocator, 5);
            notification = NotificationElement.Text;
            TableElementsEducation = driver.FindElements(TableElementsEducation_Locator);


            if (Regex.IsMatch(instituteName, pattern) && Regex.IsMatch(degree, pattern))//Checks the existance of invalid characters
            { //The following statements analyses the value of the notification and determines the test results
                if (notification.Contains("Education has been added"))
                {
                    NotificationEducationAddedAssert(notification, TableElementsEducation, instituteName, country, title, degree, yearOfGraduation);
                }

                else if (notification.Contains("This information is already exist"))
                {

                    TestContext.Write($"Addition of Education - '{degree}' has not been done due to '{notification}'\n.The system stopped the addition of duplicate entries");
                    EducationPage.cancelButton();
                }
                else if (notification.Contains("Duplicated"))
                {

                    TestContext.Write($"Addition - {degree} has not been done due to '{notification}'\n");
                    EducationPage.cancelButton();
                }
                else if (notification.Contains("Please enter all the fields"))
                {

                    TestContext.Write($"Addition of Education '{degree}' has not been done. Notification from system - '{notification}'\n");
                    EducationPage.cancelButton();


                }
                else
                    Assert.Fail($"Failed Action due to :{notification}");
            }
            else
            {
                
                if (notification.Contains("Education information was invalid"))
                {
                    EducationPage.cancelButton();
                    TestContext.Write($"Addition of '{degree}' has not been done due to {notification}\n");
                }
                else
                    Assert.Fail($"System Allowed addition of invalid characters! Notification from System :{notification}");
            }

        }

        public void DeleteEducationAssert(string DeletedDegree)
        {

            //Gets the notification message
            NotificationElement = driver.FindElement(NotificationElementLocator);
            WaitUtils.WaitElementIsVisible(NotificationElementLocator, 5);
            notification = NotificationElement.Text;
            TableElementsEducation = driver.FindElements(TableElementsEducation_Locator);

            //The following statements analyses the value of the notification and determines the test results

            if (notification.Contains("removed"))
            {
                TestContext.WriteLine($"Notification from system: '{notification}'");
                NotificationEducationDeleted(notification, TableElementsEducation, DeletedDegree);
            }


            else
                Assert.Fail($"Failed Action due to :{notification}");



        }



        public bool HasDuplicatesEducation()
        {
            // Locate the education table

            IList<IWebElement> rowsEdu = driver.FindElements(row_Locator);

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

        public void NotificationEducationAddedAssert(string notification, IList<IWebElement> TableElements, string instituteName, string country, string title, string degree, string yearOfGraduation)
        {
            Thread.Sleep(1000);
            bool hasDuplicates = HasDuplicatesEducation();

            if (hasDuplicates == false)
            {
                AddedinstituteName = driver.FindElement(AddedinstituteNameLocator(instituteName)).Text;
                AddedCountry = driver.FindElement(AddedCountryLocator(country)).Text;
                Addedtitle = driver.FindElement(AddedtitleLocator(title)).Text;
                AddedDegree = driver.FindElement(AddeddegreeLocator(degree)).Text;
                AddedYear = driver.FindElement(AddedYearLocator(yearOfGraduation)).Text;



                if (AddedinstituteName.Equals(instituteName) && AddedDegree.Equals(degree) && AddedYear.Equals(yearOfGraduation) && AddedCountry.Equals(country) && Addedtitle.Equals(title))
                {
                    TestContext.WriteLine($"TEST PASSED- Notification - {notification}");
                }
                else
                    Assert.Fail($"Element {degree} is not added");
            }
            else
                Assert.Fail($"The system allowed the addition of duplicate entires Notification - {notification} for degree - '{degree}'");


        }
        //This method checks if the element has been deleted
        public void NotificationEducationDeleted(string notification, IList<IWebElement> TableElements, string degree)
        {


            // Iterate through each element and add its text to the list
            foreach (IWebElement element in TableElements)
            {
                table_Values.Add(element.Text);

            }
            TestContext.WriteLine("Education details Present Currently:");
            foreach (string Value in table_Values)
            {
                TestContext.WriteLine(Value);
                Assert.That(!Value.Equals(degree), "Deletion Failed");

            }


        }

        public void UpdateAssertions(String oldDegreeValue, string instituteName, string country, string title, string degree, string yearOfGraduation)
        {
            //gets the notification
            NotificationElement = driver.FindElement(NotificationElementLocator);
            WaitUtils.WaitElementIsVisible(NotificationElementLocator, 3);
            notification = NotificationElement.Text;


            TableEducation = driver.FindElement(TableEducationLocator);

            if (Regex.IsMatch(instituteName, pattern) && Regex.IsMatch(degree, pattern))//Checks the existance of invalid characters
            {//The following statements analyses the value of the notification and determines the test results
                if (notification.Contains("Education as been updated"))
                {
                    TableElementsEducation = driver.FindElements(TableElementsEducation_Locator); ;

                    WindowHandlers.ScrollToView(TableEducation);
                    NotificationEducationUpdate(notification, TableElementsEducation, oldDegreeValue, instituteName, country, title, degree, yearOfGraduation);
                }
                else if (notification.Contains("This information is already exist."))
                {

                    TestContext.Write($"Updation of degree '{oldDegreeValue}' has not been done. Notification from system-{notification}\n");
                    EducationPage.cancelButton();
                }
                else if (notification.Contains("Duplicated"))
                {

                    TestContext.Write($"Updation of degree '{oldDegreeValue}' has not been done. Notification from system-{notification}\n");
                    EducationPage.cancelButton();
                }
                else if (notification.Contains("Please enter language and level"))
                {

                    TestContext.Write($"Updation of language '{oldDegreeValue}' has not been done. Notification from system-{notification}\n");
                    EducationPage.cancelButton();
                }

                else

                    Assert.Fail($"Failed Action due to :{notification}");


            }
            else

            {
                if (notification.Contains("Education information was invalid"))
                {

                    TestContext.Write($"Addition of '{oldDegreeValue}' has not been done due to {notification}\n");
                }
                else
                    Assert.Fail($"System Allowed addition of invalid characters! Notification from System :{notification}");
            }


        }


        public void NotificationEducationUpdate(string notification, IList<IWebElement> TableElements, String oldDegreeValue, string instituteName, string country, string title, string degree, string yearOfGraduation)
        {


            bool hasDuplicates = HasDuplicatesEducation();

            if (hasDuplicates == false)
            {
                try
                {
                    AddedinstituteName = driver.FindElement(AddedinstituteNameLocator(instituteName)).Text;
                    AddedCountry = driver.FindElement(AddedCountryLocator(country)).Text;
                    Addedtitle = driver.FindElement(AddedtitleLocator(title)).Text;
                    AddedDegree = driver.FindElement(AddeddegreeLocator(degree)).Text;
                    AddedYear = driver.FindElement(AddedYearLocator(yearOfGraduation)).Text;

                    if (AddedinstituteName.Equals(instituteName) && AddedDegree.Equals(degree) && AddedYear.Equals(yearOfGraduation) && AddedCountry.Equals(country) && Addedtitle.Equals(title))
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

        public void StringLengthAssertion_Education()
        {
            // Retrieves the notification
            NotificationElement = driver.FindElement(NotificationElementLocator);
            WaitUtils.WaitElementIsVisible(NotificationElementLocator, 5);
            notification = NotificationElement.Text;

            CollegeName = driver.FindElements(CollegeNameLocator);
            DegreeName = driver.FindElements(DegreeNameLocator);

            TestContext.WriteLine($"Notification from System: {notification}");

            foreach (IWebElement element in CollegeName)
            {
                table_Values_College.Add(element.Text);
            }

            foreach (IWebElement element in DegreeName)
            {
                table_Values_Degree.Add(element.Text);
            }

            Assert.Multiple(() =>
            {
                foreach (string value in table_Values_College)
                {
                    int l = value.Length;
                    // Checks if the length of characters is greater than 50
                    if (l > 50)
                    {
                        Assert.Fail("System allowed the addition of characters with length > 50 for College Name TextBox");
                    }
                }

                foreach (string value in table_Values_Degree)
                {
                    int l = value.Length;
                    // Checks if the length of characters is greater than 50
                    if (l > 50)
                    {
                        Assert.Fail("System allowed the addition of characters with length > 50 for textbox Degree");
                    }
                }
            });

            Thread.Sleep(1000);
        }

        public void Stability(int OGElementCount)
        {
            TableElementsEducation = driver.FindElements(TableElementsEducation_Locator);
            int ActualCount = TableElementsEducation.Count();
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
