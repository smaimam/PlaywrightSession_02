using System;
using Microsoft.Playwright;

namespace PlaywrightSession_02
{  
    public class BookingPage : BasePage
    {
        #region BookHotelPage Locators
        public static string fnameTxt = "#first_name";
        public static string lnameTxt = "#last_name";
        public static string addressTxt = "#address";
        public static string cCNoTxt = "#cc_num";
        public static string cCTypeDropDown = "#cc_type";
        public static string expiryDateDropDown = "#cc_exp_month";
        public static string expiryYearDropDown = "#cc_exp_year";
        public static string cVVNoTxt = "#cc_cvv";
        public static string bookNowBtn = "#book_now";
        public static string cancelBtn = "#cancel";
        public static string orderNoTxt = "#order_no";
        #endregion BookHotelPage Locators

        public static async Task BookHotel(string Firstname, string Lastname, string address, string CCNo, string CvvNo)
        {
            exStep = exTest.CreateNode("Book Hotel");
            await FillAsync(fnameTxt, Firstname);
            await FillAsync(lnameTxt, Lastname);
            await FillAsync(addressTxt, address);
            await FillAsync(cCNoTxt, CCNo);
            await TypeAsync(cCTypeDropDown, "VISA");
            await TypeAsync(expiryDateDropDown, "June");
            await TypeAsync(expiryYearDropDown,"2022");
            await FillAsync(cVVNoTxt, CvvNo);
            await ClickAsync(bookNowBtn);
        }
    }
}
