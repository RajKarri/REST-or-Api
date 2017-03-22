using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace RestWrapper.Tests
{
    [TestClass]
    public class RestSharpCallTests
    {
        [TestMethod]
        public void GitHubUserTest1()
        {
            RestSharpCall.Init("https://api.github.com/users/rajkarri", Method.GET);

            var response1 = RestSharpCall.Make();
            var response2 = RestSharpCall.Make<object>();

            var response3 = RestSharpCall.Make(null);
            var response4 = RestSharpCall.Make<object>(null);

            Assert.IsNotNull(response1);
            Assert.IsNotNull(response2);
            Assert.IsNotNull(response3);
            Assert.IsNotNull(response4);
        }

        [TestMethod]
        public void GitHubUserTest2()
        {
            RestSharpCall.Init("https://api.github.com/users", Method.GET);

            IDictionary<string, string> parameters = new Dictionary<string, string>()
            {
                {"since", "135" }
            };

            var response1 = RestSharpCall.Make(null, parameters);
            var response2 = RestSharpCall.Make<object>(null, parameters);

            var response3 = RestSharpCall.Make(parameters, null);
            var response4 = RestSharpCall.Make<object>(parameters, null);

            Assert.IsNotNull(response1);
            Assert.IsNotNull(response2);
            Assert.IsNotNull(response3);
            Assert.IsNotNull(response4);
        }
    }
}
