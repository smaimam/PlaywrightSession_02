
using Microsoft.Playwright;

namespace PlaywrightSession_02
{
    public class LoginPage : BasePage
    {
        #region LoginPage Locators
        public static string userNameTxt = "#username";
        public static string passwordTxt = "#password";
        public static string loginBtn = "#login";
        #endregion

        public static async Task Url(string url)
        {
            exStep = exTest.CreateNode("Open Url");
            await page.GotoAsync(url);          
        }

        public static async Task Login(string username, string pass)
        {
            exStep = exTest.CreateNode("Login");

            await FillAsync(userNameTxt, username);
            await FillAsync(passwordTxt, pass);
            await ClickAsync(loginBtn);
            
        }
        //public static async Task Login(string url, string username, string pass)
        //{
        //   exStep = exTest.CreateNode("Login");
        //    await GotoAsync(url);
        //    await FillAsync(userNameTxt, username);
        //    await FillAsync(passwordTxt, pass);
        //    await ClickAsync(loginBtn);
        //}
    }
}