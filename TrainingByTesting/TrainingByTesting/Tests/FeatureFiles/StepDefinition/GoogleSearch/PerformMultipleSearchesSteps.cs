using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TrainingByTesting.Utils.Driver;
using TrainingByTesting.Utils.Log;

namespace TrainingByTesting.Tests.FeatureFiles.StepDefinition.GoogleSearch
{
    [Binding]
    public class PerformMultipleSearchesSteps
    {
        public By searchInputLocator = By.XPath("//input[@name='q']");

        public string searchResultsLctMask = "//h3[starts-with(@class,'LC20lb')]/span[contains(text(),'{0}')]";

        public By searchButtonLocator = By.XPath("//input[@value='Google Search']");

        [Given(@"Navigate to Google home page")]
        public void GivenNavigateToGoogleHomePage()
        {
            Driver.InitializeBrowserAndNavigateToUrl("https://www.google.com");
        }

        [When(@"Enter search query (.*) in the search box")]
        public void WhenEnterSearchQueryInTheSearchBox(string searchQuery)
        {
            Logger.LogInfo("Entering the search query: " + searchQuery + " in the search box");
            Driver.EnterTheTextForTheElement(searchInputLocator, searchQuery);
        }

        [When(@"Click on search button")]
        public void WhenClickOnSearchButton()
        {
            Logger.LogInfo("Performing search");
            Driver.ClickOnTheElement(searchButtonLocator);
        }

        [Then(@"the search results should contain the search query (.*)")]
        public void ThenTheSearchResultsShouldContainTheSearchQuery(string searchQuery)
        {
            bool result = Driver.IsElementDisplayed(By.XPath(string.Format(searchResultsLctMask, searchQuery)));
            Assert.IsTrue(result, "There are no search results for the search query");
        }

        [AfterScenario(tags:"single")]
        public void AfterScenario()
        {
            Driver.CloseBrowser();
        }
    }
}
