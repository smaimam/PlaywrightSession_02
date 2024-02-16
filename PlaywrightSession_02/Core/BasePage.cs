using Allure.Net.Commons;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using Microsoft.CodeAnalysis;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;
using Status = AventStack.ExtentReports.Status;

namespace PlaywrightSession_02
{
    public class BasePage
    {
        public static IFrameLocator frameLocator { get; set; }
        public static IPage page { get; set; }
        public static IFrame Frame { get; set; }
        public static IBrowser Browser { get; set; }
        public static IBrowserContext Context { get; set; }

        public static ExtentReports extent;
        public static ExtentTest exTest;
        public static ExtentTest exStep;
        public static string dirpath;

        public static JObject jObject;
        public static string pathWithFileNameExtension;
        public static string reportType;


        public static async Task Initialize()
        {
            var playwright = await Playwright.CreateAsync();
            Browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            { Headless = false, SlowMo = 50, Timeout = 80000, });
            page = await Browser.NewPageAsync();
            await page.SetViewportSizeAsync(1920, 1080);
        }
        /// <summary>
        /// This method is used to capture screenshot
        /// </summary>
        /// <param name="status"></param>
        /// <param name="stepDetail"></param>
        /// <returns></returns>
        public static async Task TakeScreenshot(string stepDetail, Status status=Status.Pass)
        {
            #region Get reportType from appsetting.json
            string myJsonString = File.ReadAllText("data.json");
            var myJObject = JObject.Parse(myJsonString);          
            reportType = myJObject.SelectToken("ReportType").Value<string>();
            #endregion

            if (reportType == "extent")
            {
                string path = @"C:\ExtentReports\images\"+DateTime.Now.ToString("yyyyMMddHHmmss");
                pathWithFileNameExtension = @path + ".png";
                await page.Locator("body").ScreenshotAsync(new LocatorScreenshotOptions { Path = pathWithFileNameExtension });
                TestContext.AddTestAttachment(pathWithFileNameExtension);
                exStep.Log(status, stepDetail, MediaEntityBuilder
                .CreateScreenCaptureFromPath(path + ".png").Build());
            }
            else if (reportType == "allure")
            {
                byte[] bytes = await page.ScreenshotAsync();
                AllureLifecycle.Instance.AddAttachment(stepDetail, "image/png", bytes);
            }
        }

        /// <summary>
        /// This method is used to create Extent Report
        /// </summary>
        /// <param name="testcase"></param>
        public static void ExtentLogReport(string testcase)
        {
             extent = new ExtentReports();
            //dirpath = @"..\..\TestExecutionReports\" + '_' + testcase;

            dirpath = @"C:\ExtentReports\_" + testcase;            
                     
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(dirpath);
            htmlReporter.Config.Theme = Theme.Standard;
            extent.AttachReporter(htmlReporter);
        }

        /// <summary>
        /// This method is used to click an element
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        public static async Task ClickAsync(string locator)
        {
            try
            {
                await page.ClickAsync(locator);
                await TakeScreenshot("Click element successfully");
                
            }
            catch (Exception ex)
            {
                await TakeScreenshot("Click Failed: "+ex, Status.Fail);
                Assert.Fail();
            }
        }
        
        /// <summary>
        /// This method is used to enter any text
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task FillAsync(string locator, string data)
        {
            try
            {
                await page.FillAsync(locator, data);
                await TakeScreenshot("Enter text successfully");                
            }
            catch (Exception ex)
            {
                await TakeScreenshot("Write Failed", Status.Fail);
                Assert.Fail();
            }
        }
       
        /// <summary>
        /// This method is used for navigate
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task GotoAsync(string url)
        {
            try
            {
                await page.GotoAsync(url);
                await TakeScreenshot("Url open");
            }
            catch (Exception ex)
            {
                await TakeScreenshot("Unable to open url", Status.Fail);
                Assert.Fail();
            }
        }

        /// <summary>
        /// This method is used to select an option from dropdown
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static async Task SelectOptionAsync(string locator, string data)
        {
            try
            {
                await page.SelectOptionAsync(locator, data);
                await TakeScreenshot("Option select successfully");
            }
            catch (Exception ex)
            {
                await TakeScreenshot("Failed to select");
                Assert.Fail();
            }
        }

        /// <summary>
        /// This method is used to select an option from dropdown
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static async Task TypeAsync(string locator, string data)
        {
            try
            {
                await page.TypeAsync(locator, data);
                await TakeScreenshot("Write successfully");
            }
            catch (Exception ex)
            {
                await TakeScreenshot("Failed to write");
                Assert.Fail();
            }
        }
        public static string IsNullOrEmptyThenString(string text)
        {
            text = (text == String.Empty || text == null) ? "N/A" : text;
            return text;
        }
        public async Task<string> GetAttributeAsync(ILocator locator, string attributeName, LocatorClickOptions option = null)
        {
            string actualValue = string.Empty;
            try
            {
                if (locator != null)
                {
                    actualValue = await locator.GetAttributeAsync(attributeName);
                }
            }
            catch (Exception ex)
            {
                string ExceptionMsg = ex.ToString();
            }
            return actualValue;
        }
        public async Task IsDisabledAsync(ILocator locator, LocatorIsDisabledOptions option = null)
        {
            string attState = string.Empty;
            try
            {
                if (attState == "disabled")
                {
                    attState = await locator.GetAttributeAsync("disabled");
                }
            }
            catch (Exception ex)
            {
                string ExceptionMsg = ex.ToString();
            }
        }
        public async Task BrowseFile(ILocator locator, string filePath)
        {
            try
            {
                await locator.SetInputFilesAsync(filePath);
                
            }
            catch (Exception ex)
            {
                string ExceptionMsg = ex.ToString();
                Assert.Fail("unable to upload");
            }
        }
        public async Task TabOutFromElement()
        {
            try
            {
                await page.Keyboard.PressAsync("Tab");
            }
            catch (Exception ex)
            {
                string ExceptionMsg = ex.ToString();
                Assert.Fail("Tab out not performed");
            }
        }

        public static void ReadJSON(string filename)
        {
            string myJsonString = File.ReadAllText(filename);
            jObject = JObject.Parse(myJsonString);
        }
    }
       
}
