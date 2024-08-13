using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework.Interfaces;
using TestMarsCompetition.ModelEducation;
using TestMarsCompetition.Page;

using TestMarsCompetition.Utilities;





namespace TestMarsCompetition.Tests
{



    public class TestEducation : CommonHooks
    {

        private TestData testData;
        private EducationPage educationPage;
        private AssertionsEdu assertions;



        public TestEducation()
        {


            educationPage = new EducationPage();

            assertions = new AssertionsEdu();

        }


        [Test, Order(1), Description("TC_001 Validate if the creation of Education is successful .")]

        public void TC_001_CreateANewEducationRecord()
        {


            //Go to Tc01 in the Json input file
            testData = JsonReaderEdu.ReadTestData("Utilities/TestDataEdu-TC001.json");
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc01");


            //Get the input data for education
            var educationData = testCase.InputData.EducationData;


            educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);



            //Assertions to verify addition
            try
            {
                assertions.AddEducationAssert(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);
                Thread.Sleep(3000);
            }
            // Cleanup: Delete the education data added
            finally
            {
                educationPage.delete(educationData.Degree);
            }
        }

        [Test, Order(2), Description("TC_002 Validate if the creation of Education fails with special characters. ")]

        public void TC_002_CreateANewSkillRecordWithInvalidCharacters()
        {


            //Go to TC02 in the input file
            testData = JsonReaderEdu.ReadTestData("Utilities/TestDataEdu-TC002.json");
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc02");
            Thread.Sleep(1000);
            //Get the input data for education
            var educationData = testCase.InputData.EducationData;

            //Add Education Data
            educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);



            //Assertions to verify addition
            try
            {
                assertions.AddEducationAssert(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);
                Thread.Sleep(3000);
            }
            // Cleanup: Delete the education data added
            finally
            {
                educationPage.delete(educationData.Degree);
            }
        }
        [Test, Order(3), Description("TC_003 Validate if the creation of Education fails when fields are empty")]
        public void TC_003_CreateANewEduRecordWithEmptyCharacters()
        {


            //Go to Tc03 in the Json input file
            testData = JsonReaderEdu.ReadTestData("Utilities/TestDataEdu-TC003.json");
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc03");
            Thread.Sleep(1000);

            //Get the input data for education
            var educationData = testCase.InputData.EducationData;

            //Adding elements
            educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);


            //Assertions to verify addition
            try
            {
                assertions.AddEducationAssert(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);
                Thread.Sleep(2000);
            }
            //Cleanup of added data in the run
            finally
            {
                educationPage.delete(educationData.Degree);
            }
        }

        [Test, Order(4), Description("TC_004 Validate if the creation of Education fails when Education value = ' '")]
        public void TC_004_CreateANewEduRecordWithInvalidCharacterSpace()
        {


            //Go to Tc04 in the Json input file
            testData = JsonReaderEdu.ReadTestData("Utilities/TestDataEdu-TC004.json");
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc04");
            Thread.Sleep(1000);

            //Get the input data for education
            var educationData = testCase.InputData.EducationData;

            //Adding Elements
            educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);


            //Assertions to verify addition
            try
            {
                assertions.AddEducationAssert(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);
                Thread.Sleep(3000);
            }
            finally//Delete added data in the given run
            {
                educationPage.delete(educationData.Degree);
            }
        }


        [Test, Order(5), Description("TC_005 Verify the update functionality.")]
        public void TC_005_UpdateEducation()
        {
            //Go to Tc05 in the Json input file
            testData = JsonReaderEdu.ReadTestData("Utilities/TestDataEdu-TC005.json");
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc05");
            Thread.Sleep(1000);


            //Get the input data for education
            var educationDatas = testCase.InputData.EducationDataList;
            var editeducationData = testCase.InputData.EditEducationData;

            //Adding Elements
            foreach (var educationData in educationDatas)
            {
                educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);

                Thread.Sleep(1000);
            }

            //Updating the Education Values
            educationPage.Update(editeducationData.targetdegree, editeducationData.NewData.InstituteName, editeducationData.NewData.Country, editeducationData.NewData.Title, editeducationData.NewData.Degree, editeducationData.NewData.YearOfGraduation);
            //Assertions to verify Updation 
            try
            {
                assertions.UpdateAssertions(editeducationData.targetdegree, editeducationData.NewData.InstituteName, editeducationData.NewData.Country, editeducationData.NewData.Title, editeducationData.NewData.Degree, editeducationData.NewData.YearOfGraduation);
                Thread.Sleep(3000);
            }
            finally
            {
                //delete all the added data
                foreach (var educationData in educationDatas)
                {
                    educationPage.delete(educationData.Degree);
                    Thread.Sleep(1000);
                }
                //Delete newly updated data
                educationPage.delete(editeducationData.NewData.Degree);


            }

        }

        [Test, Order(6), Description("TC_006 Verify the delete functionality.")]
        public void TC_006_DeleteEducation()
        {


            //Go to Tc06 in the Json input file
            testData = JsonReaderEdu.ReadTestData("Utilities/TestDataEdu-TC006.json");
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc06");
            Thread.Sleep(1000);


            //Get the input data for education
            var educationDatas = testCase.InputData.EducationDataList;
            var deleteEducationData = testCase.InputData.DeleteEducationData;


            //Add input data
            foreach (var educationData in educationDatas)
            {
                educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);


                educationPage.CloseNotification();
                Thread.Sleep(1000);
            }

            //Deleting Element
            educationPage.delete(deleteEducationData.targetdegree);
            //Assertions to verify deletion
            try
            {
                assertions.DeleteEducationAssert(deleteEducationData.targetdegree);
                Thread.Sleep(3000);
            }
            finally
            {
                foreach (var educationData in educationDatas)
                {
                    educationPage.delete(educationData.Degree);
                    Thread.Sleep(1000);
                }
            }

        }

        [Test, Order(7), Description("TC_007 Verify that the system does not allow adding a Education that already exists.")]
        public void TC_007_DuplicateEntryCheckForAdditionOfEducation()
        {


            //Go to Tc07 in the Json input file
            testData = JsonReaderEdu.ReadTestData("Utilities/TestDataEdu-TC007.json");
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc07");
            Thread.Sleep(1000);


            //Get the input data for education
            var educationDatas = testCase.InputData.EducationDataList;
            var deleteEducationData = testCase.InputData.DeleteEducationData;
            int index = 0;


            //Add Elements
            try
            {
                foreach (var educationData in educationDatas)
                {
                    Thread.Sleep(1000);
                    educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);



                    if (index == 1)
                    {//Assertions to verify the behaviour of system when there is a duplicate entry 
                        assertions.AddEducationAssert(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);
                    }
                    else
                    {
                        educationPage.CloseNotification();
                    }
                    index++;
                }




                Thread.Sleep(3000);
            }
            finally
            {
                foreach (var educationData in educationDatas)
                {

                    educationPage.delete(educationData.Degree);
                }

            }
        }


        [Test, Order(8), Description("TC_008 Verify that the case sensitivity of adding a Education feature.")]
        public void TC_008A_DuplicateEntryCheckWhileUpdatingAEducation_ScenarioA()
        {


            //Go to Tc08a in the Json input file
            testData = JsonReaderEdu.ReadTestData("Utilities/TestDataEdu-TC008A.json");
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc08a");
            Thread.Sleep(1000);


            //Get the input data for education
            var educationDatas = testCase.InputData.EducationDataList;

            int index = 0;

            //Add Elements
            try
            {
                foreach (var educationData in educationDatas)
                {


                    educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);


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
            finally
            {
                foreach (var educationData in educationDatas)
                {


                    educationPage.delete(educationData.InstituteName);

                }
            }
        }


        [Test, Order(9), Description("TC_008 Verify that the case sensitivity of adding a Education feature .")]
        public void TC_008B_DuplicateEntryCheckWhileUpdatingAEdu_ScenarioB()
        {


            //Go to Tc08b in the Json input file
            testData = JsonReaderEdu.ReadTestData("Utilities/TestDataEdu-TC008B.json");
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc08b");
            Thread.Sleep(1000);

            //Get the input data for education
            var educationDatas = testCase.InputData.EducationDataList;

            int index = 0;

            //Add Data
            try
            {
                foreach (var educationData in educationDatas)
                {


                    educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);


                    if (index != 0)
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
            finally
            {
                foreach (var educationData in educationDatas)
                {
                    educationPage.delete(educationData.InstituteName);
                }
            }
        }

        [Test, Order(10), Description("TC_009 Verify if duplicate entries are blocked in case of updating the entries.")]
        public void TC_009A_DuplicateEntryCheckForAdditionOfEducation_ScenarioA()
        {


            //Go to Tc09a in the Json input file
            testData = JsonReaderEdu.ReadTestData("Utilities/TestDataEdu-TC009A.json");
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc09a");
            Thread.Sleep(1000);

            //Get the input data for education
            var educationDatas = testCase.InputData.EducationDataList;
            var editeducationData = testCase.InputData.EditEducationData;


            //Add Elements
            foreach (var educationData in educationDatas)
            {
                educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);



                Thread.Sleep(1000);
            }

            //Updating Elements
            educationPage.Update(editeducationData.targetdegree, editeducationData.NewData.InstituteName, editeducationData.NewData.Country, editeducationData.NewData.Title, editeducationData.NewData.Degree, editeducationData.NewData.YearOfGraduation);

            //Assertions to verify Actions
            try
            {
                assertions.UpdateAssertions(editeducationData.targetdegree, editeducationData.NewData.InstituteName, editeducationData.NewData.Country, editeducationData.NewData.Title, editeducationData.NewData.Degree, editeducationData.NewData.YearOfGraduation);
                Thread.Sleep(3000);
            }
            finally//Cleanup
            {

                //Deletion of added elements
                foreach (var educationData in educationDatas)
                {
                    educationPage.delete(educationData.Degree);

                }
                //Deletion of updated elements
                educationPage.delete(editeducationData.NewData.Degree);


            }
        }
        [Test, Order(11), Description("TC_009 Verify if duplicate entries are blocked in case of updating the entries.")]
        public void TC_009B_DuplicateEntryCheckForAdditionOfEducation_ScenarioB()
        {


            //Go to Tc09b in the Json input file
            testData = JsonReaderEdu.ReadTestData("Utilities/TestDataEdu-TC009B.json");
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc09b");
            Thread.Sleep(1000);


            //Get the input data for education
            var educationDatas = testCase.InputData.EducationDataList;
            var editeducationData = testCase.InputData.EditEducationData;

            //Add elements
            foreach (var educationData in educationDatas)
            {
                educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);


                Thread.Sleep(1000);
            }

            //Update Elements
            educationPage.Update(editeducationData.targetdegree, editeducationData.NewData.InstituteName, editeducationData.NewData.Country, editeducationData.NewData.Title, editeducationData.NewData.Degree, editeducationData.NewData.YearOfGraduation);
            //Assertions to verify the update data
            try
            {
                assertions.UpdateAssertions(editeducationData.targetdegree, editeducationData.NewData.InstituteName, editeducationData.NewData.Country, editeducationData.NewData.Title, editeducationData.NewData.Degree, editeducationData.NewData.YearOfGraduation);
                Thread.Sleep(3000);
            }
            finally//Cleanup Data
            {
                //Delete updated data
                educationPage.delete(editeducationData.NewData.Degree);

                foreach (var educationData in educationDatas)
                {
                    educationPage.delete(educationData.Degree);
                }

            }

        }

        [Test, Order(12), Description("TC_010 Validate the addition of Education feature with 1000 characters")]
        public void TC_010ValidateTheAdditionOfEducationFeatureWith1000Characters()
        {


            //Go to Tc10 in the Json input file
            testData = JsonReaderEdu.ReadTestData("Utilities/TestDataEdu-TC010.json");
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc10");
            Thread.Sleep(1000);

            //Get the input data for education
            var educationData = testCase.InputData.EducationData;


            Thread.Sleep(1000);
            // Generate a random Degree value
            string randomDegree = StringUtilities.GenerateRandomString(100);

            string randomCollege = StringUtilities.GenerateRandomString(100);

            // Update the in-memory test case with the random Degree
            educationData.Degree = randomDegree;
            educationData.InstituteName = randomCollege;
            //Add data
            educationPage.AddEducation(educationData.InstituteName, educationData.Country, educationData.Title, educationData.Degree, educationData.YearOfGraduation);
            try
            {
                //Assert the length of the added data
                assertions.StringLengthAssertion_Education();
                Thread.Sleep(3000);
            }
            finally
            {
                educationPage.delete(educationData.Degree);

            }

        }

        [Test, Order(13), Category("Regression")]
        public void TC_011VerifyTheStabilityOfSystemUnderHighLoad()
        {


            //Go to Tc11 in the Json input file
            testData = JsonReaderEdu.ReadTestData("Utilities/TestDataEdu-TC011.json");
            var testCase = testData.TestCases.Find(tc => tc.TestCaseId == "Tc11");
            Thread.Sleep(1000);
            // Keep track of added certificates for cleanup
            var addedEducation = new List<string>();
            var EducationDataCount = testCase.InputData.EducationDataCount;
            var educationData = testCase.InputData.EducationData;



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
                addedEducation.Add(educationData.Degree);

            }
            //Refresh data to get the updated entries
            educationPage.GoToTab();
            //Verify the stability under high load

            try
            {
                assertions.Stability(10);
                Thread.Sleep(3000);
            }


            finally
            {
                // Cleanup: Delete the education data added
                foreach (var educationDataset in addedEducation)
                {
                    // Delete the added education entry
                    educationPage.delete(educationDataset);
                    Thread.Sleep(1000);
                }

            }

        }
    }
}