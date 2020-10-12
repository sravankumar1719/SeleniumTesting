using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TrainingByTesting.Utils.Driver;
using TrainingByTesting.Utils.Log;

namespace TrainingByTesting.Tests.FeatureFiles.StepDefinition.GoogleSearch
{
    [Binding]
    public class PerformSearchSteps
    {
        public By searchInputLocator = By.XPath("//input[@name='q']");

        public string searchResultsLctMask = "//h3[starts-with(@class,'LC20lb')]/span[contains(text(),'{0}')]";

        public By searchButtonLocator = By.XPath("//input[@value='Google Search']");

        public string searchQuery = "Selenium";

        [Given(@"Launch Chrome browser")]
        public void GivenLaunchChromeBrowser()
        {
            Driver.InitializeBrowserAndNavigateToUrl("https://www.google.com");
        }

        [When(@"Enter the query Selenium in the search box")]
        public void WhenEnterTheQuerySeleniumInTheSearchBox()
        {
            Logger.LogInfo("Entering the search query: " + searchQuery + " in the search box");
            Driver.EnterTheTextForTheElement(searchInputLocator, searchQuery);
        }

        [When(@"Click on Search Button")]
        public void WhenClickOnSearchButton()
        {
            Logger.LogInfo("Performing search");
            Driver.ClickOnTheElement(searchButtonLocator);
        }

        [Then(@"the search results should contain the search query Selenium")]
        public void ThenTheSearchResultsShouldContainTheSearchQuerySelenium()
        {
            bool result = Driver.IsElementDisplayed(By.XPath(string.Format(searchResultsLctMask, searchQuery)));
            Assert.IsTrue(result, "There are no search results for the search query");
        }

        [AfterScenario("single", "multiple")]
        //[Scope (Tag = "single")][Scope (Tag = "multiple")]
        public void ClosingChromeBrowser()
        {
            Driver.CloseBrowser();
        }
    }
}
