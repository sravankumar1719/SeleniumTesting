using NUnit.Framework;
using NUnit.Framework.Interfaces;
using RelevantCodes.ExtentReports;
using System;

namespace TrainingByTesting.Utils.Report
{
    public static class Report
    {
        public static ExtentReports extent;
        public static ExtentTest test;

        public static void GenerateReport()
        {
            extent = new ExtentReports(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Utils\\Report\\Report.html", false);
            string TestCaseName = TestContext.CurrentContext.Test.Name + " " + DateTime.Now.ToString("dd.MM.yyyy HH.mm.ss");

            test = extent.StartTest(TestCaseName);

            TestStatus testCaseResult = TestContext.CurrentContext.Result.Outcome.Status;

            switch (testCaseResult)
            {
                case TestStatus.Skipped:
                    test.Log(LogStatus.Skip, "Test Skipped");
                    break;

                case TestStatus.Failed:
                    test.Log(LogStatus.Fail, "Test Failed");
                    break;

                case TestStatus.Inconclusive:
                    test.Log(LogStatus.Warning, "Inconclusive");
                    break;

                default:
                    test.Log(LogStatus.Pass, "Test Passed");
                    break;
            }

            extent.EndTest(test);
            extent.Flush();
        }
    }
}
