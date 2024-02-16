using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;

namespace PlaywrightSession_02
{
    [TestFixture]
    [AllureNUnit]
    public class ExtentExecution : PageTest 
    {

        [SetUp]
        public async Task TestSetup()
        {
            BasePage.exTest = BasePage.extent.CreateTest(TestContext.CurrentContext.Test.MethodName);
            await BasePage.Initialize();
        }

        [TearDown]
        public async Task TearDown()
        {
            await BasePage.page.CloseAsync();
        }

        [Test]
        [Category("Extent")]
        public async Task Login_ValidUserValidPassword()
        {
            await LoginPage.Url("https://adactinhotelapp.com/");
            await LoginPage.Login("AmirTester", "AmirTester");
            await Assertions.Expect(BasePage.page.Locator("td:nth-child(1)[class='welcome_menu']")).ToHaveTextAsync("Welcome to Adactin Group of Hotels");
        }

        [Test]
        [Category("Extent")]
        public async Task Login_InvalidUserInvalidPassword()
        {
            await LoginPage.Url("https://adactinhotelapp.com/");
            await LoginPage.Login("invalid", "invalid123");
            await Assertions.Expect(BasePage.page.Locator("#login_form tbody div.auth_error b")).ToHaveTextAsync("Invalid Login details or Your Password might have expired. Click here to reset your password");
        }
    }
}