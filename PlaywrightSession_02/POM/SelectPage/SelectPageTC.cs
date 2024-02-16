using System;
using Microsoft.Playwright;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;

namespace PlaywrightSession_02
{
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("SelectHotelTest")]
    public class SelectPageTC
    {
        [OneTimeSetUp]
        public async Task ClassInit()
        {

        }

        [OneTimeTearDown]
        public async Task ClassCleanUp()
        {

        }

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

        [Test(Description = "Select Hotel")]
        [AllureTag("R1")]
        [AllureOwner("Huda")]
        [AllureSubSuite("Select")]
        [AllureStep]
        public async Task SelectHotel()
        {
           
            await LoginPage.Url("https://adactinhotelapp.com/");
            await LoginPage.Login("AmirImam", "AmirImam");
            await SearchPage.SearchHotel("Sydney", "Hotel Creek", "Standard", "1 - One", "2022-10-01", "2022-10-05", "1 - One", "0 - None");
            await SelectPage.SelectHotel();
            await Assertions.Expect(BasePage.page.Locator("#book_hotel_form table tbody tr:nth-child(2) td")).ToHaveTextAsync("Book A Hotel ");
            await BasePage.page.CloseAsync();
        }
    }
}
