using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using TrainingByTesting.Utils.Driver;
using TrainingByTesting.Utils.Log;

namespace TrainingByTesting.Tests.UITesting.TrainingTests
{
    public class TrainingTestsHomePage
    {
        public By AcceptButonLocator = By.XPath("//footer[@id='footer']//div/button");

        public By BodyElementLocator = By.TagName("body");

        public By GlobalIconLocator = By.XPath("//div[@class='location-selector__globe']");

        public string CountryLctMask = "//ul[@class='location__countries-list-countries']//div[contains(text(),'{0}')]";

        public string LanguageSelectorLctMask = "//div[@class='location-selector__list languages-list']//a[starts-with(text(),'{0}')]";

        public string StateLctMask = "//label[contains(.,'{0}')]";

        public string HeaderLinksLctMask = "ul.main-nav__list [href$='{0}']";

        public By SearchButonLocator = By.XPath("//input[starts-with(@class,'input-field-search')]");

        public By NoTrainingListTextLocator = By.XPath("//div[@class='training-list__subscribe-text']/span");

        public string TrainingsListLctMask = "//div[contains(@class,'training-list')]/div//div[starts-with(text(),'{0}')]";

        private CultureInfo cultureInfo;

        private ResourceManager resourceManager;

        public void InitializeTest()
        {
            Driver.InitializeBrowserAndNavigateToUrl("https://training.by/#!/Home");
            this.SetPreferedLanguage(TestContext.Parameters["language"].ToString());
            Driver.ClickOnTheElement(AcceptButonLocator);
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

        public string GetHeaderText(string text)
        {
            Logger.LogInfo("Getting the Header text for " + text + " displayed in the website");
            Console.WriteLine("Getting the Header text for " + text + " displayed in the website");
            return Driver.GetTextForTheElement(By.CssSelector(string.Format(HeaderLinksLctMask, text))).ToLower();
        }

        public string ExpectedHeaderText(string text) => resourceManager.GetString(text, cultureInfo).ToLower();

        public void PerformSearchAndGetResults(string countryName, string stateName)
        {
            ClickOnSearchButton();
            SelectCountryName(countryName);
            SelectStateName(stateName);
            Driver.ClickOnTheElement(BodyElementLocator);
        }

        public string[] ReadDataFromCsvFile()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\TestData\\TrainingTests\\SearchOptions_" + TestContext.Parameters["language"].ToString() + ".csv";

            try
            {
                using (var parser = new StreamReader(filePath))
                {
                    Logger.LogInfo("Reading the data from the csv file " + filePath);
                    Console.WriteLine("Reading the data from the csv file " + filePath);
                    return parser.ReadToEnd().Split('\n');
                }
            }

            catch (FileNotFoundException)
            {
                Logger.LogError("The specified file doesn't exists in the path" + filePath);
                Console.WriteLine("The specified file doesn't exists in the path" + filePath);
                throw;
            }
        }

        public void SelectStateName(string stateName)
        {
            try
            {
                Driver.ClickOnTheElement(By.XPath(string.Format(StateLctMask, stateName)));
                Logger.LogInfo("State name " + stateName + " is selected");
                Console.WriteLine("State name " + stateName + " is selected");
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
                Console.WriteLine("Country name " + countryName + " is selected");
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
            Console.WriteLine("Getting No training list text for the performed search");
            return Driver.GetTextForTheElement(NoTrainingListTextLocator);
        }

        public int GetSearchResultsCount(string location)
        {
            Logger.LogInfo("Getting count of search results for the performed search");
            Console.WriteLine("Getting count of search results for the performed search");
            return Driver.GetCountForTheElement(By.XPath(string.Format(TrainingsListLctMask, location)));
        }

        public void ClickOnSearchButton()
        {
            Logger.LogInfo("Clicking on search button and performing required search");
            Console.WriteLine("Clicking on search button and performing required search");
            Driver.ClickOnTheElement(SearchButonLocator);
        }

        public bool IsSearchResultNoTrainingText(string resultText) => resultText.StartsWith(resourceManager.GetString("NoTrainings", cultureInfo));

        public void CloseChromeBrowser() => Driver.CloseBrowser();
    }
}
