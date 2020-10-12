using OpenQA.Selenium;
using System;
using System.IO;
using System.Linq;
using TrainingByTesting.Utils.Driver;
using TrainingByTesting.Utils.Log;

namespace TrainingByTesting.Tests.UITesting.HerokuTests
{
    public class HerokuAppHomePageTests
    {
        public string linkLctMask = "//a[text() = '{0}']";
        public string actionLinkLctMask = "//table[@id='table1']//tr/td[text() = '{0}']//parent::tr/td/a[text()='{1}']";
        public By chooseFileLocator = By.XPath("//input[@id='file-upload']");
        public By uploadButtonLocator = By.XPath("//input[@id='file-submit']");
        public By uploadSuccessTextLocator = By.XPath("//div[@class='example']/h3");
        public By uploadedFileNameLocator = By.XPath("//div[@id='uploaded-files']");
        public By newTabLocator = By.XPath("//a[text()='Click Here']");
        public By BodyLocator = By.TagName("body");

        public void InitialiseTests() => Driver.InitializeBrowserAndNavigateToUrl("http://the-internet.herokuapp.com/");

        public void ClickOnTheLinkWithText(string linkText)
        {
            try
            {
                Driver.ClickOnTheElement(By.XPath(string.Format(linkLctMask, linkText)));
                Logger.LogInfo("Clicked on the link " + linkText);
            }

            catch(Exception)
            {
                Console.WriteLine("The link text " + linkText + "is not available on the Home Page");
                Logger.LogError("The link text " + linkText + "is not available on the Home Page");
            }
        }

        public void UploadTheSelectedFile(string filePath)
        {
            try
            {
                Driver.WaitForElement(chooseFileLocator).SendKeys(Path.GetFullPath(filePath));
                Driver.WaitForElement(uploadButtonLocator).Click();
                Logger.LogInfo("Uploading the file");
                Console.WriteLine("Uploading the file");
            }

            catch (Exception)
            {
                Console.WriteLine("The specified file doesn't exists in the path" + filePath);
                Logger.LogError("The specified file doesn't exists in the path" + filePath);
            }
        }

        public void OpenNewTabAndSwitchToNewTab()
        {
            Driver.ClickOnTheElement(newTabLocator);
            Driver.SwitchToWindow(Driver.GetWindowHandles().Last());
            Logger.LogInfo("Navigated to last opened tab");
            Console.WriteLine("Navigated to last opened tab");
        }

        public string GetFrameTextDisplayed(string frameName)
        {
            Driver.SwitchToDefaultContent();

            if (!frameName.Equals("frame-bottom"))
            {
                Driver.SwitchToFrame("frame-top");
            }

            Driver.SwitchToFrame(frameName);
            Logger.LogInfo("Getting the text displayed in the frame " + frameName);
            Console.WriteLine("Getting the text displayed in the frame " + frameName);
            return Driver.GetTextForTheElement(BodyLocator).Trim();
        }

        public string GetUploadSuccessFullText()
        {
            Logger.LogInfo("Retreiving the File uploaded success message");
            Console.WriteLine("Retreiving the File uploaded success message");
            return Driver.GetTextForTheElement(uploadSuccessTextLocator);
        }

        public string GetUploadedFileName()
        {
            Logger.LogInfo("Retreiving the uploaded File name message");
            Console.WriteLine("Retreiving the uploaded File name message");
            return Driver.GetTextForTheElement(uploadedFileNameLocator).Trim();
        }

        public void CloseCurrentAndSwitchToDefaultTab() => Driver.CloseCurrentAndSwitchToDefaultTab();

        public string GetCurrentTabTitle() => Driver.GetCurrentTabTitle();

        public string GetCurrentUrl() => Driver.GetCurrentUrl();

        public void ClickOnActionToBePerfomedForUser(string name, string action) => Driver.ClickOnTheElement(By.XPath(string.Format(actionLinkLctMask, name, action)));

        public void CloseChromeBrowser() => Driver.CloseBrowser();
    }
}
