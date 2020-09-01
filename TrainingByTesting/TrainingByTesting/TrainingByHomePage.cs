using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace TrainingByTesting
{
    public class TrainingByHomePage
    {
        public By AcceptButonLocator = By.XPath("//footer[@id='footer']//div/button");

        public By BodyElementLocator = By.TagName("body");

        public By GlobalIconLocator = By.XPath("//div[@class='location-selector__globe']");

        public string CountryLctMask = "//ul[@class='location__countries-list-countries']//div[contains(text(),'{0}')]";

        public string LanguageSelectorLctMask = "//div[@class='location-selector__list languages-list']//a[starts-with(text(),'{0}')]";

        public string StateLocator = "//label[contains(.,'{0}')]";

        public string HeaderLinksLctMask = "ul.main-nav__list [href$='{0}']";

        public By SearchButonLocator = By.XPath("//input[starts-with(@class,'input-field-search')]");

        public By NoTrainingListTextLocator = By.XPath("//div[@class='training-list__subscribe-text']/span");

        public string TrainingsListLctMask = "//div[contains(@class,'training-list')]/div//div[text()='{0}']";

        private CultureInfo cultureInfo;

        private ResourceManager resourceManager;

        public void InitializeTest()
        {
            Driver.InitializeBrowser();
            Driver.NavigateToUrl("https://training.by/#!/Home");
            this.SetPreferedLanguage(TestContext.Parameters["language"].ToString());
            Driver.WaitForElement(AcceptButonLocator).Click();
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
            }

            catch (Exception)
            {
                Logger.LogError(language + "language doesn't present for this website");
                Console.WriteLine(language + "language doesn't present for this website");
            }
        }

        public string GetHeaderText(string text)
        {
            Logger.LogInfo("Getting the Header text for " + text + " displayed in the website");
            return Driver.GetTextForTheElement(By.CssSelector(string.Format(HeaderLinksLctMask, text))).ToLower();
        }

        public string ExpectedHeaderText(string text) => resourceManager.GetString(text, cultureInfo).ToLower();

        public void PerformSearchAndGetResults(string countryName, string stateName)
        {
            this.ClickOnSearchButton();
            this.SelectCountryName(countryName);
            this.SelectStateName(stateName);
            Driver.ClickOnTheElement(BodyElementLocator);
        }

        public string GetFilePath() => AppDomain.CurrentDomain.BaseDirectory + "..\\..\\TestData\\SearchOptions_" + TestContext.Parameters["language"].ToString() + ".csv";

        public void SelectStateName(string stateName)
        {
            try
            {
                Driver.ClickOnTheElement(By.XPath(string.Format(StateLocator, stateName)));
                Logger.LogInfo("State name " + stateName + " is selected");
            }

            catch (Exception)
            {
                Console.WriteLine("State name " + stateName + " is not displayed in the current page");
                Logger.LogError("State name " + stateName + " is not displayed in the current page");
            }
        }

        public void SelectCountryName(string countryName)
        {
            try
            {
                Driver.ClickOnTheElement(By.XPath(string.Format(CountryLctMask, countryName)));
                Logger.LogInfo("Country name " + countryName + " is selected");
            }

            catch (Exception)
            {
                Console.WriteLine("Country name " + countryName + " is not displayed in the current page");
                Logger.LogError("Country name " + countryName + " is not displayed in the current page");
            }
        }

        public string GetNoTrainingListText()
        {
            Logger.LogInfo("Getting No training list text for the performed search");
            return Driver.GetTextForTheElement(NoTrainingListTextLocator);
        }

        public int GetSearchResultsCount(string location)
        {
            Logger.LogInfo("Getting count of search results for the performed search");
            return Driver.GetCountForTheElement(By.XPath(string.Format(TrainingsListLctMask, location)));
        }

        public void ClickOnSearchButton()
        {
            Logger.LogInfo("Clicking on search button and performing required search");
            Driver.ClickOnTheElement(SearchButonLocator);
        }

        public bool IsSearchResultNoTrainingText(string resultText) => resultText.StartsWith(resourceManager.GetString("NoTrainings", cultureInfo));

        public void CloseChromeBrowser() => Driver.CloseBrowser();
    }
}
