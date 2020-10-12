using NUnit.Framework;
using TechTalk.SpecFlow;
using TrainingByTesting.Utils.Driver;

namespace TrainingByTesting.Tests.FeatureFiles.StepDefinition.TrainingWebsite
{
    [Binding]
    public class TrainingWebsiteSteps : BaseTrainingWebsite
    {
        [Given(@"Navigate to Training Website Home page")]
        public void GivenNavigateToTrainingWebsiteHomePage()
        {
            this.NavigateToUrl("https://training.by/#!/Home");
        }
        
        [Given(@"Change the Website language to specified language")]
        public void GivenChangeTheWebsiteLanguageToSpecifiedLanguage()
        {
            this.SetPreferedLanguage(TestContext.Parameters["language"].ToString());
        }

        [When(@"I click on Search input field")]
        public void WhenIClickOnSearchInputField()
        {
            this.ClickOnSearchButton();
        }

        [When(@"I click on the country (.*)")]
        public void WhenIClickOnTheCountry(string countryName)
        {
            this.SelectCountryName(countryName);
        }
        
        [When(@"Select the state (.*)")]
        public void WhenSelectTheState(string stateName)
        {
            this.SelectStateName(stateName);
        }
        
        [Then(@"Verify the Header names in the home page of the website")]
        public void ThenVerifyTheHeaderNamesInTheHomePageOfTheWebsite()
        {
            Assert.IsTrue(this.VerifyHeaderText(HeaderText.TrainingList), "Training list header text is not matching the expected text");
            Assert.IsTrue(this.VerifyHeaderText(HeaderText.About), "About header text is not matching the expected text");
            Assert.IsTrue(this.VerifyHeaderText(HeaderText.News), "Blog header text is not matching the expected text");
            Assert.IsTrue(this.VerifyHeaderText(HeaderText.FAQ), "FAQ header text is not matching the expected text");
        }
        
        [Then(@"Verify the search result (.*)")]
        public void ThenVerifyTheSearchResult(string result)
        {
            string expectedResult = this.GetExpectedResultTextForSpecifiedLanguage(result);

            if (this.IsSearchResultNoTrainingText(expectedResult))
            {
                Assert.AreEqual(expectedResult, this.GetNoTrainingListText());
            }

            else
            {
                Assert.True(this.GetSearchResultsCount(expectedResult) > 0, "Failed, because there are no search results");
            }

            this.ClickOnSearchButton();
            this.ClearSelectedLocation();
        }
    }
}
