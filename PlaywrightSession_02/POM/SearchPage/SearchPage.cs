using System;
using Microsoft.Playwright;

namespace PlaywrightSession_02
{  
    public class SearchPage : BasePage
    {
        #region SearchPage Locators
        public static string locationTxt = "#location";
        public static string hotelsTxt = "#hotels";
        public static string roomTypeTxt = "#room_type";
        public static string roomNosTxt = "#room_nos";
        public static string datePickInTxt = "#datepick_in";
        public static string datePickOutTxt = "#datepick_out";
        public static string adultRoomTxt = "#adult_room";
        public static string childRoomTxt = "#child_room";
        public static string searchBtn = "#Submit";
        #endregion

        public static async Task SearchHotel(string Location, string Hotel, string RoomType, string RoomNo, string DateIn, string DateOut, string AdultRoom, string ChildRoom)
        {
            exStep = exTest.CreateNode("Search Hotel");
            await SelectOptionAsync(locationTxt, Location);
            await SelectOptionAsync(hotelsTxt, Hotel);
            await SelectOptionAsync(roomTypeTxt, RoomType);
            await SelectOptionAsync(roomNosTxt, RoomNo);
            await FillAsync(datePickInTxt, DateIn);
            await FillAsync(datePickOutTxt, DateOut);
            await SelectOptionAsync(adultRoomTxt, AdultRoom);
            await SelectOptionAsync(childRoomTxt, ChildRoom);
            await ClickAsync(searchBtn);
        }
    }
}
