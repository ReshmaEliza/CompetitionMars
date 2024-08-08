using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework.Interfaces;
using TestMarsCompetition.Context;
using TestMarsCompetition.ModelEducation;
using TestMarsCompetition.Page;
using TestMarsCompetition.Pages;
using TestMarsCompetition.Utilities;





namespace TestMarsCompetition.Tests
{



    public class TestEducation : CommonHooks
    {


        private LoginData loginData;
      
        private Login login;
        
        private TestData testData;
        private EducationPage educationPage;
        private AssertionsEdu assertions;
        


        public TestEducation()
        {


             testData = JsonReaderEdu.ReadTestData("Utilities/TestDataEdu.json");
              loginData = testData.LoginData;
            educationPage = new EducationPage();
            login = new Login();
            assertions = new AssertionsEdu();
               
        }

        [Test, Order(1),Description("TC_001 Validate if the creation of Education is successful .")]

        public void TC_001_CreateANewEducationRecord()
        {
            //Login to the website
            login.loginPage(loginData.email, loginData.password);
            //Go to Tc01 in the Json input file
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc01");
            Thread.Sleep(1000);

            //Navigate to Education Tab
            educationPage.GoToTab();

            //Get the input data for education
            var educationData = testCase.InputData.EducationData;
            
            //Delete Elements if presents
            educationPage.DeleteAllElements();
            Thread.Sleep(3000);
            educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);

            //Collecting the elemnts added in the particular scenario
            TestContextManager.AddedEducationData.Add(educationData.Degree);
            
            //Assertions to verify addition
            assertions.AddEducationAssert(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);
            Thread.Sleep(2000);

        }

        [Test, Order(2),Description("TC_002 Validate if the creation of Education fails with special characters. ")]

        public void TC_002_CreateANewSkillRecordWithInvalidCharacters()
        {
            //Login
            login.loginPage(loginData.email, loginData.password);
            //Go to TC02 in the input file
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc02");
            Thread.Sleep(1000);
            //Go to education tab
            educationPage.GoToTab();

            //Get the input data for education
            var educationData = testCase.InputData.EducationData;

            //Delete all elements
            educationPage.DeleteAllElements();

            //Add Education Data
            educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);
            //Collecting the elements added in the particular scenario
            TestContextManager.AddedEducationData.Add(educationData.Degree);

            //Assertions to verify addition
            assertions.AddEducationAssert(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);
            Thread.Sleep(3000);

        }
        [Test, Order(3),Description("TC_003 Validate if the creation of Education fails when fields are empty")]
        public void TC_003_CreateANewEduRecordWithEmptyCharacters()
        {
            //Login to the website
            login.loginPage(loginData.email, loginData.password);
            //Go to Tc03 in the Json input file
             var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc03");
            Thread.Sleep(1000);
            //Navigate to Education Tab
            educationPage.GoToTab();
            //Get the input data for education
            var educationData = testCase.InputData.EducationData;

            //Delete Elements if present
            educationPage.DeleteAllElements();
            //Adding elements
            educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);

            //Collecting the elements added in the particular scenario
            TestContextManager.AddedEducationData.Add(educationData.Degree);

            //Assertions to verify addition
            assertions.AddEducationAssert(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);
            Thread.Sleep(3000);

        }

        [Test, Order(4),Description("TC_004 Validate if the creation of Education fails when Education value = ' '")]
        public void TC_004_CreateANewEduRecordWithInvalidCharacterSpace()
        {
            //Login to the website
            login.loginPage(loginData.email, loginData.password);
            //Go to Tc04 in the Json input file
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc04");
            Thread.Sleep(1000);
            //Navigate to Education Tab
            educationPage.GoToTab();
            //Get the input data for education
            var educationData = testCase.InputData.EducationData;

            //Delete Elements if presents
            educationPage.DeleteAllElements();
            //Adding Elements
            educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);

            //Collecting the elements added in the particular scenario
            TestContextManager.AddedEducationData.Add(educationData.Degree);

            //Assertions to verify addition
            assertions.AddEducationAssert(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);
            Thread.Sleep(3000);

        }


        [Test, Order(5),Description("TC_005 Verify the update functionality.")]
        public void TC_005_UpdateEducation()
        {
            //Login to the website
            login.loginPage(loginData.email, loginData.password);
            //Go to Tc05 in the Json input file
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc05");
              Thread.Sleep(1000);
            //Navigate to Education Tab
            educationPage.GoToTab();

            //Get the input data for education
            var educationDatas = testCase.InputData.EducationDataList;
            var editeducationData = testCase.InputData.EditEducationData;
            ////Delete Elements if presents
            educationPage.DeleteAllElements();
            Thread.Sleep(1000);
            //Adding Elements
            foreach (var educationData in educationDatas)
            {
                educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);

                //Collecting the elements added in the particular scenario

                TestContextManager.AddedEducationData.Add(educationData.Degree);
                Thread.Sleep(1000);
            }

            //Updating the Education Values
            educationPage.Update(editeducationData.targetdegree, editeducationData.NewData.InstituteName, editeducationData.NewData.Country, editeducationData.NewData.Title, editeducationData.NewData.Degree, editeducationData.NewData.YearOfGraduation);
            //Assertions to verify Updation 
            assertions.UpdateAssertions(editeducationData.targetdegree, editeducationData.NewData.InstituteName, editeducationData.NewData.Country, editeducationData.NewData.Title, editeducationData.NewData.Degree, editeducationData.NewData.YearOfGraduation);
            //Collecting the new elements added in the particular scenario
            TestContextManager.AddUpdatedEducation(editeducationData.targetdegree, editeducationData.NewData.Degree);
            
            Thread.Sleep(3000);


        }

        [Test, Order(6),Description("TC_006 Verify the delete functionality.")]
        public void TC_006_DeleteEducation()
        {
            //Login to the website
            login.loginPage(loginData.email, loginData.password);
            //Go to Tc06 in the Json input file
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc06");
            Thread.Sleep(1000);
            //Navigate to Education Tab
            educationPage.GoToTab();

            //Get the input data for education
            var educationDatas = testCase.InputData.EducationDataList;
            var deleteEducationData = testCase.InputData.DeleteEducationData;
            //Delete Elements if presents
            educationPage.DeleteAllElements();
            Thread.Sleep(1000);

            //Add input data
            foreach (var educationData in educationDatas)
            {
                educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);

                //Collecting the elements added in the particular scenario
                 TestContextManager.AddedEducationData.Add(educationData.Degree);
                
                educationPage.CloseNotification();
                Thread.Sleep(1000);
            }

            //Deleting Element
            educationPage.delete(deleteEducationData.targetdegree);
            //Assertions to verify deletion
            assertions.DeleteEducationAssert(deleteEducationData.targetdegree);
            Thread.Sleep(3000);
                    }

        [Test, Order(7),Description("TC_007 Verify that the system does not allow adding a Education that already exists.")]
        public void TC_007_DuplicateEntryCheckForAdditionOfEducation()
        {
            //Login to the website
            login.loginPage(loginData.email, loginData.password);
            //Go to Tc07 in the Json input file
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc07");
            Thread.Sleep(1000);
            //Navigate to Education Tab
            educationPage.GoToTab();
            //Get the input data for education
            var educationDatas = testCase.InputData.EducationDataList;
            var deleteEducationData = testCase.InputData.DeleteEducationData;
            int index = 0;

            //Delete Elements if presents
            educationPage.DeleteAllElements();
            Thread.Sleep(1000);
            //Add Elements
            foreach (var educationData in educationDatas)
            {
                Thread.Sleep(1000);
                educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);

                //Collecting the elements added in the particular scenario
                TestContextManager.AddedEducationData.Add(educationData.Degree);
                
                if (index == 1)
                {//Assertions to verify the behaviour of system when there is a duplicate entry 
                    assertions.AddEducationAssert(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);
                }
                else {
                    educationPage.CloseNotification();
                }
                index++;
            }



            
            Thread.Sleep(3000);
        }


        [Test, Order(8),Description("TC_008 Verify that the case sensitivity of adding a Education feature.")]
        public void TC_008A_DuplicateEntryCheckWhileUpdatingAEducation_ScenarioA()
        {
            //Login to the website
            login.loginPage(loginData.email, loginData.password);
            //Go to Tc08a in the Json input file
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc08a");
            Thread.Sleep(1000);
            educationPage.GoToTab();

            //Get the input data for education
            var educationDatas = testCase.InputData.EducationDataList;
            
            int index = 0;
            //Delete Elements if present
            educationPage.DeleteAllElements();
            Thread.Sleep(1000);
            //Add Elements
            foreach (var educationData in educationDatas)
            {
                
              
                educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);
                //Collecting the elements added in the particular scenario
                TestContextManager.AddedEducationData.Add(educationData.Degree);
                
                if (index > 0)
                {//Perform Assertion only after the addition of duplicate element
                    assertions.AddEducationAssert(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);
                 Thread.Sleep(1000);
                 educationPage.CloseNotification();


                }
                else
                {
                    educationPage.CloseNotification();
                }
                index++;
            }

                       
            
            
        }


        [Test, Order(9),Description("TC_008 Verify that the case sensitivity of adding a Education feature .")]
        public void TC_008B_DuplicateEntryCheckWhileUpdatingAEdu_ScenarioB()
        {
            //Login to the website
            login.loginPage(loginData.email, loginData.password);
            //Go to Tc08b in the Json input file
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc08b");
            Thread.Sleep(1000);
            //Go to Education Tab
            educationPage.GoToTab();
            //Get the input data for education
            var educationDatas = testCase.InputData.EducationDataList;

            int index = 0;
            //Perform delete if elements are present
            educationPage.DeleteAllElements();
            Thread.Sleep(1000);
            //Add Data
            foreach (var educationData in educationDatas)
            {

            
                educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);
                TestContextManager.AddedEducationData.Add(educationData.Degree);
                
                if (index !=0)
                {
                    //perform Assertion after the duplicate elements are added
                    assertions.AddEducationAssert(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);
                    Thread.Sleep(1000);
                    educationPage.CloseNotification();


                }
                else
                {
                    educationPage.CloseNotification();
                }
                index++;
            }

            
        }

        [Test, Order(10),Description("TC_009 Verify if duplicate entries are blocked in case of updating the entries.")]
        public void TC_009A_DuplicateEntryCheckForAdditionOfEducation_ScenarioA()
        {
            ////Login to the website
            login.loginPage(loginData.email, loginData.password);
            //Go to Tc09a in the Json input file
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc09a");
            Thread.Sleep(1000);
            //Navigate to Education Tab
            educationPage.GoToTab();
            //Get the input data for education
            var educationDatas = testCase.InputData.EducationDataList;
            var editeducationData = testCase.InputData.EditEducationData;

            educationPage.DeleteAllElements();
            Thread.Sleep(1000);
            //Add Elements
            foreach (var educationData in educationDatas)
            {
                educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);

                //Collecting the elements added in the particular scenario

                TestContextManager.AddedEducationData.Add(educationData.Degree);
                
                Thread.Sleep(1000);
            }

            //Updating Elements
            educationPage.Update(editeducationData.targetdegree, editeducationData.NewData.InstituteName, editeducationData.NewData.Country, editeducationData.NewData.Title, editeducationData.NewData.Degree, editeducationData.NewData.YearOfGraduation);
            
            //Assertions to verify Actions
            assertions.UpdateAssertions(editeducationData.targetdegree, editeducationData.NewData.InstituteName, editeducationData.NewData.Country, editeducationData.NewData.Title, editeducationData.NewData.Degree, editeducationData.NewData.YearOfGraduation);

            //Collecting the elements updated in the particular scenario
            TestContextManager.AddUpdatedEducation(editeducationData.targetdegree, editeducationData.NewData.Degree);
            
            Thread.Sleep(3000);


        }

        [Test, Order(11), Description("TC_009 Verify if duplicate entries are blocked in case of updating the entries.")]
        public void TC_009B_DuplicateEntryCheckForAdditionOfEducation_ScenarioB()
        {
            //Login to the website
            login.loginPage(loginData.email, loginData.password);
            //Go to Tc09b in the Json input file
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc09b");
            Thread.Sleep(1000);

            educationPage.GoToTab();
            //Get the input data for education
            var educationDatas = testCase.InputData.EducationDataList;
            var editeducationData = testCase.InputData.EditEducationData;

            //Delete Elements
            educationPage.DeleteAllElements();
            Thread.Sleep(1000);
            //Add elements
            foreach (var educationData in educationDatas)
            {
                educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);
                 TestContextManager.AddedEducationData.Add(educationData.Degree);
                
                
                Thread.Sleep(1000);
            }

            //Update Elements
            educationPage.Update(editeducationData.targetdegree, editeducationData.NewData.InstituteName, editeducationData.NewData.Country, editeducationData.NewData.Title, editeducationData.NewData.Degree, editeducationData.NewData.YearOfGraduation);
            TestContextManager.AddUpdatedEducation(editeducationData.targetdegree, editeducationData.NewData.Degree);
            //Assertions to verify the update data
            assertions.UpdateAssertions(editeducationData.targetdegree, editeducationData.NewData.InstituteName, editeducationData.NewData.Country, editeducationData.NewData.Title, editeducationData.NewData.Degree, editeducationData.NewData.YearOfGraduation);
            Thread.Sleep(3000);


        }

        [Test, Order(12), Description("TC_010 Validate the addition of Education feature with 1000 characters")]
        public void TC_010ValidateTheAdditionOfEducationFeatureWith1000Characters()
        {
            //Login to the website
            login.loginPage(loginData.email, loginData.password);
            //Go to Tc10 in the Json input file
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc10");
            Thread.Sleep(1000);
            educationPage.GoToTab();
            //Get the input data for education
            var educationData = testCase.InputData.EducationData;

            educationPage.DeleteAllElements();
            Thread.Sleep(1000);
            // Generate a random Degree value
            string randomDegree =StringUtilities.GenerateRandomString(100);

            string randomCollege = StringUtilities.GenerateRandomString(100);

            // Update the in-memory test case with the random Degree
            educationData.Degree = randomDegree;
            educationData.InstituteName = randomCollege;
            //Add data
            educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);
            TestContextManager.AddedEducationData.Add(educationData.Degree);
            //Assert the length of the added data
            assertions.StringLengthAssertion_Education();
                    


            
            Thread.Sleep(3000);


        }

        [Test, Order(13), Category("Regression")]
        public void TC_011VerifyTheStabilityOfSystemUnderHighLoad()
        {
            //Login to the website
            login.loginPage(loginData.email, loginData.password);
            //Go to Tc11 in the Json input file
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc11");
            Thread.Sleep(1000);
            //Navigate to Education Tab
            educationPage.GoToTab();
            var EducationDataCount = testCase.InputData.EducationDataCount;
            var educationData = testCase.InputData.EducationData;

            educationPage.DeleteAllElements();
            Thread.Sleep(1000);
                      

          //Get the number of elements to be added from json file
            int count = int.Parse(EducationDataCount.Count);

            for (int i = 0; i < count; i++)
            {
                // Generate a random Degree value
                string randomDegree = StringUtilities.GenerateRandomString(50);

                string randomCollege = StringUtilities.GenerateRandomString(50);
                // Update the in-memory test case with the random Degree
                educationData.Degree = randomDegree;
                educationData.InstituteName = randomCollege;
                educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);
                TestContextManager.AddedEducationData.Add(educationData.Degree);
                
            }
            //Refresh data to get the updated entries
            educationPage.GoToTab();
            //Verify the stability under high load
            assertions.Stability(10);

            Thread.Sleep(3000);


        }
    }
}