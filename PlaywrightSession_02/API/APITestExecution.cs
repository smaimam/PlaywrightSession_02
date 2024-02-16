using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System.Text.Json;

namespace PlaywrightPOM
{
    [TestFixture]
    public class APITestExecution : PlaywrightTest
    {
        private IAPIRequestContext Request = null;
        //API api = new API();
        
        [SetUp]
        public void TestInit()
        {
            API.Playwright = Playwright;
        }

        #region Localhost Execution
        [Test]
        public async Task GET()
        {
            Request = await Playwright.APIRequest.NewContextAsync(new()
            {
                // All requests we send go to this API endpoint.
                BaseURL = "http://localhost:3000/",
            });

            var comments = await Request.GetAsync(@"comments/");

            Assert.IsTrue(comments.Ok);
            
            var commentsJsonResponse = await comments.JsonAsync();
            JsonElement? comment = null;
            foreach (JsonElement commentObj in commentsJsonResponse?.EnumerateArray())
            {
                if (commentObj.TryGetProperty("body", out var id) == true)
                {
                    if (id.GetString() == "some comment")
                    {
                        comment = commentObj;
                    }
                }
            }
            Assert.IsNotNull(comment);
            Assert.AreEqual("some comment", comment?.GetProperty("body").GetString());
        }

        [Test]
        public async Task POST()
        {
            Request = await Playwright.APIRequest.NewContextAsync(new()
            {
                // All requests we send go to this API endpoint.
                BaseURL = "http://localhost:3000/",
            });

            var data = new Dictionary<string, string>();
            data.Add("id", "6");
            data.Add("body", "Great Tool");
            data.Add("postId", "1");
            var newcomment = await Request.PostAsync("/comments", new() { DataObject = data });
            Assert.IsTrue(newcomment.Ok);

            var getResponse = await API.GETRequest(@"http://localhost:3000/comments", "body", "Great Tool");
           
        }

        [Test]
        public async Task PUT()
        {
            Request = await Playwright.APIRequest.NewContextAsync(new()
            {
                // All requests we send go to this API endpoint.
                BaseURL = "http://localhost:3000/",
            });

            var data = new Dictionary<string, string>();
            data.Add("id", "6");
            data.Add("body", "Great Tool Updated");
            data.Add("postId", "1");
            var newcomment = await Request.PutAsync("/comments/6", new() { DataObject = data });
            Assert.IsTrue(newcomment.Ok);

            var getResponse = await API.GETRequest(@"http://localhost:3000/comments", "body", "Great Tool Updated");
        }

        [Test]
        public async Task PATCH()
        {
            Request = await Playwright.APIRequest.NewContextAsync(new()
            {
                // All requests we send go to this API endpoint.
                BaseURL = "http://localhost:3000/",
            });

            var data = new Dictionary<string, string>();
            data.Add("body", "Great Tool");
          
            var newcomment = await Request.PatchAsync("/comments/6", new() { DataObject = data });
            Assert.IsTrue(newcomment.Ok);

            var getResponse = await API.GETRequest(@"http://localhost:3000/comments", "body", "Great Tool");
        }

        [Test]
        public async Task DELETE()
        {
            Request = await Playwright.APIRequest.NewContextAsync(new()
            {
                // All requests we send go to this API endpoint.
                BaseURL = "http://localhost:3000/",
            });

            var newcomment = await Request.DeleteAsync("/comments/6");
            Assert.IsTrue(newcomment.Ok);              
        }

        #endregion Localhost Execution

    }
}