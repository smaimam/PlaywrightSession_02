using Microsoft.Playwright;
using NUnit.Allure.Core;

namespace PlaywrightSession_02
{
    [TestFixture]
    [AllureNUnit]
    public class PlaywrightFeatures
    {
        [Test]
        public async Task Login_msEdge()
        {
            var playwright = await Playwright.CreateAsync();

            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            { Headless = false, SlowMo = 50, Channel = "msedge", Timeout = 80000, });

            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            await page.GotoAsync("https://adactinhotelapp.com/");
            await page.FillAsync("#username", "AmirTester");
            await page.FillAsync("#password", "AmirTester");
            await page.ClickAsync("#login");
        }

        [Test]
        [Category("WebKit")]
        public async Task WebKit_Browser()
        {
            var playwright = await Playwright.CreateAsync();

            var browser = await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions
            { Headless = false, SlowMo = 1, Timeout = 120000, });

            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            await page.GotoAsync("https://adactinhotelapp.com/");
            await page.FillAsync("#username", "AmirTester");
            await page.FillAsync("#password", "AmirTester");
            await page.ClickAsync("#login");
        }

        [Test]
        [Category("FireFox")]
        public async Task Firefox_Browser()
        {
            var playwright = await Playwright.CreateAsync();

            var browser = await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
            { Headless = false, SlowMo = 1, Timeout = 120000 });

            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            await page.GotoAsync("https://adactinhotelapp.com/");
            await page.FillAsync("#username", "AmirTester");
            await page.FillAsync("#password", "AmirTester");
            await page.ClickAsync("#login");
        }

        [Test]
        [Category("SaveState")]
        public async Task TestMethod_SaveState()
        {
            var playwright = await Playwright.CreateAsync();

            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            { Headless = false, SlowMo = 1, });

            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            await page.GotoAsync("https://adactinhotelapp.com/");
            await page.FillAsync("#username", "AmirTester");
            await page.FillAsync("#password", "AmirTester");
            await page.ClickAsync("#login");

            // Save storage state into the file.
            await context.StorageStateAsync(new()
            {
                Path = @"c:\state.json"
            });
            await context.CloseAsync();
            await browser.CloseAsync();
        }

        [Test]
        [Category("SaveVideo")]
        public async Task TestMethod_SaveVideo()
        {
            var playwright = await Playwright.CreateAsync();

            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            { Headless = false, SlowMo = 1, });

            var context = await browser.NewContextAsync(new()
            {
                RecordVideoDir = "videos/",
                RecordVideoSize = new RecordVideoSize() { Width = 1920, Height = 1080 }
            });

            var page = await context.NewPageAsync();
            await page.SetViewportSizeAsync(1920, 1080);

            await page.GotoAsync("https://adactinhotelapp.com/");
            await page.FillAsync("#username", "AmirTester");
            await page.FillAsync("#password", "AmirTester");
            await page.ClickAsync("#login");

            await context.CloseAsync();
            await browser.CloseAsync();
        }

        [Test]
        [Category("Trace")]
        public async Task TestMethod_Trace()
        {
            var playwright = await Playwright.CreateAsync();

            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            { Headless = false, SlowMo = 1, });

            var context = await browser.NewContextAsync(new()
            {
                RecordVideoDir = "videos/",
                RecordVideoSize = new RecordVideoSize() { Width = 1920, Height = 1080 }
            });

            // Start tracing before creating / navigating a page.
            await context.Tracing.StartAsync(new()
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });

            var page = await context.NewPageAsync();
            await page.SetViewportSizeAsync(1920, 1080);

            await page.GotoAsync("https://adactinhotelapp.com/");
            await page.FillAsync("#username", "AmirTester");
            await page.FillAsync("#password", "AmirTester");
            await page.ClickAsync("#login");

            await page.TypeAsync("#location", "Sydney");
            await page.ClickAsync("#Submit");
            await page.ClickAsync("#radiobutton_1");
            await page.ClickAsync("#continue");




            // Stop tracing and export it into a zip archive.
            await context.Tracing.StopAsync(new()
            {
                Path = "trace.zip"
            });

            await context.CloseAsync();
            await browser.CloseAsync();
        }

        [Test]
        [Category("RetrieveState")]
        public async Task TestMethod_RetrieveState()
        {
            var playwright = await Playwright.CreateAsync();

            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            { Headless = false, SlowMo = 1, });

            //  Create a new context with the saved storage state.
            var context = await browser.NewContextAsync(new()
            {
                StorageStatePath = @"c:\state.json",

            });

            var page = await context.NewPageAsync();

            Thread.Sleep(5000);
            await page.GotoAsync("https://adactinhotelapp.com/SelectHotel.php");
            await page.ClickAsync("#continue");
        }
    }
}