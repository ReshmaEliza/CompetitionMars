using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMarsCompetition.Context;
using TestMarsCompetition.ModelCert;

using TestMarsCompetition.Page;


using TestMarsCompetition.Utilities;
using OpenQA.Selenium.DevTools.V124.Runtime;



namespace TestMarsCompetition.Tests
{
     class TestCertificate:CommonHooks
    {

        
        private TestDataCert testDatacert;  
        private Login login;
        private CertificatePage certificatePage;
        private AssertionCert assertions;
        

        public TestCertificate() {

            
            
            certificatePage = new CertificatePage();
            
            assertions = new AssertionCert();
            


        }


        [Test, Order(1), Description("TC_001 Validate if the creation of Certificate is successful.")]

        public void TC_001_CreateANewCertificationRecord()
        {

            //Go to Tc01 in the Json input file
            testDatacert = JsonReaderCert.ReadTestData("Utilities/TestDataCertificate-TC001.json");
            var testCase = testDatacert.TestCases.Find(tc => tc.TestCaseId == "Tc01");
            
          
            //Get the input data for Certification
            var certData = testCase.InputData.certificationData;
       
            
            //Add Elements
            certificatePage.AddCert(certData.certificationName, certData.certificationFrom, certData.certificationYear);

            //Collecting the elemnts added in the particular scenario
            TestContextManager.AddedCertData.Add(certData.certificationName);


            //Assertions to verify addition
            assertions.AddCertAssert(certData.certificationName, certData.certificationFrom, certData.certificationYear);
            Thread.Sleep(2000);

        }

        [Test, Order(2), Description("TC_002 Validate if the creation of Certificate fails with special characters. ")]

        public void TC_002_CreateANewSkillRecordWithInvalidCharacters()
        {


            //Go to TC02 in the input file
            testDatacert = JsonReaderCert.ReadTestData("Utilities/TestDataCertificate-TC002.json");
            var testCase = testDatacert.TestCases.Find(tc => tc.TestCaseId == "Tc02");
         
            //Get the input data
            var certData = testCase.InputData.certificationData;
    
            //Add Data
            certificatePage.AddCert(certData.certificationName, certData.certificationFrom, certData.certificationYear);
            //Collecting the elements added in the particular scenario
            TestContextManager.AddedCertData.Add(certData.certificationName);
            
            //Assertions to verify addition
            assertions.AddCertAssert(certData.certificationName, certData.certificationFrom, certData.certificationYear);
            Thread.Sleep(3000);

        }
        [Test, Order(3), Description("TC_003 Validate if the creation of Certification fails when fields are empty")]
        public void TC_003_CreateANewEduRecordWithEmptyCharacters()
        {

            //Go to Tc03 in the Json input file
            testDatacert = JsonReaderCert.ReadTestData("Utilities/TestDataCertificate-TC003.json");
            var testCase = testDatacert.TestCases.Find(tc => tc.TestCaseId == "Tc03");
             Thread.Sleep(1000);
            
            //Get Cert Data 
            var CertDatas = testCase.InputData.certificationDataList;
            

            //Add data
            foreach (var certData in CertDatas)
            {
                certificatePage.AddCert(certData.certificationName, certData.certificationFrom, certData.certificationYear);

                //Collecting the elements added in the particular scenario
                TestContextManager.AddedCertData.Add(certData.certificationName);

                //Assertions to verify addition
                assertions.AddCertAssert(certData.certificationName, certData.certificationFrom, certData.certificationYear);
                Thread.Sleep(3000);
            }
        }


        [Test, Order(4), Description("TC_004 Validate if the creation of Certification fails when Certification value = ' '")]
        public void TC_004_CreateANewEduRecordWithInvalidCharacterSpace()
        {

            //Go to Tc04 in the Json input file
            testDatacert = JsonReaderCert.ReadTestData("Utilities/TestDataCertificate-TC004.json");
            var testCase = testDatacert.TestCases.Find(tc => tc.TestCaseId == "Tc04");
                    
            
            //Get the input data for Certification
            var certData = testCase.InputData.certificationData;
       
            //Adding Elements
            certificatePage.AddCert(certData.certificationName, certData.certificationFrom, certData.certificationYear);
            //Collecting the elements added in the particular scenario
            TestContextManager.AddedCertData.Add(certData.certificationName);

            //Assertions to verify addition
            assertions.AddCertAssert(certData.certificationName, certData.certificationFrom, certData.certificationYear);
            Thread.Sleep(3000);

        }

        [Test, Order(5), Description("TC_005 Verify the update functionality.")]
        public void TC_005_UpdateCertification()
        {


            //Go to Tc05 in the Json input file
            testDatacert = JsonReaderCert.ReadTestData("Utilities/TestDataCertificate-TC005.json");
            var testCase = testDatacert.TestCases.Find(tc => tc.TestCaseId == "Tc05");
                
            
            //Get the input data for Certification
            var certDatas = testCase.InputData.certificationDataList;
            var editCertData = testCase.InputData.editcertificationData;
         

            //Add elements
            foreach (var certData in certDatas)
            {
                certificatePage.AddCert(certData.certificationName, certData.certificationFrom, certData.certificationYear);


                TestContextManager.AddedCertData.Add(certData.certificationName);
                
                Thread.Sleep(1000);
            }

            //Updating the Certification Values
            certificatePage.Update(editCertData.targetCertificate,editCertData.newData.certificationName, editCertData.newData.certificationFrom, editCertData.newData.certificationYear);
            //Assertions to verify Updation 
            assertions.UpdateAssertions(editCertData.targetCertificate, editCertData.newData.certificationName, editCertData.newData.certificationFrom, editCertData.newData.certificationYear);
            TestContextManager.AddUpdatedCert(editCertData.targetCertificate, editCertData.newData.certificationName);
            TestContextManager.RemoveCert(editCertData.targetCertificate);
            
            Thread.Sleep(3000);


        }


        [Test, Order(6), Description("TC_006 Verify the delete functionality.")]
        public void TC_006_DeleteCertification()
        {

            //Go to Tc06 in the Json input file
            testDatacert = JsonReaderCert.ReadTestData("Utilities/TestDataCertificate-TC006.json");
            var testCase = testDatacert.TestCases.Find(tc => tc.TestCaseId == "Tc06");
          
          
            //get the input data for cert
            var certDatas = testCase.InputData.certificationDataList;
            var deletecertdata = testCase.InputData.DeleteCertificateData;
           

            //Add Elements
            foreach (var certData in certDatas)
            {
                certificatePage.AddCert(certData.certificationName, certData.certificationFrom, certData.certificationYear);


                TestContextManager.AddedCertData.Add(certData.certificationName);
                
                certificatePage.CloseNotification();
                Thread.Sleep(1000);
            }

            //Perform Deletion
            certificatePage.delete(deletecertdata.targetcert);
            //Perform Assertions
            assertions.DeleteCertAssert(deletecertdata.targetcert);
            Thread.Sleep(3000);
        }

        [Test, Order(7), Description("TC_007 Verify that the system does not allow adding a Certification that already exists.")]
        public void TC_007_DuplicateEntryCheckForAdditionOfCertification()
        {

            //Go to Tc07 in the Json input file
            testDatacert = JsonReaderCert.ReadTestData("Utilities/TestDataCertificate-TC007.json");
            var testCase = testDatacert.TestCases.Find(tc => tc.TestCaseId == "Tc07");
          
            //Fetch the input values to be added in cert tab
            var certDatas = testCase.InputData.certificationDataList;
            var deleteEducationData = testCase.InputData.DeleteCertificateData;
            int index = 0;
         
            
            //Add Elements
            foreach (var certData in certDatas)
            {
                Thread.Sleep(1000);
                certificatePage.AddCert(certData.certificationName, certData.certificationFrom, certData.certificationYear);

                
                TestContextManager.AddedCertData.Add(certData.certificationName);
                
                if (index == 1)//Assertions to verify the behaviour of system when there is a duplicate entry 
                {
                    assertions.AddCertAssert(certData.certificationName, certData.certificationFrom, certData.certificationYear);
                }
                else
                {
                    certificatePage.CloseNotification();
                }
                index++;
            }




            Thread.Sleep(3000);
        }
        
        [Test, Order(8), Description("TC_008 Verify that the case sensitivity of adding a Cert feature")]
        public void TC_008A_DuplicateEntryCheckWhileUpdatingACertification_ScenarioA()
        {


            //Go to Tc08a in the Json input file
            testDatacert = JsonReaderCert.ReadTestData("Utilities/TestDataCertificate-TC008A.json");
            var testCase = testDatacert.TestCases.Find(tc => tc.TestCaseId == "Tc08a");
            Thread.Sleep(1000);
          
            //Get the input data
            var certDatas = testCase.InputData.certificationDataList;

            int index = 0;
        

            //Add Data
            foreach (var certData in certDatas)
            {

                Thread.Sleep(6000);
                certificatePage.AddCert(certData.certificationName, certData.certificationFrom, certData.certificationYear);
                //Collecting the elements added in the particular scenario

                TestContextManager.AddedCertData.Add(certData.certificationName);
                
                if (index > 0)//Perform Assertion only after the addition of duplicate element
                {
                    assertions.AddCertAssert(certData.certificationName, certData.certificationFrom, certData.certificationYear);
                    Thread.Sleep(1000);
                    certificatePage.CloseNotification();


                }
                else
                {
                    certificatePage.CloseNotification();
                }
                index++;
            }




            Thread.Sleep(6000);
        }


        [Test, Order(9), Description("TC_008 Verify that the case sensitivity of adding a cert feature")]
        public void TC_008B_DuplicateEntryCheckWhileUpdatingAEdu_ScenarioB()
        {


            //Go to Tc08b in the Json input file
            testDatacert = JsonReaderCert.ReadTestData("Utilities/TestDataCertificate-TC008B.json");
            var testCase = testDatacert.TestCases.Find(tc => tc.TestCaseId == "Tc08b");
        
            //Get the input data for Certification
            var certificateDatas = testCase.InputData.certificationDataList;

            int index = 0;
        
            
            //Add data
            foreach (var certData in certificateDatas)
            {

                Thread.Sleep(6000);
                certificatePage.AddCert(certData.certificationName, certData.certificationFrom, certData.certificationYear);
                
                TestContextManager.AddedCertData.Add(certData.certificationName);
                //perform Assertion after the duplicate elements are added
                if (index != 0)
                {
                    assertions.AddCertAssert(certData.certificationName, certData.certificationFrom, certData.certificationYear);
                    Thread.Sleep(1000);
                    certificatePage.CloseNotification();


                }
                else
                {
                    certificatePage.CloseNotification();
                }
                index++;
            }

            Thread.Sleep(6000);
        }
        [Test, Order(10), Description("TC_009 Verify if duplicate entries are blocked in case of updating the entries.")]
        public void TC_009A_DuplicateEntryCheckForAdditionOfCertification_ScenarioA()
        {


            //Go to Tc09a in the Json input file
            testDatacert = JsonReaderCert.ReadTestData("Utilities/TestDataCertificate-TC009A.json");
            var testCase = testDatacert.TestCases.Find(tc => tc.TestCaseId == "Tc09a");
       
            //Get the input data for Certification
            var certDatas = testCase.InputData.certificationDataList;
            var editCertData = testCase.InputData.editcertificationData;
           
            //Add Elements
            foreach (var certData in certDatas)
            {
                certificatePage.AddCert(certData.certificationName, certData.certificationFrom, certData.certificationYear);


                TestContextManager.AddedCertData.Add(certData.certificationName);
                //scenarioContext.Set("Education", educationData.Degree);
                Thread.Sleep(1000);
            }

            //Updating Elements
            certificatePage.Update(editCertData.targetCertificate, editCertData.newData.certificationName, editCertData.newData.certificationFrom, editCertData.newData.certificationYear);
            //Assertions to verify Actions
            assertions.UpdateAssertions(editCertData.targetCertificate, editCertData.newData.certificationName, editCertData.newData.certificationFrom, editCertData.newData.certificationYear);
            //Collecting the elements updated in the particular scenario
            TestContextManager.AddUpdatedCert(editCertData.targetCertificate, editCertData.newData.certificationName);
            Thread.Sleep(3000);


        }

        [Test, Order(11),Description("TC_009 Verify if duplicate entries are blocked in case of updating the entries.")]
        public void TC_009B_DuplicateEntryCheckForAdditionOfCertification_ScenarioB()
        {

            //Go to Tc09b in the Json input file
            testDatacert = JsonReaderCert.ReadTestData("Utilities/TestDataCertificate-TC009B.json");
            var testCase = testDatacert.TestCases.Find(tc => tc.TestCaseId == "Tc09b");
          
            //Add certdata
            var certificateDatas = testCase.InputData.certificationDataList;
            var editCertData = testCase.InputData.editcertificationData;
        
            //Adding Cert Elements
            foreach (var certData in certificateDatas)
            {

                certificatePage.AddCert(certData.certificationName, certData.certificationFrom, certData.certificationYear);
                 TestContextManager.AddedCertData.Add(certData.certificationName);
                
                Thread.Sleep(1000);
            }

            //Update Elements
            certificatePage.Update(editCertData.targetCertificate, editCertData.newData.certificationName, editCertData.newData.certificationFrom, editCertData.newData.certificationYear);
             TestContextManager.AddUpdatedCert(editCertData.targetCertificate, editCertData.newData.certificationName);
            //Assertions to verify the update data
            assertions.UpdateAssertions(editCertData.targetCertificate, editCertData.newData.certificationName, editCertData.newData.certificationFrom, editCertData.newData.certificationYear);

            Thread.Sleep(3000);


        }


        [Test, Order(12), Description("TC_010 Validate the addition of Certification feature with 1000 characters")]
        public void TC_010ValidateTheAdditionOfCertificationFeatureWith1000Characters()
        {

            //Go to Tc10 in the Json input file
            testDatacert = JsonReaderCert.ReadTestData("Utilities/TestDataCertificate-TC010.json");
            var testCase = testDatacert.TestCases.Find(tc => tc.TestCaseId == "Tc10");
            var certData = testCase.InputData.certificationData;
           
            // Generate a random Degree value
            string randomcertificatename = StringUtilities.GenerateRandomString(100);

            string randomcertificationFrom = StringUtilities.GenerateRandomString(100);

            // Update the in-memory test case with the random Degree
            certData.certificationName = randomcertificatename;
            certData.certificationFrom = randomcertificationFrom;
            //Adding data
            certificatePage.AddCert(certData.certificationName, certData.certificationFrom, certData.certificationYear);
            TestContextManager.AddedCertData.Add(certData.certificationName);
            
            assertions.StringLengthAssertion_certificate();




            Thread.Sleep(3000);


        }

        [Test, Order(13), Category("Regression")]
        public void TC_011VerifyTheStabilityOfSystemUnderHighLoad()
        {

            //Go to Tc11 in the Json input file
            testDatacert = JsonReaderCert.ReadTestData("Utilities/TestDataCertificate-TC011.json");
            var testCase = testDatacert.TestCases.Find(tc => tc.TestCaseId == "Tc11");
            Thread.Sleep(1000);
        
            //Add input data
            var certDataCount = testCase.InputData.certificateDataCount;
            var certData = testCase.InputData.certificationData;

            Thread.Sleep(1000);



            //Retrieve the count of elements
            int count = int.Parse(certDataCount.Count);

            for (int i = 0; i < count; i++)
            {
                string randomcertificatename = StringUtilities.GenerateRandomString(100);

                string randomcertificationFrom = StringUtilities.GenerateRandomString(100);

                // Update the in-memory test case with the random Degree
                certData.certificationName = randomcertificatename;
                certData.certificationFrom = randomcertificationFrom;
                //Add values
                certificatePage.AddCert(certData.certificationName, certData.certificationFrom, certData.certificationYear);
                TestContextManager.AddedCertData.Add(certData.certificationName);
          
                
            }
            //Refresh data to get the updated entries
            certificatePage.GoToTab();
            //assertions
            assertions.Stability(count);

            Thread.Sleep(3000);


        }

    }
}
