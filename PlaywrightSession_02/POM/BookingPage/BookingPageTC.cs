using Microsoft.Playwright;
using NUnit.Allure.Core;
using System;

namespace PlaywrightSession_02
{
    [TestFixture]
    [AllureNUnit]
    public class BookingPageTC
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

        [Test]
        public async Task BookHotel()
        { 
            string Url = "https://adactinhotelapp.com/";
            await LoginPage.Url(Url);
            await LoginPage.Login("AmirImam", "AmirImam");
            await SearchPage.SearchHotel("Sydney", "Hotel Creek", "Standard", "1 - One", "2022-10-01", "2022-10-05", "1 - One", "0 - None");
            await SelectPage.SelectHotel();
            await BookingPage.BookHotel("Huda", "Aleem", "N.N", "12345678912121214", "321");
            await BasePage.page.CloseAsync();
        }
    }
}