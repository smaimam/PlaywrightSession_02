using AventStack.ExtentReports;
using NUnit.Allure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightSession_02
{
    [SetUpFixture]
    public class AssemblyInitialize
    {
        [OneTimeSetUp]
        public static void AssemblyLevelSetup()
        {
            // Create Extent Report
            BasePage.ExtentLogReport("TestReport");
        }

        [OneTimeTearDown]
        public static void AssemblyLevelTearDown()
        {
            BasePage.extent.Flush();
        }
    }
}
