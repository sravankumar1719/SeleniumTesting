using OpenQA.Selenium;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using TrainingByTesting.Utils.Driver;
using TrainingByTesting.Utils.Log;

namespace TrainingByTesting.Tests.FeatureFiles.StepDefinition.TrainingWebsite
{
    public class BaseTrainingWebsite
    {
        public string CountryLctMask = "//ul[@class='location__countries-list-countries']//div[contains(text(),'{0}')]";

        public string HeaderLinksLctMask = "ul.main-nav__list [href$='{0}']";

        public string LanguageSelectorLctMask = "//div[@class='location-selector__list languages-list']//a[starts-with(text(),'{0}')]";

        public string StateLctMask = "//label[contains(.,'{0}')]";

        public string TrainingsListLctMask = "//div[contains(@class,'training-list')]/div//div[starts-with(text(),'{0}')]";

        public By AcceptButonLocator = By.XPath("//footer[@id='footer']//div/button");

        public By BodyElementLocator = By.TagName("body");

        public By ClearSelectedLocationLocator = By.XPath("//div[@class='location__cities']//label[contains(@class,'location__location-active-label')]");

        public By GlobalIconLocator = By.XPath("//div[@class='location-selector__globe']");

        public By SearchButonLocator = By.XPath("//input[starts-with(@class,'input-field-search')]");

        public By NoTrainingListTextLocator = By.XPath("//div[@class='training-list__subscribe-text']/span");


        private CultureInfo cultureInfo;

        private ResourceManager resourceManager;

        public enum HeaderText { TrainingList, About, News, FAQ};

        public void SelectCountryName(string countryName)
        {
            try
            {
                Driver.ClickOnTheElement(By.XPath(string.Format(CountryLctMask, resourceManager.GetString(countryName, cultureInfo))));
                Logger.LogInfo("Country name " + countryName + " is selected");
                Console.WriteLine("Country name " + countryName + " is selected");
            }

            catch (Exception)
            {
                Console.WriteLine("Country name " + countryName + " is not displayed in the current page");
                Logger.LogError("Country name " + countryName + " is not displayed in the current page");
            }
        }

        public void ClearSelectedLocation()
        {
            Logger.LogInfo("Clearing the selected location");
            Console.WriteLine("Clearing the selected location");
            Driver.ClickOnTheElement(ClearSelectedLocationLocator);
        }

        public void ClickOnSearchButton()
        {
            Logger.LogInfo("Clicking on search button and performing required search");
            Console.WriteLine("Clicking on search button and performing required search");
            Driver.ClickOnTheElement(SearchButonLocator);
        }

        public string GetExpectedResultTextForSpecifiedLanguage(string result)
        {
            Driver.ClickOnTheElement(BodyElementLocator);

            Logger.LogInfo("Getting expected text for the specified language");
            Console.WriteLine("Getting expected text for the specified language");

            if(result.Equals("No trainings are available."))
            {
                return resourceManager.GetString("NoTrainings", cultureInfo);
            }

            return resourceManager.GetString(result, cultureInfo);
        }

        public string GetNoTrainingListText()
        {
            Logger.LogInfo("Getting No training list text for the performed search");
            Console.WriteLine("Getting No training list text for the performed search");
            return Driver.GetTextForTheElement(NoTrainingListTextLocator);
        }

        public int GetSearchResultsCount(string expectedText)
        {
            Logger.LogInfo("Getting count of search results for the performed search");
            Console.WriteLine("Getting count of search results for the performed search");
            return Driver.GetCountForTheElement(By.XPath(string.Format(TrainingsListLctMask, expectedText)));
        }

        public bool IsSearchResultNoTrainingText(string resultText) => resultText.StartsWith(resourceManager.GetString("NoTrainings", cultureInfo));

        public void NavigateToUrl(string url)
        {
            Driver.InitializeBrowserAndNavigateToUrl(url);
            Driver.ClickOnTheElement(AcceptButonLocator);
        }

        public void SelectStateName(string stateName)
        {
            try
            {
                Driver.ClickOnTheElement(By.XPath(string.Format(StateLctMask, resourceManager.GetString(stateName, cultureInfo))));
                Logger.LogInfo("State name " + stateName + " is selected");
                Console.WriteLine("State name " + stateName + " is selected");
            }

            catch (Exception)
            {
                Console.WriteLine("State name " + stateName + " is not displayed in the current page");
                Logger.LogError("State name " + stateName + " is not displayed in the current page");
            }
        }

        public void SetPreferedLanguage(string language)
        {
            if (language.Equals("Ukrainian"))
            {
                cultureInfo = new CultureInfo("uk");
            }

            else if (language.Equals("Russian"))
            {
                cultureInfo = new CultureInfo("ru");
            }

            resourceManager = new ResourceManager("TrainingByTesting.Resource", Assembly.GetExecutingAssembly());
            try
            {
                Driver.ClickOnTheElement(GlobalIconLocator);
                Driver.ClickOnTheElement(By.XPath(string.Format(LanguageSelectorLctMask, resourceManager.GetString("Language", cultureInfo))));
                Driver.WaitForPageToLoad("?lang");
                Logger.LogInfo("Specified language " + language + " got successfully applied");
                Console.WriteLine("Specified language " + language + " got successfully applied");
            }

            catch (Exception)
            {
                Logger.LogError(language + "language doesn't present for this website");
                Console.WriteLine(language + "language doesn't present for this website");
            }
        }

        public bool VerifyHeaderText(HeaderText text)
        {
            Logger.LogInfo("Getting the Header text for " + text + " displayed in the website");
            Console.WriteLine("Getting the Header text for " + text + " displayed in the website");
            string expectedText = resourceManager.GetString(text.ToString(), cultureInfo).ToLower();
            string actualText = Driver.GetTextForTheElement(By.CssSelector(string.Format(HeaderLinksLctMask, text.ToString()))).ToLower();
            return expectedText.Equals(actualText);
        }
    }
}
