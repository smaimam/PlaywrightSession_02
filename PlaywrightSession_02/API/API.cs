using Microsoft.Playwright;
using System.Text.Json;

namespace PlaywrightPOM
{
    public class API
    {
        public static IAPIRequestContext Request = null;
        public static IPlaywright Playwright;

        #region API Generic Method

        public static async Task<JsonElement?> GETRequest(string endpoint)
        {
            Request = await Playwright.APIRequest.NewContextAsync();
            var response = await Request.GetAsync(@endpoint);
            Assert.IsTrue(response.Ok);
            var jsonResponse = await response.JsonAsync();

            return jsonResponse;
        }

        public static async Task<JsonElement?> GETRequest(string endpoint, string key, string expectedValue)
        {
            Request = await Playwright.APIRequest.NewContextAsync();

            var response = await Request.GetAsync(@endpoint);
            Assert.IsTrue(response.Ok);
            var jsonResponse = await response.JsonAsync();

            return jsonResponse;           

            JsonElement? resData = null;
            foreach (JsonElement responseObj in jsonResponse?.EnumerateArray())
            {
                if (responseObj.TryGetProperty(key, out var id) == true)
                {
                    if (id.GetString() == expectedValue)
                    {
                        resData = responseObj;
                    }
                }
            }
            Assert.IsNotNull(resData);
            Assert.AreEqual(expectedValue, resData?.GetProperty(key).GetString());
        }

        public static async Task<JsonElement?> POSTResponse(string endpoint, Dictionary<string,string> data)
        {
            Request = await Playwright.APIRequest.NewContextAsync();    
            var postResponse = await Request.PostAsync(endpoint, new() { DataObject = data });
            Assert.IsTrue(postResponse.Ok);
            var jsonResponse = await postResponse.JsonAsync();
            return jsonResponse;
        }

        public static async Task<JsonElement?> PUTResponse(string endpoint, Dictionary<string, string> data)
        {
            Request = await Playwright.APIRequest.NewContextAsync();
            var putResponse = await Request.PutAsync(endpoint, new() { DataObject = data });
            Assert.IsTrue(putResponse.Ok);
            var jsonResponse = await putResponse.JsonAsync();
            return jsonResponse;
        }

        public static async Task<JsonElement?> PATCHResponse(string endpoint, Dictionary<string, string> data)
        {
            Request = await Playwright.APIRequest.NewContextAsync();
            var patchResponse = await Request.PatchAsync(endpoint, new() { DataObject = data });
            Assert.IsTrue(patchResponse.Ok);
            var jsonResponse = await patchResponse.JsonAsync();
            return jsonResponse;
        }

        public static async Task<JsonElement?> DELETEResponse(string endpoint)
        {
            Request = await Playwright.APIRequest.NewContextAsync();
            var deleteResponse = await Request.DeleteAsync(endpoint);
            Assert.IsTrue(deleteResponse.Ok);
            var jsonResponse = await deleteResponse.JsonAsync();
            return jsonResponse;
        }


        #endregion API Generic Method
    }
}