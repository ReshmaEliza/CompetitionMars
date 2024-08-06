
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestMarsCompetition.Utilities;
using TestMarsCompetition.Context;
using TestMarsCompetition.Pages;
using TestMarsCompetition.Page;


namespace TestMarsCompetition.Utilities
{
    
    public class CommonHooks
    {
        public static AventStack.ExtentReports.ExtentReports extent;
        public static ExtentTest test;
        private EducationPage educationPage;
        private CertificatePage certificatePage;
        


        public CommonHooks()
        {
            extent = new AventStack.ExtentReports.ExtentReports();

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
        public static void StartUp()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            WebdriverManager.InitializeDriver();

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

            // Clean up the added education data
            
                var addedEducationData = TestContextManager.AddedEducationData;

            foreach (var educationData in addedEducationData)
            {
                Thread.Sleep(1000);
                educationPage.delete(educationData);


            }

            var updatedEducationvalue = TestContextManager.UpdatedEducation;
            // Delete all updated languages
            foreach (var Educationset in updatedEducationvalue)
            {
                Thread.Sleep(2000);
                educationPage.delete(Educationset); //  deletion of updated elements for the particular scenario
            }
        
            // Clean up the added cert data
        
                var addedcertData = TestContextManager.AddedCertData;
            
                foreach (var certificationData in addedcertData)
                {
                    Thread.Sleep(1000);
                    certificatePage.delete(certificationData);
                
               

                }

                var updatedCertvalue = TestContextManager.UpdatedCert;
                // Delete all updated languages
                foreach (var certset in updatedCertvalue)
                {
                    Thread.Sleep(2000);
                    certificatePage.delete(certset); //  deletion of updated elements for the particular scenario
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

