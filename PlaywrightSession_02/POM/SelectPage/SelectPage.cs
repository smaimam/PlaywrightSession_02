using Microsoft.Playwright;
using System;

namespace PlaywrightSession_02
{  
    public class SelectPage : BasePage
    {
        #region SelectPage Locators
        public static string continueBtn = "#continue";
        public static string radBtn = "input[type='radio']";
        #endregion

        public static async Task SelectHotel()
        {
            exStep = exTest.CreateNode("Select");
            await ClickAsync(radBtn);
            await ClickAsync(continueBtn);
        }
    }
}
