using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Allure.Core;

namespace PlaywrightSession_02
{
    [TestFixture]
    [AllureNUnit]
    public class PlaywrightLocatorsTC: PageTest
    {
        #region Playwright Locators Testcases

        #region GET BY ROLE - AriaRole

        [Test]
        public async Task GetByRole_Button()
        {
            await Page.GotoAsync("https://demoqa.com/buttons");
            await Page.GetByRole(AriaRole.Button).Nth(1).ClickAsync();
            Thread.Sleep(3000);
        }

        [Test]
        public async Task GetByRole_RadioButton()
        {
            await Page.GotoAsync("https://demoqa.com/radio-button");
            await Page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).ClickAsync(new() { Force = true });
            Thread.Sleep(3000);
        }

        [Test]
        public async Task GetByRole_ListItem()
        {
            await Page.GotoAsync("https://demoqa.com/checkbox");
            await Page.GetByRole(AriaRole.Button, new() { Name = "Toggle" }).ClickAsync();
            await Page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "Documents" }).Locator("svg").Nth(1).ClickAsync();
            Thread.Sleep(3000);
        }

        [Test]
        public async Task GetByRole_Links()
        {
            await Page.GotoAsync("https://demoqa.com/links");
            await Page.GetByRole(AriaRole.Link).First.ClickAsync();
            Thread.Sleep(3000);
        }

        [Test]
        public async Task GetByRole_TextBox()
        {
            await Page.GotoAsync("https://demoqa.com/text-box");
            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Full Name" }).FillAsync("Test");
            await Page.GetByRole(AriaRole.Textbox, new() { Name = "name@example.com" }).FillAsync("Test1@gmail.com");
            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Current Address" }).FillAsync("Test123");
        }
        #endregion

        [Test]
        public async Task GetByPlaceHolder()
        {
            await Page.GotoAsync("https://demoqa.com/text-box");
            await Page.GetByPlaceholder("Full Name").FillAsync("Tester1");
            await Page.GetByPlaceholder("name@example.com").FillAsync("Tester12@gmail.com");
            await Page.GetByPlaceholder("Current Address").FillAsync("ABC Area");
            await Page.Locator("#permanentAddress").FillAsync("ABC Area");
        }

        [Test]
        public async Task GetByLabel()
        {
            await Page.GotoAsync("https://demoqa.com/upload-download");
            await Page.GetByLabel("Select a file").ClickAsync();
        }

        [Test]
        public async Task GetByTitle()
        {
            await Page.GotoAsync("https://demoqa.com/text-box");
            await Page.GetByTitle("ToolsQA").IsVisibleAsync();
        }

        [Test]
        public async Task GetByText()
        {
            await Page.GotoAsync("https://demoqa.com/links");
            await Page.GetByText("Created").ClickAsync();
        }

        [Test]
        public async Task GetByAltText()
        {
            await Page.GotoAsync("https://adactinhotelapp.com/");
            await Page.GetByAltText("Hotel Image 3").IsVisibleAsync();
            await Page.GetByText("New User Register Here").ClickAsync();
            await Page.GetByAltText("Refresh Captcha").ClickAsync();
        }
    }
    #endregion
}