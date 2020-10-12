using NUnit.Framework;
using System;
using TrainingByTesting.Utils.Report;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingByTesting.Tests.UITesting.HerokuTests
{
    [TestFixture]
    public class HerokuAppTests : HerokuAppHomePageTests
    {

        [SetUp]
        public void InitialiseTest()
        {
            this.InitialiseTests();
        }

        [Test]
        public void FileUpload()
        {
            this.ClickOnTheLinkWithText("File Upload");
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\TestData\\TrainingTests\\SearchOptions_English.csv";

            this.UploadTheSelectedFile(filePath);

            Assert.AreEqual("File Uploaded!", this.GetUploadSuccessFullText());
            Assert.AreEqual("SearchOptions_English.csv", this.GetUploadedFileName());
        }

        [Test]
        public void FramesTest()
        {
            this.ClickOnTheLinkWithText("Nested Frames");

            string frameText = GetFrameTextDisplayed("frame-left");

            Assert.AreEqual("LEFT", frameText);

            frameText = GetFrameTextDisplayed("frame-middle");
            Assert.AreEqual("MIDDLE", frameText);

            frameText = GetFrameTextDisplayed("frame-right");
            Assert.AreEqual("RIGHT", frameText);

            frameText = GetFrameTextDisplayed("frame-bottom");
            Assert.AreEqual("BOTTOM", frameText);
        }

        [Test]
        public void SwitchWindowsTest()
        {
            this.ClickOnTheLinkWithText("Multiple Windows");

            this.OpenNewTabAndSwitchToNewTab();

            Assert.AreEqual("New Window", this.GetCurrentTabTitle());

            this.CloseCurrentAndSwitchToDefaultTab();

            Assert.AreEqual("The Internet", this.GetCurrentTabTitle());
        }

        [Test]
        public void TablesTest()
        {
            this.ClickOnTheLinkWithText("Sortable Data Tables");

            this.ClickOnActionToBePerfomedForUser("Doe", "edit");

            Assert.IsTrue(this.GetCurrentUrl().Contains("edit"));

            this.ClickOnActionToBePerfomedForUser("Doe", "delete");

            Assert.IsTrue(this.GetCurrentUrl().Contains("delete"));
        }

        [TearDown]
        public void CleanUp()
        {
            Report.GenerateReport();
            this.CloseChromeBrowser();
        }
    }
}
