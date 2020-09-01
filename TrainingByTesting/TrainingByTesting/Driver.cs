using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;

namespace TrainingByTesting
{
    public static class Driver
    {
        private static IWebDriver driver;

        private static string SauceLabsUrl = "https://Sravankumar1719:706990d6-1db3-4759-b6d9-d5d8c2bf9603@ondemand.saucelabs.com/wd/hub";
        private static string BrowserStackUrl = "https://sravankumar23:qZw1BduYdzXwRYWhYFh1@hub-cloud.browserstack.com/wd/hub";
        private static string SeleniumGridNode = "http://10.181.182.1:4444/wd/hub";

        public static void NavigateToUrl(string url)
        {
            Logger.LogInfo("Navigating to the url " + url);
            driver.Navigate().GoToUrl(url);
        }

        public static void CloseBrowser()
        {
            Logger.LogInfo("Closing the chrome instance");
            driver.Quit();
        }

        public static void InitializeBrowser()
        {
            string TestCaseName = TestContext.CurrentContext.Test.Name + " " + DateTime.Now.ToString("dd.MM.yyyy HH.mm.ss");
            DesiredCapabilities desiredCapabilities = new DesiredCapabilities();
            desiredCapabilities.SetCapability("BrowserName", "chrome");
            desiredCapabilities.SetCapability("name", TestCaseName);
            driver = new RemoteWebDriver(new Uri(SauceLabsUrl), desiredCapabilities);

            driver = new ChromeDriver("C:\\SeleniumDrivers");
            driver.Manage().Window.Maximize();
            Logger.LogInfo("Initialized the chrome browser.");
        }

        public static void SwitchToFrame(string frameName)
        {
            try
            {
                driver.SwitchTo().Frame(frameName);
                Logger.LogInfo("Switched to the frame " + frameName);
            }
            catch (Exception)
            {
                Console.WriteLine("Frame with name " + frameName + "doesn't exists");
                Logger.LogError("Frame with name " + frameName + "doesn't exists");
            }
        }

        public static void SwitchToDefaultContent() => driver.SwitchTo().DefaultContent();

        public static string GetTextForTheElement(By element) => WaitForElement(element).Text;

        public static void ClickOnTheElement(By element) => WaitForElement(element).Click();

        public static int GetCountForTheElement(By element) => WaitForElements(element).Count;

        public static IWebElement WaitForElement(By elementLocator, int timeout = 30)
        {
            try
            {
                var wait = new DefaultWait<IWebDriver>(driver);
                wait.Timeout = TimeSpan.FromSeconds(timeout);
                wait.PollingInterval = TimeSpan.FromSeconds(1);
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                return wait.Until(ExpectedConditions.ElementToBeClickable(elementLocator));
            }

            catch (Exception)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' is not found.");
                throw;
            }
        }

        public static IReadOnlyCollection<IWebElement> WaitForElements(By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new DefaultWait<IWebDriver>(driver);
                wait.Timeout = TimeSpan.FromSeconds(timeout);
                wait.PollingInterval = TimeSpan.FromSeconds(1);
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                return wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' is not found.");
                throw;
            }
        }

        public static bool WaitForPageToLoad(string urlText, int timeout = 30)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.UrlContains(urlText));
            }
            catch (Exception)
            {
                Console.WriteLine("The page is taking much time to load.");
                throw;
            }
        }
    }
}
