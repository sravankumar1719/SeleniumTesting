using NUnit.Framework;
using System.Collections.Generic;
using TrainingByTesting.Utils.Report;

namespace TrainingByTesting.Tests.UITesting.TrainingTests
{
    [TestFixture]
    [Parallelizable]
    public class ValidateHomePageTests : TrainingTestsHomePage
    {
        [SetUp]
        public void StartUp()
        {
            this.InitializeTest();
        }

        [Test]
        public void VerifyHeaderLinks()
        {
            List<string> expectedHeaderNames = new List<string> { "TrainingList", "About", "News", "FAQ" };

            for (int i = 0; i < expectedHeaderNames.Count; i++)
                Assert.AreEqual(this.GetHeaderText(expectedHeaderNames[i]), this.ExpectedHeaderText(expectedHeaderNames[i]));
        }

        [Test]
        public void VerifySearchResults()
        {
            string[] Lines = this.ReadDataFromCsvFile();

            for (int i = 1; i <= Lines.Length - 1; i++)
            {
                string[] dataValue = Lines[i].Replace("\r", "").Split(',');

                this.PerformSearchAndGetResults(dataValue[0], dataValue[1]);

                if (this.IsSearchResultNoTrainingText(dataValue[2]))
                {
                    Assert.AreEqual(dataValue[2], this.GetNoTrainingListText());
                }

                else
                {
                    Assert.True(this.GetSearchResultsCount(dataValue[2]) > 0, "Failed, because there are no search results");
                }
                this.ClickOnSearchButton();
                this.SelectStateName(dataValue[1]);
            }
        }

        [TearDown]
        public void TestCleanUp()
        {
            Report.GenerateReport();
            this.CloseChromeBrowser();
        }
    }
}
