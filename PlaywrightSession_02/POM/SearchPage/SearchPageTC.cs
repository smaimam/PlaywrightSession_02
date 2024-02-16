using System;
using Microsoft.Playwright;
using NUnit.Allure.Core;

namespace PlaywrightSession_02
{
    [TestFixture]
    [AllureNUnit]
    public class SearchPageTC
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

        }

        [Test]
        public async Task SearchHotel()
        {
            await LoginPage.Url("https://adactinhotelapp.com/");
            await LoginPage.Login("AmirImam", "AmirImam");
            await SearchPage.SearchHotel("Sydney", "Hotel Creek", "Standard", "1 - One", "2022-10-01", "2022-10-05", "1 - One", "0 - None");
            await Assertions.Expect(BasePage.page.Locator("#select_form table tbody td.login_title")).ToHaveTextAsync("Select Hotel ");
            await BasePage.page.CloseAsync();
        }
    }
}
