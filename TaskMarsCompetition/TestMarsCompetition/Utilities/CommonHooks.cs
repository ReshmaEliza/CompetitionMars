
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestMarsCompetition.Utilities;
using TestMarsCompetition.Context;

using TestMarsCompetition.Page;
using TestMarsCompetition.ModelEducation;
using TestMarsCompetition.ModelLogin;


namespace TestMarsCompetition.Utilities
{
    
    public class CommonHooks
    {
        public static AventStack.ExtentReports.ExtentReports extent;
        public static ExtentTest test;
        private EducationPage educationPage;
        private CertificatePage certificatePage;
        private TestDataLogin loginData;

        private Login login;

        private TestData testData;
        string testName = TestContext.CurrentContext.Test.Name;

        public CommonHooks()
        {
            extent = new AventStack.ExtentReports.ExtentReports();
            login = new Login();
            educationPage = new EducationPage();
            certificatePage = new CertificatePage();
            
        }


        //report file
        [OneTimeSetUp]
        public void Setup()

        {
            //Create directory for the reporting
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

           
            String reportPath = projectDirectory + "//index.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);

            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host Name", "Local host");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Username", "R");

        }
        [SetUp]
        public  void StartUp()
        {
            
            test = extent.CreateTest(testName);
            WebdriverManager.InitializeDriver();
            loginData = JsonReaderlogin.ReadTestData("Utilities/TestDataLogin.json");
             login.loginPage(loginData.email, loginData.password);
            

            if (testName.Contains("Education", StringComparison.OrdinalIgnoreCase))
            {
                educationPage.GoToTab();
                educationPage.DeleteAllElements();
            }
            else if (testName.Contains("Certificate", StringComparison.OrdinalIgnoreCase))
            {
                certificatePage.GoToTab();
                certificatePage.DeleteAllElements();
            }
         
            
        }


        [TearDown]
        public void AfterTest()


        {
            //Get stacktrace in case of an error for a particular testcase
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;


            DateTime time = DateTime.Now;
            String fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";

            if (status == TestStatus.Failed)
            {

                test.Fail("Test failed", captureScreenShot(WebdriverManager.GetDriver(), fileName));
                test.Log(Status.Fail, "test failed with logtrace" + stackTrace);

            }
            else if (status == TestStatus.Passed)
            {
                TestContext.WriteLine("Test Passed");
               
            }

            extent.Flush();

            // Clean up the added education data if Edu Tests are run
            if (testName.Contains("Education", StringComparison.OrdinalIgnoreCase))
            {
                var addedEducationData = TestContextManager.AddedEducationData;

            foreach (var educationData in addedEducationData)
            {
                Thread.Sleep(1000);
                educationPage.delete(educationData);


            }

            var updatedEducationvalue = TestContextManager.UpdatedEducation;

            // Delete all if update action was performed on  Education 
           
                foreach (var Educationset in updatedEducationvalue)
                {
                    Thread.Sleep(2000);
                    educationPage.delete(Educationset); //  deletion of updated elements for the particular scenario
                }
               

            }

            // Clean up the added cert data if the test for cert is run
            else if (testName.Contains("Certificate", StringComparison.OrdinalIgnoreCase))
            {
                var addedcertData = TestContextManager.AddedCertData;
            
                foreach (var certificationData in addedcertData)
                {
                    Thread.Sleep(1000);
                    certificatePage.delete(certificationData);
                
               

                }
           
                var updatedCertvalue = TestContextManager.UpdatedCert;
                // Delete all updated certificates
                foreach (var certset in updatedCertvalue)
                {
                    Thread.Sleep(2000);
                    certificatePage.delete(certset); //  deletion of updated elements for the particular scenario
                }


            }
            WebdriverManager.QuitDriver();
        }

        public MediaEntityModelProvider captureScreenShot(IWebDriver driver, String screenShotName)//Takes Screenshot

        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();




        }

    }

}

