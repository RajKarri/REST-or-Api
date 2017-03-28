//-----------------------------------------------------------------------
// <copyright file="RestSharpCallTest.cs" company="XXXXXXX">
// Copyright (c) XXXXXXX. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace RestWrapper.Tests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit testing class
    /// </summary>
    [TestClass]
    public class RestSharpCallTest
    {
        /// <summary>
        /// Unit test case to pull GITHUB user details
        /// </summary>
        [TestMethod]
        public void GitHubUserDetailsTest()
        {
            RestSharpCall.Init("https://api.github.com/users/rajkarri", RestSharpMethod.GET);

            var response1 = RestSharpCall.Make();
            var response2 = RestSharpCall.Make<object>();

            var response3 = RestSharpCall.Make(null);
            var response4 = RestSharpCall.Make<object>(null);

            Assert.IsNotNull(response1);
            Assert.IsNotNull(response2);
            Assert.IsNotNull(response3);
            Assert.IsNotNull(response4);
        }

        /// <summary>
        /// Unit test case to pull few GITHUB users
        /// </summary>
        [TestMethod]
        public void GitHubUsersTest()
        {
            RestSharpCall.Init("https://api.github.com/users", RestSharpMethod.GET);

            IDictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "since", "135" }
            };

            var response1 = RestSharpCall.Make(parameters);
            var response2 = RestSharpCall.Make<object>(parameters);

            Assert.IsNotNull(response1);
            Assert.IsNotNull(response2);
        }
    }
}
