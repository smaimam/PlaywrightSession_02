using Microsoft.Playwright;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;

namespace PlaywrightSession_02
{
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("Login Suite")]
    [AllureFeature("Login Feature")]
    public class AllureExecution
    {
        [SetUp]
        public async Task TestSetup()
        {
            await BasePage.Initialize();
            BasePage.exTest = BasePage.extent.CreateTest(TestContext.CurrentContext.Test.MethodName);
        }

        [TearDown]
        public async Task TearDown()
        {
            await BasePage.page.CloseAsync();
        }

        [Test]
        [Category("Allure")]
        [AllureStory("Login")]
        public async Task Login_ValidUserValidPassword()
        {
            await LoginPage.Url("https://adactinhotelapp.com/");
            await LoginPage.Login("AmirImam", "AmirImam");
            await Assertions.Expect(BasePage.page.Locator("td:nth-child(1)[class='welcome_menu']")).ToHaveTextAsync("Welcome to Adactin Group of Hotels");
        }

        [Test]
        [Category("Allure")]
        public async Task Login_InvalidUserInvalidPassword()
        {
            await LoginPage.Url("https://adactinhotelapp.com/");
            await LoginPage.Login("invalid", "invalid123");
            await Assertions.Expect(BasePage.page.Locator("#login_form tbody div.auth_error b")).ToHaveTextAsync("Invalid Login details or Your Password might have expired. Click here to reset your password");
        }
    }
}